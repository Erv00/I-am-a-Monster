using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Move : MonoBehaviour {
	
	NavMeshAgent agent;
	LinkedList<Vector3> waypoints = new LinkedList<Vector3>();
	void Start(){
		agent = GetComponent<NavMeshAgent> ();
	}


	void Update(){
		if (Input.GetMouseButtonDown(1)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit inf;
			if (Physics.Raycast(ray,out inf)) {
				waypoints.AddLast(inf.point);
			}
		}
		if (waypoints.Count > 0) {
			agent.SetDestination (waypoints.First.Value);
			float dist = Vector3.Distance (waypoints.First.Value, transform.position);
			if (dist < 2) {
				waypoints.RemoveFirst ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			waypoints.RemoveFirst ();
			agent.SetDestination (waypoints.First.Value);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		if(agent != null)
		Gizmos.DrawCube (agent.destination, new Vector3(1,3,1));
	}
}
