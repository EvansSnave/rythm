using System.Collections;
using UnityEngine;

public class PlayerRythm : MonoBehaviour
{
	[SerializeField] private Rhythm _rhythm;
	[SerializeField] private float _perfectThreshold = 1.0f;
	private int _perfectNotes = 0;
	private int _totalNotes = 4;
	private bool _canPlayNotes = true;
	private float _currentBeatTime = 0;
	private bool _notePlayedThisBeat = true;
	private int _keyPressCount = 0;
	private float _keyPressTime = 0;
	private int _currentBeatIndex;
	private float _startTime;

	private void Start()
	{
		if (_rhythm != null)
		{
			_rhythm.OnBeat += HandleBeat;
		}
		_startTime = Time.time;
	}

	private void Update()
	{
		if (_canPlayNotes) 
		{ 
			CheckForPlayerInput();
		}
	}

	private void CheckForPlayerInput()
	{
		bool leftArrow = Input.GetKeyDown(KeyCode.LeftArrow);
		bool rightArrow = Input.GetKeyDown(KeyCode.RightArrow);
		bool upArrow = Input.GetKeyDown(KeyCode.UpArrow);
		bool downArrow = Input.GetKeyDown(KeyCode.DownArrow);

		int keysPressed = (leftArrow ? 1 : 0) + (rightArrow ? 1 : 0) + (upArrow ? 1 : 0) + (downArrow ? 1 : 0);

		if (keysPressed > 1)
		{
			Debug.Log("Multiple keys pressed! Missed Note!");
			ResetPerfectNotes();
		}
		else if (keysPressed == 1)
		{
			_keyPressTime = Time.time;
			_keyPressCount++;

			if (_keyPressCount == 1)
			{

				if (IsPerfectNote())
				{
					Debug.Log("Perfect Note!");
					_perfectNotes++;
					_notePlayedThisBeat = true;

					if (_perfectNotes == _totalNotes)
					{
						Debug.Log("Perfect Command!");
						_canPlayNotes = false;
						StartCoroutine(ResetAfterTempo());
						_perfectNotes = 0;
					}
				}
				else
				{
					Debug.Log("Missed Note!");
					ResetPerfectNotes();
				}
			}
			else
			{
				Debug.Log("Too many key presses during this beat! Missed Note!");
				ResetPerfectNotes();
			}
		}
	}

	private bool IsPerfectNote()
	{
		// Determine which tempo cycle we're in
		float elapsedTime = Time.time - _startTime;
		float currentTempoCycleStart = Mathf.Floor(elapsedTime / _rhythm.tempoDuration) * _rhythm.tempoDuration;

		// Get the expected time for this beat in the current tempo cycle
		float expectedBeatTime = currentTempoCycleStart + _rhythm.beatTimings[_currentBeatIndex];

		// Calculate the allowed time range for a perfect note hit
		float minTime = expectedBeatTime - _perfectThreshold;
		float maxTime = expectedBeatTime + _perfectThreshold;

		Debug.Log("Beat Time Range: [" + minTime + ", " + maxTime + "]");
		Debug.Log("Key press time: " + _keyPressTime);

		return _keyPressTime >= minTime && _keyPressTime <= maxTime;
	}

	private IEnumerator ResetAfterTempo()
	{
		yield return new WaitForSeconds(_rhythm.tempoDuration);
		_canPlayNotes = true;
	}

	private void ResetPerfectNotes() { 
		_perfectNotes = 0;
		_notePlayedThisBeat = false;
		_keyPressCount = 0;
		_currentBeatIndex = 0;
	}

	private void HandleBeat()
	{
		if (_notePlayedThisBeat == false && _perfectNotes > 0)
		{
			Debug.Log("No input!");
			ResetPerfectNotes();
		}

		_currentBeatTime = Time.time;
		_notePlayedThisBeat = false;
		_keyPressCount = 0;
		_currentBeatIndex = (_currentBeatIndex + 1) % _rhythm.beatTimings.Length;
	}
}
