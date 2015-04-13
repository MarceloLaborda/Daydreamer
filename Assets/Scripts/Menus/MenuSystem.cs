using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSystem : MonoBehaviour {

    public GameObject mainTab;
    public GameObject optionsTab;
    public List<GameObject> options;
    public List<GameObject> soundOptions;
    public List<GameObject> musicSlider;
    public List<GameObject> sfxSlider;
    public int optionSelected;
    public int soundOptionSelected;
    public Color optionSelectedColor;
    private int numOptions;
    private int numSoundOptions;
    public bool inOptions;
    public SoundManager soundManager;
    private int musicLevel;
    private int sfxLevel;

	// Use this for initialization
	void Start () {
        optionSelected = 0;
        numOptions = options.Count;
        numSoundOptions = soundOptions.Count;
        options[optionSelected].transform.localScale = Vector3.one * 1.3f;
        options[optionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;

        musicLevel = (int)(soundManager.musicVolume * 5.0f);
        for (int i = 0; i < musicLevel; i++) {
            musicSlider[i].SetActive(true);
        }

        sfxLevel = (int)(soundManager.sfxVolume * 5.0f);
        for (int i = 0; i < sfxLevel; i++) {
            sfxSlider[i].SetActive(true);
        }
	}


    public void SelectNextOption() {
        if (!inOptions) {
            if (optionSelected + 1 < numOptions) {
                options[optionSelected].transform.localScale = Vector3.one;
                options[optionSelected].GetComponent<SpriteRenderer>().color = Color.white;
                optionSelected++;
                options[optionSelected].transform.localScale = Vector3.one * 1.3f;
                options[optionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
            }
        } else {
            if (soundOptionSelected + 1 < numSoundOptions) {
                soundOptions[soundOptionSelected].transform.localScale = Vector3.one;
                soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = Color.white;
                soundOptionSelected++;
                soundOptions[soundOptionSelected].transform.localScale = Vector3.one * 1.1f;
                soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
            }
        }
    }

    public void SelectPreviousOption() {
        if (!inOptions) {
            if (optionSelected - 1 >= 0) {
                options[optionSelected].transform.localScale = Vector3.one;
                options[optionSelected].GetComponent<SpriteRenderer>().color = Color.white;
                optionSelected--;
                options[optionSelected].transform.localScale = Vector3.one * 1.3f;
                options[optionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
            }
        }else{
            if (soundOptionSelected - 1 >= 0) {
                soundOptions[soundOptionSelected].transform.localScale = Vector3.one;
                soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = Color.white;
                soundOptionSelected--;
                soundOptions[soundOptionSelected].transform.localScale = Vector3.one * 1.1f;
                soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
            }
        }
    }

    public void BackOptions()
    {
        inOptions = false;
        optionsTab.SetActive(false);
        mainTab.SetActive(true);
    }

    public void VolumeUp() {
        if (soundOptions[soundOptionSelected].name == "Music") {
            if (musicLevel < 5) {
                musicSlider[musicLevel].SetActive(true);
                musicLevel++;
                soundManager.MusicVolumeUp();
            }
        } else if (soundOptions[soundOptionSelected].name == "SFX") {
            if (sfxLevel < 5) {
                sfxSlider[sfxLevel].SetActive(true);
                sfxLevel++;
                soundManager.SFXVolumeUp();
                soundManager.sfx["Firefly_SoundEffect"].Play();
            }
        }
    }

    public void VolumeDown() {

        if (soundOptions[soundOptionSelected].name == "Music") {
            if (musicLevel > 0) {
                musicSlider[musicLevel - 1].SetActive(false);
                musicLevel--;
                soundManager.MusicVolumeDown();
            }
        } else if (soundOptions[soundOptionSelected].name == "SFX") {
            if (sfxLevel > 0) {
                sfxSlider[sfxLevel - 1].SetActive(false);
                sfxLevel--;
                soundManager.SFXVolumeDown();
                soundManager.sfx["Firefly_SoundEffect"].Play();
            }
        }

    }

    public void ActivateCurrentOption() {
        if (options[optionSelected].name == "NewGame"){
            soundManager.StopAll();
            Application.LoadLevel("Intro");
        }else if(options[optionSelected].name == "Options") {
            soundOptions[soundOptionSelected].transform.localScale = Vector3.one;
            soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = Color.white;
            soundOptionSelected = 0;
            soundOptions[soundOptionSelected].transform.localScale = Vector3.one * 1.1f;
            soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
            mainTab.SetActive(false);
            optionsTab.SetActive(true);
            inOptions = true;
        }else if (options[optionSelected].name == "Quit") {
            Application.Quit();
        }
    }
}
