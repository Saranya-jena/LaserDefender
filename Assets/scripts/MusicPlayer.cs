﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    static MusicPlayer instance;
    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;
    private AudioSource music;
	
	void Start(){
		
		if (instance != null && instance !=this) {
			Destroy (gameObject);

		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
		}

	}
     void OnLevelWasLoaded(int level)
    {
        music = GetComponent<AudioSource>();
        music.Stop();
        if (level==0)
        {
            music.clip = startClip;
        }
        if (level == 1)
        {
            music.clip = gameClip;
        }
        if(level==2)
        {
            music.clip = endClip;
        }

        music.loop = true;
        music.Play();
    }
   
}
