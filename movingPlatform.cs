using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{

	/*[Range(-10f, 10f)] public float movespeed, dirX = 4f;
	[Range(1f, 10f)] public float length = 5f;
	//public bool debugLine = true;
	public bool ismoving = true;
	bool moveRight = true;

	private void Start()
	{
		if (!ismoving)
		{
			movespeed = 0f;
		}
	}
	void Update()
    {
		if (transform.position.x > length)
			moveRight = false;
		if (transform.position.x > -length)
			moveRight = true;
		if (moveRight)
		{
			transform.position = new Vector2(transform.position.x + movespeed * Time.deltaTime, transform.position.y);
		}
		else
		{
			transform.position = new Vector2(transform.position.x - movespeed * Time.deltaTime, transform.position.y);
		}
		/*if(Mathf.Abs(transform.position.x - endPos.x) < threshold || Mathf.Abs(transform.position.x - startPos.x) < threshold)
		{
			rb.velocity = -rb.velocity;
		}
	}*/
	[Range(-10f, 10f)] public float moveSpeed = 3f;
	[Range(1f, 10f)] public float length = 4f;
	public bool isMoving = true;
	public bool moveUp = false;
	private bool negativeSpeed = false;
	bool moveRight = true;
	bool moveLeft = true;
	//Vector3 start, end;

	private void Start()
	{
		if (!isMoving) moveSpeed = 0;
		if (moveSpeed < 0)
		{
			negativeSpeed = true;
		}
		//start = transform.position;
		//end = new Vector3(transform.position.x + length, 0, 0);
	}
	void Update()
	{
		if (!moveUp)
		{
			if (!negativeSpeed)
			{
				if (transform.localPosition.x > length)
					moveRight = false;
				if (transform.localPosition.x < -length)
					moveRight = true;
				if (moveRight)
					transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
				else
					transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
			}
			else
			{
				if (transform.localPosition.x > length)
					moveLeft = true;
				if (transform.localPosition.x < -length)
					moveLeft = false;
				if (moveLeft)
					transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
				else
					transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
			}			
		}
		else
		{
			if (!negativeSpeed)
			{
				if (transform.localPosition.y > length)
					moveRight = false;
				if (transform.localPosition.y < -length)
					moveRight = true;

				if (moveRight)
					transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
				else
					transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
			}
			else
			{
				if (transform.localPosition.y > length)
					moveRight = true;
				if (transform.localPosition.y < -length)
					moveRight = false;

				if (moveRight)
					transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
				else
					transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
			}
		}
		if (!isMoving)
		{
			if (!moveUp)
			{
				Debug.DrawLine(new Vector2(transform.position.x + length, transform.position.y), new Vector2(transform.position.x - length, transform.position.y), Color.blue);
			}
			else
			{
				Debug.DrawLine(new Vector2(transform.position.x, transform.position.y + length), new Vector2(transform.position.x, transform.position.y - length), Color.blue);
			}
		}
	}

}
