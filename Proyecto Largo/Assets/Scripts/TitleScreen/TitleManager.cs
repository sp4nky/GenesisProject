using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private MainMenu menu;
    public TapForContinue firstScreen;
    public AudioSource audioSource;
    public AudioClip prologoClip;

    private void Awake()
    {
        menu = GetComponent<MainMenu>();
    }

    private void Start()
    {
        firstScreen.OnKeyPressed.RemoveAllListeners();
        firstScreen.OnKeyPressed.AddListener(PlayIntro);
    }

    public void PlayIntro()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = prologoClip;
        audioSource.Play();
        firstScreen.OnKeyPressed.RemoveAllListeners();
        firstScreen.OnKeyPressed.AddListener(GoToGame);
    }

    public void GoToGame()
    {
        menu.GoToMap();
    }

}
