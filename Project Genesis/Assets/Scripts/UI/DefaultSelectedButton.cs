﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultSelectedButton : MonoBehaviour
{
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
