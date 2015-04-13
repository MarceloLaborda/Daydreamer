using UnityEngine;
using System.Collections;

public class MenuInput : MonoBehaviour
{

    private MenuSystem menuSystem;

    private float keyboardVerticalMovement;
    private float keyboardHorizontalMovement;
    private float gamepadHorizontalMovement;
    private float gamepadVerticalMovement;

    private bool inputActive;

    void Start(){
        inputActive = true;
        menuSystem = gameObject.GetComponent<MenuSystem>();
    }

    // Update is called once per frame
    void Update() {

        gamepadHorizontalMovement = Input.GetAxis("Gamepad_HorizontalMovement");
        gamepadVerticalMovement = Input.GetAxis("Gamepad_VerticalMovement");
        keyboardVerticalMovement = Input.GetAxis("Keyboard_VerticalMovement");
        keyboardHorizontalMovement = Input.GetAxis("Keyboard_HorizontalMovement");

		//Button commands from the keyboard
        if (gamepadVerticalMovement < -0.5f ||keyboardVerticalMovement == 1.0f) {
            if (inputActive) {
                menuSystem.SelectPreviousOption();
            }
            inputActive = false;
		}

        if (gamepadVerticalMovement > 0.5f || keyboardVerticalMovement == -1.0f) {
            if (inputActive) {
                menuSystem.SelectNextOption();
            }
            inputActive = false;
		}

        if (gamepadHorizontalMovement < -0.5f || keyboardHorizontalMovement == -1.0f) {
            if (inputActive) {
                if (menuSystem.inOptions == true) {
                    menuSystem.VolumeDown();
                }
            }
            inputActive = false;
        }

        if (gamepadHorizontalMovement > 0.5f || keyboardHorizontalMovement == 1.0f) {
            if (inputActive) {
                if (menuSystem.inOptions == true) {
                    menuSystem.VolumeUp();
                }
            }
            inputActive = false;
        }

        if (Input.GetButtonDown("Gamepad_Submit") || Input.GetButtonDown("Gamepad_Jump") || Input.GetButtonDown("Keyboard_Submit")) {
            menuSystem.ActivateCurrentOption();
		}

        if (Input.GetButtonDown("Gamepad_Cancel") || Input.GetButtonDown("Keyboard_Cancel")) {

            if (menuSystem.inOptions == true) {
                menuSystem.BackOptions();
            } else {
                Application.Quit();
            }

        }

        if (keyboardVerticalMovement == 0.0f && keyboardHorizontalMovement == 0.0f && (gamepadHorizontalMovement < 0.5f && gamepadHorizontalMovement >= 0.0f || gamepadHorizontalMovement > -0.5f && gamepadHorizontalMovement <= 0.0f) && (gamepadVerticalMovement < 0.5f && gamepadVerticalMovement >= 0.0f || gamepadVerticalMovement > -0.5f && gamepadVerticalMovement <= 0.0f)) {
            inputActive = true;
        }
	}
}