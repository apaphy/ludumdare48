using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
	public static IEnumerator PlayGameDelay(string index, float delay)
	{
		FindObjectOfType<transitionManager>().GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(delay);
		SceneManager.LoadSceneAsync(index);
	}
	public static IEnumerator PlayGameDelay(int index, float delay)
	{
		FindObjectOfType<transitionManager>().GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(delay);
		SceneManager.LoadSceneAsync(index);
	}
	public static bool OnMinigameCompletion(int completionIndex)
	{
		if (SceneManager.GetActiveScene().buildIndex != completionIndex)
			return false; //we want to return to the main menu, so don't contiunue
		else return true; //we are continuing since the indices are the same
	}
}
