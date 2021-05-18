using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public List<AudioClip> songs = new List<AudioClip>();
    private int index = 0;
    public bool stop;

    // Start is called before the first frame update
    void Start()
    {
        GameManagement.instance.audioManager = this;
        songs = Resources.LoadAll<AudioClip>("Audio/Songs/").ToList();
        PlayNextSong();
    }

    private void Update()
    {
        if (!musicSource.isPlaying && !stop) 
            PlayNextSong();
    }

    public void PlayNextSong()
    {
        index++;
        if (index >= songs.Count)
            index = 0;
        musicSource.clip = songs[index];
        musicSource.Play();
    }
}
