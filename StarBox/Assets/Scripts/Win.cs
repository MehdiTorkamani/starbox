using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

	[SerializeField] GameObject nextLevelPanel;
	[SerializeField] GameObject endPanel;

	public static bool winning = false;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Hole" && winning == false)
		{
			winning = true;
			StartCoroutine (ScaleDown());
		}
	}

	IEnumerator ScaleDown ()
	{
		GetComponentInParent<Player> ().WinSound ();
		GameObject player = transform.parent.gameObject;

		for (int i = 0; i < 40; i++)
		{
			player.transform.localScale = player.transform.localScale + new Vector3 (-1f, 0f, 0f) * 0.05f;
			player.transform.position = player.transform.position + new Vector3(0f, -1f ,0f) * 0.043f;
			yield return null;
		}

		yield return new WaitForSeconds (1f);

		if (GM.currentLevel == GM.numberOfLevels)
			endPanel.SetActive (true);
		else
			nextLevelPanel.SetActive (true);
		
		winning = false;
	}
}
