using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaleTimeCinemachine : MonoBehaviour
{
    private Cinemachine.CinemachineBrain brainCam;

    private void Awake()
    {
        brainCam = GetComponent<Cinemachine.CinemachineBrain>();
    }

    void Start()
    {
        brainCam.m_IgnoreTimeScale = true;
    }
}
