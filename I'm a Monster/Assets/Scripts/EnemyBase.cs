using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour {
	NavMeshAgent agent;
	GameObject pl;
	[SerializeField]
	int fieldOfView = 120;
	[SerializeField]
	int viewDistance = 3;
	[SerializeField]
	int maxWalkDistance = 10;
	[SerializeField]
	int range = 2;
	[SerializeField]
	int attackCooldown = 2;
	[SerializeField]
	int damage = 15;

	int atkCooldown;

	public static Vector3 knownPos = new Vector3(9999,9999,9999);
	private Vector3 unknown = new Vector3 (9999, 9999, 9999);

	void Start(){
		agent = GetComponent<NavMeshAgent> ();
		pl = GameObject.Find ("Player");
		atkCooldown = attackCooldown * 50;
	}

	void Update(){
		if (pl == null) {
			Debug.LogError ("Shoud restart");
			return;
		}
		Vector3 tmp = pl.transform.position - transform.position;
		if (Vector3.Angle (tmp,transform.forward) < fieldOfView / 2){ 
			if(Vector3.Distance(transform.position,pl.transform.position) < viewDistance) {
				Debug.Log ("Detect");
				Detect ();
			}
		}

		if (knownPos != unknown && Vector3.Distance (transform.position, knownPos) < maxWalkDistance) {
			agent.SetDestination (knownPos);
		}
	}

	void Detect(){
		knownPos = pl.transform.position;
	}

	void Attack(){
		Debug.Log ("LUNCH ATTACK");
		if (pl.GetComponent<Health> ().TakeDamage (damage, gameObject, Enums.DamageTypes.FORCE))
			knownPos = unknown;
		atkCooldown = attackCooldown * 50;
	}

	void FixedUpdate(){
		if (atkCooldown > 0)
			atkCooldown--;
		if (knownPos != unknown && Vector3.Distance (transform.position, pl.transform.position) < range && atkCooldown <= 0) {
			Attack ();
		}
	}
}

