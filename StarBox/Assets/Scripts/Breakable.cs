using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

	Vector3 startPos;
	Quaternion startRot;
	Rigidbody rb;
	GameObject parentObject;

	void Start()
	{
		parentObject = transform.parent.gameObject;
		startPos = parentObject.transform.position;
		startRot = parentObject.transform.rotation;
		rb = GetComponentInParent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "End")
		{
			rb.isKinematic = false;
			Player player = col.GetComponentInParent<Player> ();
			StartCoroutine (Respawn (player));
		}

	}

	IEnumerator Respawn(Player player)
	{
		yield return null;

		player.StartCoroutine (player.Respawn ());

		yield return new WaitForSeconds (1.8f);

		rb.isKinematic = true;
		parentObject.transform.rotation = startRot;
		parentObject.transform.position = startPos;
	}
}
