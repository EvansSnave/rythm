using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Rhythm : MonoBehaviour
{
	private Image _rhythmGuide;
	[SerializeField] private float _fadeDuration = 1.0f;
	[SerializeField] private float _tempoDuration = 4.0f;
	[SerializeField] private float[] _beatTimings;

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
		while(true)
		{
			foreach(float beatTime in _beatTimings) {
				Invoke("Beat", beatTime);
			}

			yield return new WaitForSeconds(_tempoDuration);
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
