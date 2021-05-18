using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticePanel : MonoBehaviour
{
    private void OnEnable()
    {
        GameManagement.instance.Pause();
    }
    // Update is called once per frame
    void Update()
    {
        bool close = Input.anyKey;
        if (close && !Input.GetKey(KeyCode.I))
        {
            GameManagement.instance.Unpause();
            Destroy(gameObject);
        }
    }
}
