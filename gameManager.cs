using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
	public float gameDelay = 1.5f;
	public GameObject dieEffect;
	public GameObject shrimpParticle;
	public GameObject outOfFuelText;
	private void Start()
	{
		PlayerPrefs.SetInt("progress", SceneManager.GetActiveScene().buildIndex);
		//audiomanager.instance.play("level");
	}
	public void OnGameLose()
	{
		audiomanager.instance.play("levelLose");
		outOfFuelText.SetActive(true);
		StartCoroutine(PlayGameDelay(SceneManager.GetActiveScene().buildIndex, gameDelay, 2f));
	}
	public void RestartGame()
	{
		StartCoroutine(PlayGameDelay(SceneManager.GetActiveScene().buildIndex, gameDelay, 0f));
	}
	public void OnGameWin()
	{
		audiomanager.instance.play("levelWin");
		StartCoroutine(PlayGameDelay(SceneManager.GetActiveScene().buildIndex + 1, gameDelay, 2f));
	}
	public void GoToMenu()
	{
		StartCoroutine(PlayGameDelay("menu", gameDelay));
	}
	public void PauseGame(float timescaled)
	{
		Time.timeScale = timescaled;
	}
	public IEnumerator PlayGameDelay(int index, float delay1, float delay2)
	{
		yield return new WaitForSeconds(delay2);
		FindObjectOfType<transitionManager>().GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(delay1);
		SceneManager.LoadSceneAsync(index);
	}
	public IEnumerator PlayGameDelay(string index, float delay)
	{
		FindObjectOfType<transitionManager>().GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(delay);
		SceneManager.LoadSceneAsync(index);
	}
}
