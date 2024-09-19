using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	[SerializeField] private float _damage;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<Health>().TakeDamage(_damage);
		}
	}
}
