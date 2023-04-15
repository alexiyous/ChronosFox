using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public AudioSource mainMenuMusic, levelMusic;

    public AudioSource[] sfx;

    public void PlayMainMenuMusic()//method to play main menu music, set it on the test main menu script
    {
        levelMusic.Stop();//stop the level music from playing

        mainMenuMusic.Play();//play the main menu music
    }

    public void PlayLevelMusic()//method to play level music, set it on camera controller script
    {
        if (!levelMusic.isPlaying)//if level music is not playing
        {
            mainMenuMusic.Stop();//stop the main menu music from playing
            levelMusic.Play();//play the level music
        }
    }


    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void PlaySFXAdjusted(int sfxToAdjust)
    {
        sfx[sfxToAdjust].pitch = Random.Range(.8f, 1.2f);
        PlaySFX(sfxToAdjust);
    }
}