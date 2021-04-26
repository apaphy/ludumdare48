using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionManager : MonoBehaviour
{
	public Animator transition;
	public float waitTime = 3f;
	public void Transition(string levelName)
	{
		StartCoroutine(LoadLevel(levelName));
	}
	IEnumerator LoadLevel(string levelName)
	{
		transition.SetTrigger("Start");
		yield return new WaitForSeconds(waitTime);

		SceneManager.LoadScene(levelName);
	}
}
