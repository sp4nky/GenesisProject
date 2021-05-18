using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioSource ASMusic;

    public AudioClip deathClip;
    public AudioClip finalGameClip;
    public AudioClip welcomeClip;
    public AudioClip newFriendClip;
    public AudioClip Attack;
    public AudioClip shield;
    public AudioClip OpenTheDoor;
    /*
    public AudioClip BeNice;
    public AudioClip Controls;
    public AudioClip Elevator;
    public AudioClip FinalTest;
    public AudioClip FloorColapse;
    public AudioClip MyNameZenobia;
    public AudioClip OpenDoor;
    public AudioClip Spikes;
    public AudioClip TestCompleted;
    */


    private bool volumeChanged = false;

    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying && volumeChanged)
        {
            volumeChanged = false;
            ASMusic.volume = 0.5f;
            audioSource.pitch = 1;
        }
    }

    public void changeMusic(AudioClip clip)
    {
        ASMusic.Stop();
        ASMusic.PlayOneShot(clip);
    }

    public void playClip(AudioClip clip)
    {
        if (!audioSource.isPlaying)
        {
            ASMusic.volume = .1f;
            audioSource.PlayOneShot(clip);
            volumeChanged = true;
        }
    }

    public void playOpenTheDoor()
    {
        if (!audioSource.isPlaying)
        {
            ASMusic.volume = .1f;
            audioSource.PlayOneShot(OpenTheDoor);
            volumeChanged = true;
        }
    }

    public void playdeathClip()
    {
        if (!audioSource.isPlaying)
        {
            AudioClip clip = ASMusic.clip;
            ASMusic.Stop();
            ASMusic.PlayOneShot(deathClip);
        }
    }


    public void playfinalGameClip()
    {
        if (!audioSource.isPlaying)
        {
            ASMusic.volume = 0;
            audioSource.PlayOneShot(finalGameClip);
            volumeChanged = true;
        }
    }
    public void playwelcomeClip()
    {
        if (!audioSource.isPlaying)
        {
            ASMusic.volume = .1f;
            audioSource.PlayOneShot(welcomeClip);
            volumeChanged = true;
        }
    }
    public void playnewFriendClip()
    {
        if (!audioSource.isPlaying)
        {
            ASMusic.volume = .1f;
            audioSource.PlayOneShot(newFriendClip);
            volumeChanged = true;
        }
    }

    public void playShield()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(shield);
            volumeChanged = true;

        }
    }

    public void playAttack()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Attack);
            volumeChanged = true;

        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
