using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	private CharacterController2D characterController2D;

    private float gamepadHorizontalMovement;
    private float keyboardHorizontalMovement;
    private float keyboardVerticalMovement;
    private float gamepadRun;

    public bool isEnabled;
    private int waitFrames;
    private bool canSlide;
    private bool isRunning;

	// Use this for initialization
	void Start () {
        canSlide = true;
        isRunning = false;
        isEnabled = true;
        waitFrames = 0;
		characterController2D = gameObject.GetComponent<CharacterController2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isEnabled) {
            if (waitFrames == 0) {
                //Button commands from the keyboard
                gamepadHorizontalMovement = Input.GetAxis("Gamepad_HorizontalMovement");
                keyboardHorizontalMovement = Input.GetAxis("Keyboard_HorizontalMovement");
                keyboardVerticalMovement = Input.GetAxis("Keyboard_VerticalMovement");
                gamepadRun = Input.GetAxis("Gamepad_Run");


                if (Input.GetButtonDown("Keyboard_Run")) {
                    isRunning = true;
                    characterController2D.Button_Run_press();
                }

                if (Input.GetButtonUp("Keyboard_Run")) {
                    isRunning = false;
                    characterController2D.Button_Run_release();
                }

                if (gamepadRun > 0.0f) {

                    characterController2D.Button_Run_press();
                }

                if (gamepadRun == 0.0f) {
                    if (!isRunning) {
                        characterController2D.Button_Run_release();
                    }
                }

                if (gamepadHorizontalMovement < -0.5f || keyboardHorizontalMovement == -1.0f) {
                    characterController2D.Button_Left_press();
                }

                if ((gamepadHorizontalMovement > -0.5f && gamepadHorizontalMovement <= 0.0f) && (keyboardHorizontalMovement > -1.0f && keyboardHorizontalMovement <= 0.0f)) {
                    characterController2D.Button_Left_release();
                }

                if (gamepadHorizontalMovement > 0.5f || keyboardHorizontalMovement == 1.0f) {
                    characterController2D.Button_Right_press();
                }

                if ((gamepadHorizontalMovement < 0.5f && gamepadHorizontalMovement >= 0.0f) && (keyboardHorizontalMovement < 1.0f && keyboardHorizontalMovement >= 0.0f)) {
                    characterController2D.Button_Right_release();
                }

                if (Input.GetButtonDown("Keyboard_Jump") || Input.GetButtonDown("Gamepad_Jump")) {
                    characterController2D.JustPressedSpace = 2;
                    characterController2D.Button_Jump_press();
                }
                if (Input.GetButtonUp("Keyboard_Jump") || Input.GetButtonUp("Gamepad_Jump")) {
                    characterController2D.Button_Jump_release();
                }

                if (Input.GetButtonDown("Gamepad_Slide") ) {
                    characterController2D.Button_Dash_press();
                }

                if (keyboardVerticalMovement == -1.0f) {
                    if (canSlide) {
                        characterController2D.Button_Dash_press();
                    }
                    canSlide = false;
                }

                if (keyboardVerticalMovement == 0.0f) {
                    canSlide = true;
                }

            } else {
                waitFrames--;
            }
        } else {
            waitFrames = 1;
        }

	}
}
