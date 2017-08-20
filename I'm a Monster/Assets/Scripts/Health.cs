using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	[SerializeField]
	private int maxHealth = 100;
	[SerializeField]
	private int health;
	public enum DamageTypes 
	{
		EXPLOSION,FORCE,FIRE,MAGIC
	}
	public enum HealthSources 
	{
		MEDICINE,HEAL_RAY,IMMUNITY,MAGIC
	}

	public void Start(){
		health = maxHealth;
	}

	public bool TakeDamage(int amm, GameObject inflicter,DamageTypes dType){
		health -= CalculateDamage (amm, dType);
		if (health <= 0) {
			Die ();
			return true;
		}
		return false;
	}

	public void AddHP(int amm, HealthSources src){
		health += amm;
	}

	private int CalculateDamage(int x,DamageTypes type){
		return x;
	}

	private void Die(){
		Debug.Log ("NOOOO");
		Destroy (gameObject);
	}
}
