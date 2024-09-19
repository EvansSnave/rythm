using UnityEngine;

public class HealthItem : MonoBehaviour
{
	[SerializeField] private float _healthValue;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<Health>().Heal(_healthValue);
		}
	}
}
