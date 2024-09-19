using System.Collections;
using UnityEngine;

public class PlayerRythm : MonoBehaviour
{
	[SerializeField] private Rhythm _rhythm;
	[SerializeField] private float _perfectThreshold = 1.0f;
	[SerializeField] private float _tempoDuration = 4;
	private int _perfectNotes = 0;
	private int _totalNotes = 4;
	private bool _canPlayNotes = true;
	private float _currentBeatTime = 0;
	private bool _notePlayedThisBeat = true;
	private int _keyPressCount = 0;

	private void Start()
	{
		if (_rhythm != null)
		{
			_rhythm.OnBeat += HandleBeat;
		}
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
		float timeSinceLastBeat = Time.time - _currentBeatTime;
		return Mathf.Abs(timeSinceLastBeat) <= _perfectThreshold;
	}

	private IEnumerator ResetAfterTempo()
	{
		yield return new WaitForSeconds(_tempoDuration);
		_canPlayNotes = true;
	}

	private void ResetPerfectNotes() { 
		_perfectNotes = 0;
		_notePlayedThisBeat = false;
		_keyPressCount = 0;
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
	}
}
