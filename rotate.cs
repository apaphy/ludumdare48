using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float speed = 1f;
    public float maxAngle = 30f;
	private void Start()
	{
		
	}
	void Update()
    {
        float rotateZ = Mathf.Sin(Time.deltaTime * speed);
        rotateZ *= maxAngle;
        transform.Rotate(new Vector3(0f, 0f, -rotateZ));
    }
}
