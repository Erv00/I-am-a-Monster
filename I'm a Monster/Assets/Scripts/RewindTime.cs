using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTime : MonoBehaviour {

	LinkedList<TimeFrame> stack;
	public bool isRewinding = false;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		stack = new LinkedList<TimeFrame> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			StartRewind ();
		if (Input.GetKeyUp (KeyCode.Space))
			StopRewind ();
	}

	void StartRewind(){
		isRewinding = true;
		if (rb != null)
			rb.isKinematic = true;
	}

	void StopRewind(){
		isRewinding = false;
		if (rb != null)
			rb.isKinematic = false;
	}

	void FixedUpdate(){
		if (isRewinding)
			Rewind ();
		else
			Record ();
	}

	void Rewind(){
		if (stack.Count > 0) {
			TimeFrame tmp = stack.First.Value;
			transform.position = tmp.position;
			transform.rotation = tmp.rotation;
			stack.RemoveFirst ();
		} else {
			StopRewind ();
		}
	}

	void Record(){
		if(stack.Count>Mathf.Round(5f/Time.fixedDeltaTime))
			stack.RemoveLast ();
		stack.AddFirst (new TimeFrame(transform.position,transform.rotation));
	}
}
