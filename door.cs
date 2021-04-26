using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class door : MonoBehaviour
{
	public bool goingLeft = false;
	public float length = 2f;
	public int iterations = 60;
	List<key> Keys = new List<key>();
	bool moving;
	Vector2 targetPos;
    void Start()
    {
        key[] _keys = FindObjectsOfType<key>();
		Keys = _keys.ToList();
		if (goingLeft)
			length = -length;
	}
    public void OnKeyGet(key Key)
	{
		int index = 0;
		foreach (key x in Keys)
		{
			if (x.Equals(Key))
				index = Keys.IndexOf(x);
		}
		Keys.RemoveAt(index);
		if (Keys.Count == 0)
			Open();
	}
    void Open()
	{
		Vector3 porb = transform.right * length;
		float targetX = transform.position.x + porb.x;
		float targetY = transform.position.y + porb.y;
		targetPos = new Vector2(targetX, targetY);
		StartCoroutine(MoveDoor(targetPos));
	}
	void ShowPath()
	{
		Vector3 porb = transform.right * length;
		float transformX = transform.position.x;
		float transformY = transform.position.y;
		//Debug.Log(porb);
		Debug.DrawLine(new Vector2(transformX + porb.x, transformY + porb.y), transform.position, Color.white);
	}
	private void Update()
	{
		ShowPath();
	}
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(targetPos, 0.1f);
	}
	IEnumerator MoveDoor(Vector2 target)
	{
		Vector2 pos = transform.position;
		Vector2 distance = target - pos;
		float incrementX = distance.x / iterations;
		float incrementY = distance.y / iterations;
		Vector2 increment = new Vector2(incrementX, incrementY);
		audiomanager.instance.play("doorOpen");
		for (int i = 0; i < iterations; i++)
		{
			pos += increment;
			transform.position = pos;
			yield return new WaitForSeconds(1 / iterations);
		}
	}
}
