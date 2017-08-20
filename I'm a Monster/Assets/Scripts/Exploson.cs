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
	}
}
