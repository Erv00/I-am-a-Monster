using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Exploson : MonoBehaviour {
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit inf;
			if (Physics.Raycast(ray,out inf)) {
				Collider[] colliders = Physics.OverlapSphere(inf.point, 5);
				foreach (Collider hit in colliders)
				{
					Rigidbody rb = hit.GetComponent<Rigidbody>();

					if (rb != null && rb != GetComponent<Rigidbody>())
						rb.AddExplosionForce(500, inf.point, 5, 3.0F);
					DoDamage (hit);
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			GetComponent<Health> ().TakeDamage (2, gameObject, Enums.DamageTypes.EXPLOSION);
			Collider[] colliders = Physics.OverlapSphere(transform.position, 5);
			foreach (Collider hit in colliders)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();

				if (rb != null && rb != GetComponent<Rigidbody>())
					rb.AddExplosionForce(500, transform.position, 5, 3.0F);
				DoDamage (hit);
			}
		}
	}

	void DoDamage(Collider c){
		Health h = c.GetComponent<Health> ();
		if (h != null) {
			h.TakeDamage (10, gameObject, Enums.DamageTypes.EXPLOSION);
		}
	}
}
