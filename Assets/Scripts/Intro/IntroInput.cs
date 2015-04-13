using UnityEngine;
using System.Collections;

public class IntroInput : MonoBehaviour {


    private IntroManager introManager;

    private bool canGoToNextSlide;

    void Start()
    {
        canGoToNextSlide = true;
        introManager = gameObject.GetComponent<IntroManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Gamepad_Jump") || Input.GetButtonDown("Keyboard_Submit")) {
            if (canGoToNextSlide) {
                introManager.NextTake();
            }
            canGoToNextSlide = false;
        }

        if (Input.GetButtonUp("Gamepad_Jump") || Input.GetButtonUp("Keyboard_Submit")) {
            canGoToNextSlide = true;
        }

        if (Input.GetButtonDown("Gamepad_Submit") || Input.GetButtonDown("Keyboard_Cancel")) {
            introManager.SkipIntro();
        }

    }
}
