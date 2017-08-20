using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploson : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit inf;
			if (Physics.Raycast(ray,out inf)) {
				//agent.SetDestination (inf.point);
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
