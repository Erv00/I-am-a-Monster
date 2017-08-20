using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour {

	NavMeshAgent agent;
	void Start(){
		agent = GetComponent<NavMeshAgent> ();
	}


	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit inf;
			if (Physics.Raycast(ray,out inf)) {
				agent.SetDestination (inf.point);
			}
		}
	}
}
