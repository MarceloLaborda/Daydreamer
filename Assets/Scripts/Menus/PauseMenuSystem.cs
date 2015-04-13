using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuSystem : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainTab;
    public GameObject optionsTab;
    public List<GameObject> options;
    public List<GameObject> soundOptions;
    public List<GameObject> musicSlider;
    public List<GameObject> sfxSlider;
    public int optionSelected;
    public int soundOptionSelected;
    public Color optionSelectedColor;
    public bool isActive;
    public bool inOptions; 
    private int numOptions;
    private int numSoundOptions;
    public SoundManager soundManager;
    private int musicLevel;
    private int sfxLevel;

    public InputManager inputManager;
    


    // Use this for initialization
    void Start()
    {
        isActive = false;
        inOptions = false;
        pauseMenu.SetActive(false);
        optionSelected = 0;
        numOptions = options.Count;
        numSoundOptions = soundOptions.Count;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        
        musicLevel = (int)(soundManager.musicVolume * 5.0f);
        for (int i = 0; i < musicLevel; i++) {
            musicSlider[i].SetActive(true);
        }

        sfxLevel = (int)(soundManager.sfxVolume * 5.0f);
        for (int i = 0; i < sfxLevel; i++)
        {
            sfxSlider[i].SetActive(true);
        }
    }

    public void ShowPauseMenu(){
        inputManager.isEnabled = false;
        options[optionSelected].transform.localScale = Vector3.one;
        options[optionSelected].GetComponent<SpriteRenderer>().color = Color.white;
        optionSelected = 0;
        options[optionSelected].transform.localScale = Vector3.one * 1.3f;
        options[optionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
        isActive = true;
        pauseMenu.SetActive(true);
    }

    public void SelectNextOption()
    {
        if (!inOptions)
        {
            if (optionSelected + 1 < numOptions)
            {
                options[optionSelected].transform.localScale = Vector3.one;
                options[optionSelected].GetComponent<SpriteRenderer>().color = Color.white;
                optionSelected++;
                options[optionSelected].transform.localScale = Vector3.one * 1.3f;
                options[optionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
            }
        }
        else
        {
            if (soundOptionSelected + 1 < numSoundOptions)
            {
                soundOptions[soundOptionSelected].transform.localScale = Vector3.one;
                soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = Color.white;
                soundOptionSelected++;
                soundOptions[soundOptionSelected].transform.localScale = Vector3.one * 1.3f;
                soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
            }
        }
    }

    public void SelectPreviousOption()
    {
        if (!inOptions)
        {
            if (optionSelected - 1 >= 0)
            {
                options[optionSelected].transform.localScale = Vector3.one;
                options[optionSelected].GetComponent<SpriteRenderer>().color = Color.white;
                optionSelected--;
                options[optionSelected].transform.localScale = Vector3.one * 1.3f;
                options[optionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
            }
        }
        else
        {
            if (soundOptionSelected - 1 >= 0)
            {
                soundOptions[soundOptionSelected].transform.localScale = Vector3.one;
                soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = Color.white;
                soundOptionSelected--;
                soundOptions[soundOptionSelected].transform.localScale = Vector3.one * 1.3f;
                soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
            }
        }
    }

    public void VolumeUp()
    {
        if (soundOptions[soundOptionSelected].name == "Music")
        {
            if (musicLevel < 5)
            {
                musicSlider[musicLevel].SetActive(true);
                musicLevel++;
                soundManager.MusicVolumeUp();
            }
        }
        else if (soundOptions[soundOptionSelected].name == "SFX")
        {
            if (sfxLevel < 5)
            {
                sfxSlider[sfxLevel].SetActive(true);
                sfxLevel++;
                soundManager.SFXVolumeUp();
                soundManager.sfx["Firefly_SoundEffect"].Play();
            }
        }
    }

    public void VolumeDown()
    {
        
        if (soundOptions[soundOptionSelected].name == "Music")
        {
            if (musicLevel > 0)
            {
                musicSlider[musicLevel - 1].SetActive(false);
                musicLevel--;
                soundManager.MusicVolumeDown();
            }
        }
        else if (soundOptions[soundOptionSelected].name == "SFX")
        {
            if (sfxLevel > 0)
            {
                sfxSlider[sfxLevel - 1].SetActive(false);
                sfxLevel--;
                soundManager.SFXVolumeDown();
                soundManager.sfx["Firefly_SoundEffect"].Play();
            }
        }

    }

    public void ActivateCurrentOption()
    {
        if (!inOptions)
        {
            if (options[optionSelected].name == "Resume")
            {
                Resume();
            }
            else if (options[optionSelected].name == "Options")
            {
                soundOptions[soundOptionSelected].transform.localScale = Vector3.one;
                soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = Color.white;
                soundOptionSelected = 0;
                soundOptions[soundOptionSelected].transform.localScale = Vector3.one * 1.3f;
                soundOptions[soundOptionSelected].GetComponent<SpriteRenderer>().color = optionSelectedColor;
                mainTab.SetActive(false);
                optionsTab.SetActive(true);
                inOptions = true;
            }
            else if (options[optionSelected].name == "Exit")
            {
                soundManager.StopAll();
                Destroy(soundManager.gameObject);
                Time.timeScale = 1;
                Application.LoadLevel("TitleScreen");
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isActive = false;
        pauseMenu.SetActive(false);
        inputManager.isEnabled = true;
    }

    public void BackOptions()
    {
        inOptions = false;
        optionsTab.SetActive(false);
        mainTab.SetActive(true);
    }
}