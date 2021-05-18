using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{

    public GameObject target;
    public LayerMask playerLayer;
    public GameObject hitMarker;


    // Update is called once per frame
    void Update()
    {
        float ropeMaxCastDistance  = target.GetComponent<RopeHingeAnchor>().ropeMaxCastDistance;
        var hit = Physics2D.Raycast(hitMarker.transform.position, target.transform.position - hitMarker.transform.position, ropeMaxCastDistance, playerLayer);
        if (hit.collider != null)
        {
            hitMarker.SetActive(true);
        }
        else
        {
            hitMarker.SetActive(false);
        }

    }

}
