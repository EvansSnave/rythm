using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Rhythm : MonoBehaviour
{
	private Image _rhythmGuide;
	[SerializeField] private float _fadeDuration = 1.0f;

	public float tempoDuration = 4.0f;
	public float[] beatTimings;
	public delegate void OnBeatDelegate();
	public event OnBeatDelegate OnBeat;

	private void Awake()
	{
		_rhythmGuide = GetComponent<Image>();

		if (_rhythmGuide == null)
		{
			Debug.LogError("No Image component found on the GameObject!");
		}
	}

	private void Start()
	{
		StartCoroutine(RhythmLoop());
	}

	private IEnumerator RhythmLoop()
	{
		float totalElapsedTime = 0f;

		while (true)
		{
			// Loop through the beat timings array and invoke beats at the correct times.
			foreach (float beatTime in beatTimings)
			{
				// Calculate the next beat timing based on the current cycle (totalElapsedTime).
				float nextBeatTime = totalElapsedTime + beatTime;
				float currentTime = Time.time;

				// If the current time is less than the next beat, wait for the beat to occur.
				yield return new WaitForSeconds(nextBeatTime - currentTime);

				// Trigger the beat.
				Beat();
			}

			// After one full tempo cycle (all beats in the array), increase the total elapsed time.
			totalElapsedTime += tempoDuration;
		}
	}

	private void Beat()
	{
		if (_rhythmGuide == null) return;

		Color imageColor = _rhythmGuide.color;
		imageColor.a = 1;
		_rhythmGuide.color = imageColor;

		StartCoroutine(FadeOut());

		OnBeat?.Invoke();
	}

	private IEnumerator FadeOut()
	{
		float elapsedTime = 0f;
		Color startColor = _rhythmGuide.color;
		Color endColor = new(startColor.r, startColor.g, startColor.b, 0);

		while (elapsedTime < _fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			_rhythmGuide.color = Color.Lerp(startColor, endColor, elapsedTime / _fadeDuration);
			yield return null;
		}

		_rhythmGuide.color = endColor;
	}
}
