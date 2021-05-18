using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultSelectedButton : MonoBehaviour
{
    private Button btn;


    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        btn.Select();
    }

    

}
