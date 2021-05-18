using Cinemachine;
using Cinemachine.PostFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public GameObject rPGInterface;
    public GameObject combatInterface;
    public CinemachineBrain camBrain;
    public GameObject combatCamera;
    private bool cameraUpdated = false;


    // Start is called before the first frame update
    void Start()
    {
        GameManagement.instance.disableRPGOnCombat = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.instance.onCombat)
        {
            player.SetActive(false);
            rPGInterface.SetActive(false);
            combatInterface.SetActive(true);
            combatCamera.SetActive(true);
            if(!cameraUpdated)
                UpdateProfileCombat();

        }
        else
        {
            cameraUpdated = false;
            player.SetActive(true);
            rPGInterface.SetActive(true);
            combatInterface.SetActive(false);
            combatCamera.SetActive(false);
        }

    }

    private void UpdateProfileCombat()
    {
        CinemachinePostProcessing cinemachinePost = combatCamera.GetComponent<CinemachinePostProcessing>();
        CinemachinePostProcessing cinemachinePostBrain = camBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachinePostProcessing>();
        cinemachinePost.m_Profile = cinemachinePostBrain.m_Profile;
        cameraUpdated = true;
    }

}
