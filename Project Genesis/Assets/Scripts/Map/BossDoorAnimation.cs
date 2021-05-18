using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorAnimation : MonoBehaviour
{
    public CloseDoorTrigger closeDoor;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.enabled = false;
        closeDoor.onTriggerPass.AddListener(CloseDoor);
    }

    public void CloseDoor()
    {
        anim.SetTrigger("Close");
    }

    public void enableAnimation()
    {
        anim.enabled = true;
    }
}
