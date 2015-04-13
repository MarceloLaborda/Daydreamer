using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {


    public GameObject musicGroup;
    public GameObject sfxGroup;
    public Dictionary<string, AudioSource> music;
    public Dictionary<string, AudioSource> sfx;
    public float musicVolume = 0.6f;
    public float sfxVolume = 0.6f;

	// Use this for initialization
	void Start () {
        music = new Dictionary<string,AudioSource>();
        sfx = new Dictionary<string,AudioSource>();
        foreach (Transform sound in musicGroup.gameObject.transform)
        {
            sound.gameObject.GetComponent<AudioSource>().volume = musicVolume;
            music.Add(sound.name, sound.gameObject.GetComponent<AudioSource>());
        }
        foreach (Transform sound in sfxGroup.gameObject.transform)
        {
            sound.gameObject.GetComponent<AudioSource>().volume = sfxVolume;
            sfx.Add(sound.name, sound.gameObject.GetComponent<AudioSource>());
        }
        if (Application.loadedLevelName == "TitleScreen") {
            music["Theme_Track"].Play();
        }
	}


    public void MusicVolumeDown()
    {
        foreach (var musicStream in music.Values) {
            musicStream.volume -= 0.2f;
            musicVolume = musicStream.volume;
        }
    }

    public void SFXVolumeDown()
    {
        foreach (var sfxStream in sfx.Values) {
            sfxStream.volume -= 0.2f;
            sfxVolume = sfxStream.volume;
        }
    }

    public void MusicVolumeUp()
    {
        foreach (var musicStream in music.Values) {
            musicStream.volume += 0.2f;
            musicVolume = musicStream.volume;
        }
    }

    public void SFXVolumeUp()
    {
        foreach (var sfxStream in sfx.Values) {
            sfxStream.volume += 0.2f;
            sfxVolume = sfxStream.volume;
        }
    }

    public void StopAll() {
        foreach (var musicStream in music.Values) {
            musicStream.Stop();
        }
        foreach (var sfxStream in sfx.Values) {
            sfxStream.Stop();
        }
    }

    void OnLevelWasLoaded(int level) {
        if (level == 1) { //Intro
            music["Theme_Track"].Play();
        } else if (level == 2) { //Forest Transition
            music["Loading_Track"].Play();
        } else if (level == 3) { //Forest
            music["Birds_Ambient"].Play();
        } else if (level == 4) { //Desert Transition
            music["Loading_Track"].Play();
        } else if (level == 5) { //Desert
            music["Birds_Ambient"].Play();
        } else if (level == 6) { //Ice Transition
            music["Loading_Track"].Play();
        } else if (level == 7) { //Ice
            music["Birds_Ambient"].Play();
        } else if (level == 8) { //Lava Transition
            music["Loading_Track"].Play();
        } else if (level == 9) { //Lava
            music["Birds_Ambient"].Play();
        }
    }
	

}
