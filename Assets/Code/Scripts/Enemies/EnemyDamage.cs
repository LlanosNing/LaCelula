using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
	public int EnemyHealth = 100;

	public GameObject deathEffect;

	public void TakeDamage(int damage)
	{
		EnemyHealth -= damage;

		if (EnemyHealth <= 0)
		{
			Die();
		}
	}
	void Die()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
