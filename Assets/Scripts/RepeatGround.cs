using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatGround : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 restartPos;
    private float repeatLength;

    void Start()
    {
        startPos = GameObject.Find("Ground 1").transform.position;
        restartPos = GameObject.Find("Ground 2").transform.position;
        repeatLength = GetComponent<BoxCollider>().size.z * 2;
    }

    void Update()
    {
        // Make ground move 
        if (transform.position.z < startPos.z - repeatLength)
        {
            transform.position = restartPos;
        }
    }
}
