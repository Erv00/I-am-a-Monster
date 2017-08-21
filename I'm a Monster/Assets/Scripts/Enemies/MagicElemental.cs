using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicElemental : EnemyBase {
	public override void UpdateDest ()
	{
		base.UpdateDest ();
		Ray r = new Ray (transform.position, (pl.transform.position - transform.position));
		Debug.DrawRay (transform.position, (pl.transform.position - transform.position),Color.red);
		RaycastHit hit;
		if (Physics.Raycast (r, out hit)) {
			if (hit.collider.name == "Player" && PlayerPos.knownPos != PlayerPos.unknown) {
				transform.LookAt (hit.point);
			}
		}
	}
}
