using UnityEngine;
using System.Collections;

public class PauseMenuInput : MonoBehaviour
{

    private PauseMenuSystem pauseMenuSystem;

    private float keyboardVerticalMovement;
    private float keyboardHorizontalMovement;
    private float gamepadHorizontalMovement;
    private float gamepadVerticalMovement;

    private bool inputActive;
    private bool pauseMenuActive;

    void Start()
    {
        pauseMenuActive = false;
        inputActive = true;
        pauseMenuSystem = gameObject.GetComponent<PauseMenuSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        gamepadHorizontalMovement = Input.GetAxis("Gamepad_HorizontalMovement");
        gamepadVerticalMovement = Input.GetAxis("Gamepad_VerticalMovement");
        keyboardVerticalMovement = Input.GetAxisRaw("Keyboard_VerticalMovement");
        keyboardHorizontalMovement = Input.GetAxisRaw("Keyboard_HorizontalMovement");

        if (pauseMenuActive) {

            //Button commands from the keyboard
            if (gamepadVerticalMovement < -0.5f || keyboardVerticalMovement == 1.0f) {
                if (inputActive) {
                    pauseMenuSystem.SelectPreviousOption();
                }
                inputActive = false;
            }

            if (gamepadVerticalMovement > 0.5f || keyboardVerticalMovement == -1.0f) {
                if (inputActive) {
                    pauseMenuSystem.SelectNextOption();
                }
                inputActive = false;
            }

            if (gamepadHorizontalMovement < -0.5f || keyboardHorizontalMovement == -1.0f) {
                if (inputActive) {
                    if (pauseMenuSystem.inOptions == true) {
                        pauseMenuSystem.VolumeDown();
                    }
                }
                inputActive = false;
            }

            if (gamepadHorizontalMovement > 0.5f || keyboardHorizontalMovement == 1.0f) {
                if (inputActive) {
                    if (pauseMenuSystem.inOptions == true) {
                        pauseMenuSystem.VolumeUp();
                    }
                }
                inputActive = false;
            }


            
            if (Input.GetButtonDown("Gamepad_Jump")) {
                if (pauseMenuSystem.isActive == true) {
                    if (pauseMenuSystem.options[pauseMenuSystem.optionSelected].name == "Resume") {
                        pauseMenuActive = false;
                    }
                    pauseMenuSystem.ActivateCurrentOption();
                }

            }
             

            if (Input.GetButtonDown("Keyboard_Cancel")) {
                if (pauseMenuSystem.isActive == true) {
                    if (pauseMenuSystem.inOptions == true) {
                        pauseMenuSystem.BackOptions();
                    } else {
                        pauseMenuSystem.Resume();
                        pauseMenuActive = false;
                    }
                }
            }

            if (Input.GetButtonDown("Gamepad_Cancel")) {
                if (pauseMenuSystem.isActive == true) {
                    if (pauseMenuSystem.inOptions == true) {
                        pauseMenuSystem.BackOptions();
                    } else {
                        pauseMenuSystem.Resume();
                        pauseMenuActive = false;
                    }
                }
            }

            if (Input.GetButtonDown("Gamepad_Submit") || Input.GetButtonDown("Keyboard_Submit")) {
                if (pauseMenuSystem.isActive == true) {
                    if (pauseMenuSystem.options[pauseMenuSystem.optionSelected].name == "Resume") {
                        pauseMenuActive = false;
                    }
                    pauseMenuSystem.ActivateCurrentOption();
                }
            }


            if (keyboardVerticalMovement == 0.0f && keyboardHorizontalMovement == 0.0f && (gamepadHorizontalMovement < 0.5f && gamepadHorizontalMovement >= 0.0f || gamepadHorizontalMovement > -0.5f && gamepadHorizontalMovement <= 0.0f) && (gamepadVerticalMovement < 0.5f && gamepadVerticalMovement >= 0.0f || gamepadVerticalMovement > -0.5f && gamepadVerticalMovement <= 0.0f)) {
                inputActive = true;
            }

        } else {

            if (Input.GetButtonDown("Gamepad_Submit") || Input.GetButtonDown("Keyboard_Submit") || Input.GetButtonDown("Keyboard_Cancel")) {
                pauseMenuActive = true;
                Time.timeScale = 0;
                pauseMenuSystem.ShowPauseMenu();
            }

        
        }

       


    }
}