using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform FocusOn;
    public float CameraDistance = 13.2864f;
    public float zoomSpeed = 0.3f;
    private float tparam = 0;
    public bool inPause = false;
    private float initialDistance = 30;
    private Vector3 playerPosition;

    private void Start()
    {
        initialDistance = CameraDistance;
        tparam = 1;
    }

    private void FixedUpdate()
    {
        if(!inPause)
            playerPosition= new Vector3(FocusOn.position.x+1f, FocusOn.position.y+ 7f, transform.position.z);
        else
            playerPosition = new Vector3(FocusOn.position.x, FocusOn.position.y + 0.3f, transform.position.z);

        transform.position = playerPosition;
        //Transicion Lenta
        if (tparam < 1)
        {
            tparam += Time.deltaTime * zoomSpeed;
            GetComponent<UnityEngine.Camera>().orthographicSize = Mathf.Lerp(GetComponent<UnityEngine.Camera>().orthographicSize, ( CameraDistance), tparam);
        }
        //si cambio la distancia de la camara
        if(CameraDistance!= initialDistance)
        {
            tparam = 0;
        }
        initialDistance = CameraDistance;
    }

}
