using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingPlatform : MonoBehaviour
{
    [Range(10f, 50f)] public float rotSpeed = 10f;
	public float angle = 90f;
	public float waitTime = 2f;
	public float howSoonToInvoke = 0.1f;
	Quaternion dest, original;
	bool move = true;
	bool wait = false;
	private void Start()
	{
		original = transform.rotation;
		dest = Quaternion.AngleAxis(angle, Vector3.forward) * transform.rotation;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, dest, Time.deltaTime * rotSpeed);
		wait = true;
	}
	void Update()
    {
		if (transform.rotation == dest || transform.rotation == original)
		{
			if (wait)
			{
				StartCoroutine("ChangeDirection");	
			}	
		}

		if (move)
			transform.rotation = Quaternion.RotateTowards(transform.rotation, dest, Time.deltaTime * rotSpeed);
		else
			transform.rotation = Quaternion.RotateTowards(transform.rotation, original, Time.deltaTime * rotSpeed);			
	}
	IEnumerator ChangeDirection()
	{
		wait = false;
		yield return new WaitForSeconds(waitTime);
		move = !move;
		Invoke("SetWaitTrue", howSoonToInvoke);
	}
	void SetWaitTrue()
	{
		wait = true;
	}
}
