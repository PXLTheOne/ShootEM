using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    Vector3 mousePos;
    public GameObject Player;
    public Camera mainCamera;
    private Vector3 newMousePos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        newMousePos = mainCamera.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseDown()
    {
        
        Player.transform.position = new Vector3(newMousePos.x, Player.transform.position.y, newMousePos.z);
        
    }
}
