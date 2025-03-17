using UnityEngine;
using UnityEngine.UI;

public class AlwaysFaceCamera : MonoBehaviour
{
    //Camera cam;
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
