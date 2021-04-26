using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menumanager : MonoBehaviour
{
	private void Start()
	{
		audiomanager.instance.StopPlaying("level");
	}
	public void QuitGame()
	{
		StartCoroutine(QuitGameDelay());
	}
	public void PlayGame()
	{
		int progress = PlayerPrefs.GetInt("progress");
		if (progress == 0) progress = 1;
		StartCoroutine(PlayGameDelay(progress, 2f));
	}
	public void GoToMenu()
	{
		StartCoroutine(PlayGameDelay("menu", 2f));
	}
	IEnumerator PlayGameDelay(int index, float delay)
	{
		FindObjectOfType<transitionManager>().GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(delay);
		audiomanager.instance.play("level");
		SceneManager.LoadSceneAsync(index);
	}
	IEnumerator PlayGameDelay(string index, float delay)
	{
		FindObjectOfType<transitionManager>().GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(delay);
		audiomanager.instance.play("level");
		SceneManager.LoadSceneAsync(index);
	}
	IEnumerator QuitGameDelay()
	{
		FindObjectOfType<transitionManager>().GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(2f);
		Application.Quit();
	}
}
