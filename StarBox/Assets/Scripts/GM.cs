using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	[SerializeField] GameObject[] levels;

	[SerializeField] GameObject startPanel;
	[SerializeField] GameObject continuePanel;
	[SerializeField] GameObject nextLevelPanel;
	[SerializeField] GameObject endPanel;
	[SerializeField] GameObject canvas;
	[SerializeField] Text levelText;
	[SerializeField] Text levelCompletedText;

	public static int currentLevel = 1;
	public static int numberOfLevels = 0;

	void Awake ()
	{
		for (int i = 0; i < levels.Length; i++)
		{
			levels [i].SetActive (false);
		}
		numberOfLevels = levels.Length;

		canvas.SetActive (true);
		startPanel.SetActive (true);
		nextLevelPanel.SetActive (false);
		endPanel.SetActive (false);
	}

	void Update()
	{
		if (startPanel.activeSelf && Input.GetKeyDown (KeyCode.Space))
			StartGame ();

		if (nextLevelPanel.activeSelf && Input.GetKeyDown (KeyCode.Space))
			StartNextLevel ();

		if (endPanel.activeSelf && Input.GetKeyDown (KeyCode.Space))
			RestartGame ();
	}


	public void StartGame()
	{
		continuePanel.SetActive (true);
	}

	public void ContinueToGame()
	{
		startPanel.SetActive (false);
		levels [0].SetActive (true);
		currentLevel = 1;

		levelText.text = "Level: " + currentLevel;
		levelCompletedText.text = "Level " + currentLevel + " Completed!";
	}
	
	public void StartNextLevel()
	{
		nextLevelPanel.SetActive (false);
		levels [currentLevel-1].SetActive (false);

		currentLevel++;
		levelText.text = "Level: " + currentLevel;
		levelCompletedText.text = "Level " + currentLevel + " Completed!";
		levels [currentLevel-1].SetActive (true);
	}

	public void RestartGame()
	{
		SceneManager.LoadScene (0);
	}
}
