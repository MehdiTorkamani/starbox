using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Vector3 startPos;
	Quaternion startRot;
	Rigidbody rb;
	bool readyTurn = true;
	bool dying = false;

	public int speed;
	float fSpeed;

	AudioSource audioSource;
	[SerializeField] AudioClip moveSound;
	[SerializeField] AudioClip winSound;
	[SerializeField] AudioClip dieSound;

	public Transform [] turningPoints = new Transform[12];

	void Start()
	{
		startPos = transform.position;
		startRot = transform.rotation;

		fSpeed = (float)speed;
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void FixedUpdate()
	{
		if (readyTurn && !Win.winning) {
			if (Input.GetKey (KeyCode.UpArrow))
			{
				StartCoroutine (Up ());
				return;
			}
			if (Input.GetKey (KeyCode.DownArrow))
			{
				StartCoroutine (Down ());
				return;
			}

			if (Input.GetKey (KeyCode.RightArrow))
			{	
				StartCoroutine (Right ());
				return;
			}
			if (Input.GetKey (KeyCode.LeftArrow))
			{
				StartCoroutine (Left ());
				return;
			}
		}
	}
		

	IEnumerator Up()
	{	
		readyTurn = false;
		Vector3 pos = Vector3.zero;

		for (int i = 0; i < turningPoints.Length; i++)
		{
			Vector3 edgeCurrent = turningPoints [i].position;
			if (edgeCurrent.z > transform.position.z + 0.2 && edgeCurrent.y < transform.position.y - 0.2)
				pos = turningPoints [i].position;
		}

		for (int i = 0; i < 90; i+=speed)
		{
			transform.RotateAround (pos, new Vector3 (1f, 0, 0), fSpeed);
			yield return null;
		}

		TurnCompleted ();
	}

	IEnumerator Down()
	{
		readyTurn = false;
		Vector3 pos = Vector3.zero;

		for (int i = 0; i < turningPoints.Length; i++)
		{
			Vector3 edgeCurrent = turningPoints [i].position;
			if (edgeCurrent.z < transform.position.z - 0.2 && edgeCurrent.y < transform.position.y - 0.2) 
				pos = turningPoints[i].position;
		}

		for (int i = 0; i < 90; i+=speed)
		{
			transform.RotateAround (pos, new Vector3 (1f, 0, 0), -fSpeed);
			yield return null;
		}

		TurnCompleted ();
	}

	IEnumerator Right()
	{
		readyTurn = false;
		Vector3 pos = Vector3.zero;

		for (int i = 0; i < turningPoints.Length; i++)
		{
			Vector3 edgeCurrent = turningPoints [i].position;
			if (edgeCurrent.x > transform.position.x + 0.2 && edgeCurrent.y < transform.position.y - 0.2)
				pos = turningPoints [i].position;
		}

		for (int i = 0; i < 90; i+=speed)
		{
			transform.RotateAround (pos, new Vector3 (0, 0, 1f), -fSpeed);
			yield return null;
		}

		TurnCompleted ();
	}

	IEnumerator Left()
	{
		readyTurn = false;
		Vector3 pos = Vector3.zero;

		for (int i = 0; i < turningPoints.Length; i++)
		{
			Vector3 edgeCurrent = turningPoints [i].position;
			if (edgeCurrent.x < transform.position.x - 0.2 && edgeCurrent.y < transform.position.y - 0.2) 
				pos = turningPoints [i].position;
		}

		for (int i = 0; i < 90; i+=speed)
		{
			transform.RotateAround (pos, new Vector3 (0, 0, 1f), fSpeed);
			yield return null;
		}

		TurnCompleted ();
	}

	void TurnCompleted()
	{
		if (dying == false && !Win.winning)
		{
			audioSource.clip = moveSound;
			audioSource.Play ();
			readyTurn = true;
		}
	}
		

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Die" && !dying)
			StartCoroutine (Respawn());
	}

	public IEnumerator Respawn()
	{	
		audioSource.clip = dieSound;
		audioSource.Play ();
		dying = true;
		readyTurn = false;
		rb.isKinematic = false;

		yield return new WaitForSeconds (2f);

		rb.isKinematic = true;
		transform.rotation = startRot;
		transform.position = startPos;
		dying = false;
		readyTurn = true;
	}

	//Called from Win Script
	public void WinSound()
	{
		audioSource.clip = winSound;
		audioSource.Play ();
	}
}

