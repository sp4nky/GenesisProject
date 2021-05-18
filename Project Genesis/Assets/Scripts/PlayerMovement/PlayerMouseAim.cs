using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseAim : MonoBehaviour
{
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        //Get mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        
        //Look to mouse position
        transform.up = mousePos - transform.position;

    }
}
