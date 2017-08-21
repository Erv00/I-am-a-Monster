using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Move : MonoBehaviour {
	
	NavMeshAgent agent;
	void Start(){
		agent = GetComponent<NavMeshAgent> ();
	}


	void Update(){
		if (Input.GetMouseButtonDown (1)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit inf;
			if (Physics.Raycast (ray, out inf)) {
				agent.SetDestination (inf.point);
			}
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			agent.SetDestination (transform.position);
		}
	}
}
