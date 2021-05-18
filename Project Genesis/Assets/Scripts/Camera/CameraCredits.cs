using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCredits : MonoBehaviour
{
    public CinemachineVirtualCamera cameraStart;
    public CinemachineVirtualCamera cameraEnd;
    public CinemachineVirtualCamera CreditsStart;
    private MainMenu menu;
    public Animator anim;
    private AudioSource source;
    public AudioClip CreditsClip;
    public AudioClip CreditsInitialClip;


    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<MainMenu>();
        source = GetComponent<AudioSource>();
        cameraStart.enabled = true;
        cameraEnd.enabled = false;
        CreditsStart.enabled = false;
        StartCoroutine(Credits());
    }

    private IEnumerator Credits()
    {
        yield return null;
        cameraEnd.enabled = true;
        //yield return new WaitForSeconds(10);
        cameraStart.enabled = false;
        yield return new WaitForSeconds(25);
        source.Stop();
        source.PlayOneShot(CreditsInitialClip);
        yield return new WaitForSeconds(10);
        anim.enabled = true;
        CreditsStart.enabled = true;
        cameraEnd.enabled = false;
        source.Stop();
        source.PlayOneShot(CreditsClip);
        yield return new WaitForSeconds(65);
        menu.GoToMainMenu();

    }
}
