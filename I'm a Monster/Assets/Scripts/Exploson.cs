using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
public class Exploson : MonoBehaviour {

	public float explosonCooldownS = 2;
	[SerializeField]
	private int expCooldown;
	[SerializeField]
	GameObject ploson;

	void Start(){
		expCooldown = Mathf.RoundToInt(explosonCooldownS * 50f);
	}

	void Update () {
		if (Input.GetMouseButtonDown(0) && expCooldown <=0) {
			Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit Minf;
			if (Physics.Raycast(mouse,out Minf)) {
				CastIt (Minf);
			}
		}
		if (Input.GetKeyDown (KeyCode.E) && expCooldown <= 0) {
			GetComponent<Health> ().TakeDamage (2, gameObject, Enums.DamageTypes.EXPLOSION);
			Collider[] colliders = Physics.OverlapSphere(ploson.transform.position, 5);
			expCooldown = Mathf.RoundToInt(explosonCooldownS * 50);
			foreach (Collider hit in colliders)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();

				if (rb != null && rb != GetComponent<Rigidbody>())
					rb.AddExplosionForce(500, transform.position, 5, 3.0F);
				DoDamage (hit);

				NavMeshAgent ag = hit.GetComponent<NavMeshAgent>();
				if (ag != null) {
					ag.enabled = false;
					StartCoroutine (Concussion (ag));
				}
				Debug.Log ("EXPLOSON");
			}
		}
	}

	void DoDamage(Collider c){
		Health h = c.GetComponent<Health> ();
		if (h != null) {
			h.TakeDamage (10, gameObject, Enums.DamageTypes.EXPLOSION);
		}
	}

	IEnumerator Concussion(NavMeshAgent a){
		yield return new WaitForSeconds (3);
		a.enabled = true;
		a.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		a.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
	}

	void FixedUpdate(){
		if(expCooldown > 0)
			expCooldown--;
	}

	Ray pos;
	RaycastHit posInf;
	void CastIt(RaycastHit Minf){
		pos = new Ray (ploson.transform.position, (Minf.point - ploson.transform.position));
		Physics.Raycast (pos, out posInf);
		Debug.DrawLine (ploson.transform.position, posInf.point, Color.green, 2);
		if (posInf.point == Minf.point) {
			expCooldown = Mathf.RoundToInt (explosonCooldownS * 50);
			Collider[] colliders = Physics.OverlapSphere (Minf.point, 5);
			foreach (Collider hit in colliders) {
				Rigidbody rb = hit.GetComponent<Rigidbody> ();

				if (rb != null)
					rb.AddExplosionForce (500, Minf.point, 5, 3.0F);
				DoDamage (hit);

				NavMeshAgent ag = hit.GetComponent<NavMeshAgent> ();
				if (ag != null) {
					ag.enabled = false;
					StartCoroutine (Concussion (ag));
				}
				Debug.Log ("EXPLOSON");
			}
		} else {
			if (posInf.collider.name == "Player") {
				transform.LookAt (Minf.point);
				pos = new Ray (ploson.transform.position, (Minf.point - ploson.transform.position));
				Physics.Raycast (pos, out posInf);
				Debug.DrawLine (ploson.transform.position, posInf.point, Color.green, 2);
				if (posInf.point == Minf.point) {
					expCooldown = Mathf.RoundToInt (explosonCooldownS * 50);
					Collider[] colliders = Physics.OverlapSphere (Minf.point, 5);
					foreach (Collider hit in colliders) {
						Rigidbody rb = hit.GetComponent<Rigidbody> ();

						if (rb != null)
							rb.AddExplosionForce (500, Minf.point, 5, 3.0F);
						DoDamage (hit);

						NavMeshAgent ag = hit.GetComponent<NavMeshAgent> ();
						if (ag != null) {
							ag.enabled = false;
							StartCoroutine (Concussion (ag));
						}
						Debug.Log ("EXPLOSON");
					}
				}
			}
		}
	}
}
