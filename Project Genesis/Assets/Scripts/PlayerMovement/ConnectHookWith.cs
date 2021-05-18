using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectHookWith : MonoBehaviour
{
    public Camera cam;
    private DistanceJoint2D joint;
    private RaycastHit2D hit;
    public float distance = 20;
    public LayerMask mask;
    public Transform Rope;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            //Get mouse position
            Vector3 mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;


            //Get object to colision
            hit = Physics2D.Raycast(transform.position, mousePos - transform.position ,distance,mask);
            if(hit.collider!=null)
            {
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                joint.distance = Vector2.Distance(transform.position, hit.point);
                joint.enabled = true;
                transform.localScale =new  Vector3(1, distance,0);
            }
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
            joint.enabled = false;
        }
        
    }
}
