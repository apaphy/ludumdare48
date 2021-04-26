using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
	public door[] Door;
	private void Start()
	{
		Door = FindObjectsOfType<door>();
	}
	public void UnlockDoor()
	{
		audiomanager.instance.play("shrimpGet");
		foreach (var item in Door)
		{
			item.OnKeyGet(this);
		}
	}
}
