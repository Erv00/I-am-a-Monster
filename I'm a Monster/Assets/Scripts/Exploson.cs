using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

					if (rb != null)
						rb.AddExplosionForce(500, inf.point, 5, 3.0F);
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			Collider[] colliders = Physics.OverlapSphere(transform.position+Vector3.down, 5);
			foreach (Collider hit in colliders)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();

				if (rb != null && rb != GetComponent<Rigidbody>())
					rb.AddExplosionForce(500, transform.position+Vector3.down, 5, 3.0F);
			}	
		}
	}
}
