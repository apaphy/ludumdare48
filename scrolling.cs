using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrolling : MonoBehaviour
{
    float length, startpos;
    public GameObject cam;
    public float scrolleffect;
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - scrolleffect));
        float distance = (cam.transform.position.x * scrolleffect);

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }

}
