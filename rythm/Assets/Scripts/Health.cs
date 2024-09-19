using UnityEngine;

public class Health : MonoBehaviour
{
	public float startingHealth;
	public float currentHealth {  get; private set; }

	private void Awake()
	{
		currentHealth = startingHealth;
	}

	public void TakeDamage(float damage)
	{
		currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

		if (currentHealth < 0)
		{

		}
		else
		{

		}
	}

	public void Heal(float amount)
	{
		currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
	}
}
