using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void StartGame()
	{
		SceneManager.LoadScene("Game");
		Time.timeScale = 1;
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void RestartGame()
	{
		SceneManager.LoadScene("Game");
		Time.timeScale = 1;
	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene("Menu");
	}

}
