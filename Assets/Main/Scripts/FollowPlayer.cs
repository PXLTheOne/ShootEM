using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Vector3 Yoffset = new Vector3(0, 5, 0);
    public Vector3 YRotationOffset = new Vector3(65, 0, 0);
    public bool isGameStarted = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            transform.position = GameObject.Find("Player").gameObject.transform.position + Yoffset;
            transform.rotation = GameObject.Find("Player").gameObject.transform.rotation * Quaternion.Euler(YRotationOffset);
        }
    }
}
