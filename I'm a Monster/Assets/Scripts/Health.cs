using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	[SerializeField]
	private int maxHealth = 100;
	[SerializeField]
	private int health;
	[SerializeField]
	public bool resistantToExplosion, resistantToForce, resistantToFire,resistantToMagic, resistantToMedicine,resistantToHeal;
	[SerializeField]
	public bool weakToExplosion, weakToForce, weakToFire,weakToMagic, weakToMedicine,weakToHeal;

	public void Start(){
		health = maxHealth;
	}

	public bool TakeDamage(int amm, GameObject inflicter,Enums.DamageTypes dType){
		health -= CalculateDamage (amm, dType);
		if (health <= 0) {
			Die ();
			return true;
		}
		return false;
	}

	public void AddHP(int amm, Enums.HealthSources src){
		health += amm;
	}

	private int CalculateDamage(int x,Enums.DamageTypes type){
		switch (type) {

		case Enums.DamageTypes.EXPLOSION:
			if (resistantToExplosion)
				x /= 2;
			if (weakToExplosion)
				x *= 2;
			break;
		
		case Enums.DamageTypes.FIRE:
			if (resistantToFire)
				x /= 2;
			if (weakToFire)
				x *= 2;
			break;

		case Enums.DamageTypes.FORCE:
			if (resistantToForce)
				x /= 2;
			if (weakToForce)
				x *= 2;
			break;

		case Enums.DamageTypes.MAGIC:
			if (resistantToMagic)
				x /= 2;
			if (weakToMagic)
				x *= 2;
			break;

		default:
			break;
		}
		return x;
	}

	private void Die(){
		Debug.Log ("NOOOO");
		Destroy (gameObject);
	}
}
