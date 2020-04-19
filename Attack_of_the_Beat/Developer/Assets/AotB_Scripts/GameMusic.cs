using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(AudioSource))]
public class GameMusic : MonoBehaviour
{
    private GameInput input;

    AudioSource audioSourceMain;
    public float[] mainSamples;

    public AudioMixer masterMixer;
    public AudioClip currSong;

    public AudioClip[] songs;
    public int currSongNum;

    // Start is called before the first frame update
    void Start()
    {
        /*songs = new AudioClip[10];
        songs[0] = Resources.Load<AudioClip>("Music/believer");
        songs[1] = Resources.Load<AudioClip>("Music/bummer");
        songs[2] = Resources.Load<AudioClip>("Music/flight");
        songs[3] = Resources.Load<AudioClip>("Music/flight2");
        songs[4] = Resources.Load<AudioClip>("Music/furret");
        songs[5] = Resources.Load<AudioClip>("Music/gambit");
        songs[6] = Resources.Load<AudioClip>("Music/path");
        songs[7] = Resources.Load<AudioClip>("Music/sanctified");
        songs[8] = Resources.Load<AudioClip>("Music/tears");
        songs[9] = Resources.Load<AudioClip>("Music/turbo_killer");*/
        LoadMusic();

        input = gameObject.AddComponent<PlayerInput>();

        audioSourceMain = GetComponent<AudioSource>();
        mainSamples = new float[512];


        currSong = audioSourceMain.clip;
        audioSourceMain.Play();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        int choice = input.GetNumberRow();
        if (choice != -1)
        {
            ChangeMusic(choice);
        }
        //if (audioSourceMain.clip.name != currSong.name)
        //{
        //    ChangeMusic(2);
        //}

        GetSpectrumAudioSource();
    }

    void GetSpectrumAudioSource()
    {
        audioSourceMain.GetSpectrumData(mainSamples, 0, FFTWindow.Hamming);
    }

    void ChangeMusic(int num)
    {
        if (num == 0) currSongNum--;
        else if (num == 1) currSongNum++;

        if (currSongNum == songs.Length) currSongNum = 0;
        if (currSongNum < 0) currSongNum = songs.Length - 1;
        
        currSong = songs[currSongNum];

        audioSourceMain.Stop();
        audioSourceMain.clip = currSong;
        audioSourceMain.Play();
    }


    void LoadMusic()
    {
        songs = Resources.LoadAll<AudioClip>("Music");
         
    }


}
