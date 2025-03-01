using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMuzzle : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.LookAt(transform.forward);
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    rb.AddTorque(transform.forward * 500, ForceMode.Acceleration);
        //}
    }
}