using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour {
	NavMeshAgent agent;
	public GameObject pl;
	[SerializeField]
	int fieldOfView = 120;
	[SerializeField]
	int viewDistance = 3;
	[SerializeField]
	int maxWalkDistance = 100;
	[SerializeField]
	int range = 2;
	[SerializeField]
	int attackCooldown = 2;
	[SerializeField]
	int damage = 15;

	int atkCooldown;

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
		Detect ();
		UpdateDest ();
	}

	public virtual void Detect(){
		Vector3 tmp = pl.transform.position - transform.position;
		if (Vector3.Angle (tmp,transform.forward) < fieldOfView / 2){ 
			if(Vector3.Distance(transform.position,pl.transform.position) < viewDistance) {
				Debug.Log ("Detect");
				PlayerPos.knownPos = pl.transform.position;
			}
		}
	}

	public virtual void UpdateDest(){
		if (PlayerPos.knownPos != PlayerPos.unknown && Vector3.Distance (transform.position, PlayerPos.knownPos) < maxWalkDistance) {
			agent.SetDestination (PlayerPos.knownPos);
			Debug.Log ("Update Dest");
		}
	}

	public virtual void Attack(){
		Debug.Log ("LUNCH ATTACK");
		if (pl.GetComponent<Health> ().TakeDamage (damage, gameObject, Enums.DamageTypes.FORCE))
			PlayerPos.knownPos = PlayerPos.unknown;
		atkCooldown = attackCooldown * 50;
	}

	void FixedUpdate(){
		if (atkCooldown > 0)
			atkCooldown--;
		if (PlayerPos.knownPos != PlayerPos.unknown && Vector3.Distance (transform.position, pl.transform.position) < range && atkCooldown <= 0) {
			Attack ();
		}
	}
}

