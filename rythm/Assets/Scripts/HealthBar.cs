using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Health _playerHealth;
	[SerializeField] private Image _totalHealthBar;
	[SerializeField] private Image _currentHealthBar;

	private void Start()
	{
		_totalHealthBar.fillAmount = _playerHealth.currentHealth / 4;
	}

	private void Update()
	{
		_currentHealthBar.fillAmount = _playerHealth.currentHealth / 4;
	}
}
