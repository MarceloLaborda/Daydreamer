using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;


    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D character;
        private bool jump;

		private float currentTime = 0.0f; //running
		public float maxTime = 0.5f;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
            if (!jump) {
				// Read the jump input in Update so button presses aren't missed.
				jump = Input.GetButtonDown ("Xbox360ControllerAButton");
			}

        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool run = Input.GetKey(KeyCode.LeftShift);
			if (!run) {
				run = (Input.GetAxis ("Xbox360ControllerTriggers") > 0);
			}

            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float h = 0.0f;

			//When player moves
			if (Mathf.Abs (Input.GetAxis ("Xbox360ControllerLeftAnalogX")) > 0.5f || Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.0f) {

				h = 1.0f * Mathf.Sign (Input.GetAxis ("Xbox360ControllerLeftAnalogX"));
				if(Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.0f){
					h = Input.GetAxis ("Horizontal");  //Keyboard
				}

				currentTime -= Time.deltaTime;
				if (currentTime <= 0) {
					character.cameraFollow.SendMessage("ToggleSmoothTimeX", false);
					if(!run){
						character.cameraFollow.SendMessage("ToggleWalkOffsetX", true);
					}else{
						character.cameraFollow.SendMessage("ToggleRunOffsetX", true);
					}
			}
			} else {
				character.cameraFollow.SendMessage("ToggleSmoothTimeX", true);
				character.cameraFollow.SendMessage("ToggleWalkOffsetX", false);
				currentTime = maxTime;
			}
			// Pass all parameters to the character control script.

			/*
			if(Input.GetKey("right") || Input.GetKey("left")){
				timer -= Time.deltaTime;
				if (timer <= 0) {
					print ("10sec");
				}
			}
			if (Input.GetKeyUp ("right") || Input.GetKeyUp ("left")) {
				timer = 2.0f;
			}
			*/

            character.Move(h, run, jump);
            jump = false;
        }
    }
