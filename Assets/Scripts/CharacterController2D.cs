using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharacterController2D : MonoBehaviour {
	
	//
	public float horizontalSpeed = 3500.0f;  //Maximum speed the player can move on the x axis
	public float jumpForce = 25.0f;         //Vertical force applied to the character when jumping
    public float runningSpeed = 1.0f;

	public bool doubleJumps; // Determines if player can double jump

	private bool isFacingRight; // Determines which way the character is currently facing

    public bool jumpsPerpendicularToGround = false; //Controls if player jumps perpendicular to the ground or always up in world coordinates


	//TO DO:
	[SerializeField] private LayerMask whatIsGround; // A mask establishes what is ground to the character


	//track the current events of the player character.
	private bool DJ_available;
	private float JumpForceCount;
	private bool isGrounded;

	public List<GameObject> GroundedToObjectsList;
	public List<GameObject> WalledToObjectsList;

	private float walljump_count;

	private bool isWallSliding; //Controls if the character is touching a wall. This is used for the wall sliding mechanic.

    private bool isDashing;
    private float dashingVelocity;

	private bool WallGripJustStarted;



	private bool NoNeedForSafeJump_bool = false;

	public int JustPressedSpace; //TO DO: was private
	private int FixStateTimer = 0; 
	private int OnCollisionStayCounter = 0;
	private int OnCollisionBugThreshold = 0;
	private int UpInTheAir_Counter;
	
	private float Ground_X_MIN;
	private float Ground_X_MAX;
	private float Ground_Y_MIN;
	private float Ground_Y_MAX;

	//This is to make sure character moves along moving platforms when standing on them.
	public GameObject PlatformRoot_PREFAB;
	private PlatformRoot PlatformRoot;
	public GameObject VisualRoot;

	//Checkpoint system
	public CheckpointSystem checkpointSystem;



	//Input
	private bool leftInputActive;
	private bool rightInputActive;
    private bool jumpInputActive;

	//Animation Controller and Sprite
	public GameObject characterSprite;  //Reference to the gameObject that contains the sprite renderer and animator for the character
    //private Vector3 characterSpriteInitialScale; //Stores the initial values for the local scaling of the character
    private Animator animatorController;  //Used to switch between the character's animations


	//Here are reference slots for Player Particle Emitters
	public ParticleSystem WallGripParticles;
	private int WallGripEmissionRate;
	public ParticleSystem JumpParticles_floor;
	public ParticleSystem JumpParticles_wall;
	public ParticleSystem JumpParticles_doublejump;
	public ParticleSystem Particles_DeathBoom;


	//AudioSources play the audios of the scene.
	public SoundManager soundManager;

    public Transform obstacleCheck; //When sliding, checks if the player is touching the ceiling of the obstacle

    private int Dashing_Counter;

    private bool isTouchingObstacle;



	
	// Use this for initialization
	void Start () {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        checkpointSystem = GameObject.Find("CheckpointSystem").GetComponent<CheckpointSystem>();
        if (checkpointSystem == null){
            Debug.Log("No Checkpoint System found on scene.");
        }

        animatorController = characterSprite.GetComponent<Animator>(); //Get reference to the animator
		GroundedToObjectsList = new List<GameObject> ();
		WalledToObjectsList = new List<GameObject> ();

		//These define when collision is detected as a floor and when as a wall.
		Ground_X_MIN = -0.75f;
		Ground_X_MAX = 0.75f;
		Ground_Y_MIN = 0.5f;
		Ground_Y_MAX = 1f;

		//RootOBJ makes sure that character together with moving platforms
		GameObject ROOTOBJ = Instantiate (PlatformRoot_PREFAB, Vector3.zero, Quaternion.identity) as GameObject;
		PlatformRoot = ROOTOBJ.GetComponent<PlatformRoot> ();
		ROOTOBJ.name = "_PlatformRoot";

		//Default values for WallGrip Particle Emitter.
		WallGripEmissionRate = 10;
		WallGripParticles.emissionRate = 0;

		//Player characters looks right in the start of the scene.
		isFacingRight = true;

        isDashing = false;

	}



	// Update is called once per frame
	void Update () {

		if (walljump_count >= 0) {
			walljump_count -= Time.deltaTime;		
		}

	}

    void FaceLeft(){
        isFacingRight = false;
        VisualRoot.transform.localScale = new Vector3(Mathf.Abs(VisualRoot.transform.localScale.x) * -1, VisualRoot.transform.localScale.y, VisualRoot.transform.localScale.z);
    }

    void FaceRight(){
        isFacingRight = true;
        VisualRoot.transform.localScale = new Vector3(Mathf.Abs(VisualRoot.transform.localScale.x), VisualRoot.transform.localScale.y, VisualRoot.transform.localScale.z);
    }



	void FixedUpdate(){
        //print(rigidbody2D.velocity.x);
		//Movement
        if (!isDashing) {
            if (leftInputActive == true && rightInputActive == false) { //Checks if player is pressing left
                if (isFacingRight == true && isWallSliding == false) { //Flips character if it is facing the opposite direction
                    FaceLeft();
                }

                rigidbody2D.AddForce(new Vector2(transform.right.x, transform.right.y) * -horizontalSpeed * (isGrounded || UpInTheAir_Counter < 8 ? runningSpeed : 1.0f) * Time.deltaTime);

            } else if (leftInputActive == false && rightInputActive == true) { //Checks if player is pressing right
                if (isFacingRight == false && isWallSliding == false) {
                    FaceRight();
                }

                rigidbody2D.AddForce(new Vector2(transform.right.x, transform.right.y) * horizontalSpeed * (isGrounded || UpInTheAir_Counter < 8 ? runningSpeed : 1.0f) * Time.deltaTime);

            }


            if (isGrounded == true && isWallSliding == false) { //Prevents character from sliding on slopes
                this.rigidbody2D.gravityScale = 0f; //The player is not affected by gravity when grounded and not wall sliding.
            } else {
                if (this.rigidbody2D.gravityScale != 1f) {
                    this.rigidbody2D.gravityScale = 1f;
                }
            }


            if (isGrounded == false && isWallSliding == true) { //Slowsdown fall if the character is wall sliding
                this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, Physics2D.gravity.y * 0.01f);
            }


            //----

            //If character is in the air. Rotate it so its up vector matches the world up vector after few frames.

            UpInTheAir_Counter += 1;
            if (UpInTheAir_Counter > 5) {
                if (isGrounded == false && isWallSliding == false) {
                    Vector2 RealDirectionV2 = new Vector2(this.transform.up.x, this.transform.up.y);
                    Vector2 WorldUpVec = new Vector2(0f, 1f);
                    float TorqueTo = Vector2.Angle(WorldUpVec, RealDirectionV2);
                    if (WorldUpVec.normalized.x > RealDirectionV2.normalized.x) {
                        TorqueTo = TorqueTo * (-1);
                    }
                    if (-WorldUpVec.normalized.y > RealDirectionV2.normalized.y) {
                        TorqueTo = TorqueTo * (-1);
                    }
                    this.rigidbody2D.AddTorque(TorqueTo * 400f * Time.deltaTime);
                }
            }


            //Lift player up if jump is happening.
            if (jumpInputActive == true && JumpForceCount > 0) {
                //rigidbody2D.AddForce(new Vector2(0f, jumpForce));
                this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, jumpForce);
                JumpForceCount -= 0.1f * Time.deltaTime;
            }


            //This if-statement makes sure character is not grounded to any platform when there is no collision detections. (In some cases OnCollisionExit messages might be lost. This makes sure that it will not cause a bug.)
            //START-------
            if (OnCollisionStayCounter == 0) {
                OnCollisionBugThreshold += 1;
            } else {
                OnCollisionStayCounter = 0;
            }

            if (OnCollisionBugThreshold > 4 && (isGrounded == true || isWallSliding == true)) {
                bool isBox = false;
                foreach (GameObject ground in GroundedToObjectsList) {
                    if (ground.tag == "Box") {
                        isBox = true;
                    }
                }
                if (!isBox) {
                    DJ_available = true;
                    isGrounded = false;
                    isWallSliding = false;
                    this.transform.parent = null;
                    GroundedToObjectsList.Clear();
                    WalledToObjectsList.Clear();
                    WallGripParticles.emissionRate = 0;
                    FixStateTimer = 0;
                    OnCollisionBugThreshold = 0;
                    OnCollisionStayCounter = 1;
                }
            }
            //--------END


            float AnimVelY = this.rigidbody2D.velocity.y;
            float AnimVelX = this.rigidbody2D.velocity.sqrMagnitude * 4f;

            if (JustPressedSpace > 0) {
                AnimVelX = 0f;
                JustPressedSpace -= 1;
            }

            if (jumpInputActive == false && isGrounded == true) {
                //-- Set to zero to get run animation instead of fall animation
                AnimVelY = 0f;
            }

            //Send variables to Animation Controller
            animatorController.SetFloat("HorizontalSpeed", AnimVelX);
            animatorController.SetFloat("VerticalSpeed", AnimVelY);
            animatorController.SetBool("Grounded", isGrounded);
            animatorController.SetBool("Walled", isWallSliding);
        
        } else {//Dashing
            //rigidbody2D.AddForce(new Vector2(dashingVelocity, 0f));
            
            isTouchingObstacle = Physics2D.OverlapCircle(obstacleCheck.position, .1f, 1 << LayerMask.NameToLayer ("Obstacle"));
            if (isGrounded && Dashing_Counter < 60) {
                animatorController.SetBool("Sliding", isDashing);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                this.rigidbody2D.velocity = new Vector2((isFacingRight ? Mathf.Abs(dashingVelocity) : -Mathf.Abs(dashingVelocity)), 0f);
                if (!isTouchingObstacle) {
                    dashingVelocity *= 0.98f;
                    Dashing_Counter += 1;
                }
            } else {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                Dashing_Counter = 0;
                isDashing = false;
                animatorController.SetBool("Sliding", isDashing);
            }

        }

	}






	void OnCollisionEnter2D(Collision2D coll) {

		if(coll.gameObject.CompareTag("Enemy")){
			return;
		}

		//This makes sure character doesn't slide from previous force when hitting platform. Unless player is holding Left or Right button.
		if (isGrounded == false && leftInputActive == false && rightInputActive == false) {
			this.rigidbody2D.velocity = new Vector2 (this.rigidbody2D.velocity.x * 0.25f, -0.01f);
            //this.rigidbody2D.velocity = new Vector2 (this.rigidbody2D.velocity.x * 0.0f, -0.01f);
		} else if (isGrounded == false) {
			this.rigidbody2D.velocity = new Vector2 (this.rigidbody2D.velocity.x, -0.01f);	
		}

		OnCollisionStayCounter += 1;
		OnCollisionBugThreshold = 0;
		UpInTheAir_Counter = 0;

		foreach (ContactPoint2D contact in coll.contacts) {

			//If character hits his head to the roof. Stop Jump Force.
			if (0.1f > contact.normal.y && ((contact.normal.x*contact.normal.x) < (0.85f*0.85f))) {
				JumpForceCount = 0f;
			}

			//If it wasn't the roof. Was it a ground perhaps?
			else if (contact.normal.x >= Ground_X_MIN && contact.normal.x <= Ground_X_MAX && contact.normal.y >= Ground_Y_MIN && contact.normal.y <= Ground_Y_MAX) {

				int CountHappenings = 0;
				foreach(GameObject GroundedObject in GroundedToObjectsList){
					if(contact.collider.gameObject.GetInstanceID() == GroundedObject.GetInstanceID()){
						CountHappenings += 1;
					}
				}

				//Is the platform already listed in GroundedObjects? If not Add it to the list.
				if(CountHappenings == 0){
					DJ_available = false;
					GroundedToObjectsList.Add(contact.collider.gameObject);
					//Move PlatformRoot to the new platform.
					this.transform.parent = null;
					PlatformRoot.transform.position = contact.collider.gameObject.transform.position;
					PlatformRoot.RootedTo = contact.collider.gameObject;
					this.transform.parent = PlatformRoot.transform;

					isGrounded = true;

					this.rigidbody2D.AddForce (contact.normal * (-300f));

					if(isWallSliding == true){
						WallGripParticles.emissionRate = 0;
						FixStateTimer = 0;
					}
				}

			//If it wasnt a roof or a ground it must have been wall. No need for Normal check anymore.
			}else{
				//Character must be faling downwards to grab the wall.
				if (this.rigidbody2D.velocity.y < 0f && isGrounded == false) {
					this.rigidbody2D.velocity = Vector2.zero;
					//is the Object already listed in WalledObjects?
					int CountHappenings = 0;
					foreach(GameObject WalledObject in WalledToObjectsList){
						if(contact.collider.gameObject.GetInstanceID() == WalledObject.GetInstanceID()){
							CountHappenings += 1;
						}
					}
					//if not. Lets list it.
					if(CountHappenings == 0){
                        //print (contact.collider.tag) ;
                        if (contact.collider.tag != "Box") {
                            DJ_available = false;
                            WalledToObjectsList.Add(contact.collider.gameObject);
                            this.transform.parent = null;
                            PlatformRoot.transform.position = contact.collider.gameObject.transform.position;
                            PlatformRoot.RootedTo = contact.collider.gameObject;
                            this.transform.parent = PlatformRoot.transform;

                            isWallSliding = true;

                            //Check that the player is facing to the right direction
                            if (contact.normal.x > 0) {
                                FaceRight();
                            } else {
                                FaceLeft();
                            }

                            //Start emiting smoke particles when touching the wall
                            WallGripParticles.emissionRate = WallGripEmissionRate;
                        }

					}
				}
			}
		}
	}



	void OnCollisionStay2D(Collision2D coll) {

        if (coll.gameObject.tag == "Box") {
            //print("isBox");
            /*foreach (ContactPoint2D contact in coll.contacts) {
                print(contact.point);
            }*/
            float contactPoint = coll.contacts[0].point.y - coll.gameObject.transform.position.y;
            if (contactPoint < 0.0f) {
                animatorController.SetBool("Pushing", true);
            }

        }

		OnCollisionStayCounter += 1;
		UpInTheAir_Counter = 0;

		//This is making sure that when character is colliding with something it is always registered.
		if (isGrounded == false && isWallSliding == false) {
			FixStateTimer += 1;
			if(FixStateTimer > 4){
				foreach (ContactPoint2D contact in coll.contacts) {


                    if (0.1f > contact.normal.y && ((contact.normal.x * contact.normal.x) < (0.85f * 0.85f))) {
                        JumpForceCount = 0f;
                    } else if (contact.normal.x >= Ground_X_MIN && contact.normal.x <= Ground_X_MAX && contact.normal.y >= Ground_Y_MIN && contact.normal.y <= Ground_Y_MAX) {
                        FixStateTimer = 0;
                        DJ_available = false;
                        GroundedToObjectsList.Add(contact.collider.gameObject);
                        isGrounded = true;
                    } else {
                        if (contact.collider.tag != "Box") {
                            if (this.rigidbody2D.velocity.y < 0f) {
                                FixStateTimer = 0;
                                DJ_available = false;
                                WalledToObjectsList.Add(contact.collider.gameObject);
                                isWallSliding = true;

                                this.transform.parent = null;
                                PlatformRoot.transform.position = contact.collider.gameObject.transform.position;
                                PlatformRoot.RootedTo = contact.collider.gameObject;
                                this.transform.parent = PlatformRoot.transform;

                                if (contact.normal.x > 0) {
                                    FaceRight();
                                } else {
                                    FaceLeft();
                                }

                                //Start emiting smoke particles when touching the wall
                                WallGripParticles.emissionRate = WallGripEmissionRate;
                            }
                        }
                    }
                 
				}
			}
		}


		//OnStay Ground Events:
		else if (isGrounded == true) {
			Vector2 StandDirection = Vector2.zero;
			foreach (ContactPoint2D contact in coll.contacts) {
				int CountHappenings = 0;
				foreach (GameObject GroundedObject in GroundedToObjectsList) {
					if (contact.collider.gameObject.GetInstanceID () == GroundedObject.GetInstanceID ()) {
						StandDirection += contact.normal;
						CountHappenings += 1;
					}
				}
				if (CountHappenings > 0) {

					StandDirection = StandDirection/CountHappenings;
					//This makes sure that character doesn't walk on the walls.
					if((StandDirection.x > Ground_X_MAX || StandDirection.x < Ground_X_MIN) && (StandDirection.y > Ground_Y_MAX || StandDirection.y < Ground_Y_MIN)){
						this.rigidbody2D.AddForce(StandDirection * 100f);
					}else{
						//this Rotates the character to allign with platform.
						Vector2 RealDirectionV2 = new Vector2(this.transform.up.x,this.transform.up.y);
						float TorqueTo = Vector2.Angle (StandDirection, RealDirectionV2);
						if (StandDirection.normalized.x > RealDirectionV2.normalized.x) {
							TorqueTo = TorqueTo * (-1);
						}
						if (-StandDirection.normalized.y > RealDirectionV2.normalized.y) {
							TorqueTo = TorqueTo * (-1);
						}
						this.rigidbody2D.AddTorque (TorqueTo * 1000f * Time.deltaTime);

						this.rigidbody2D.AddForce (StandDirection * (-300f));
					}
				}
			}


		//OnStay Wall Events:
		} else if (isWallSliding == true) {

			foreach (ContactPoint2D contact in coll.contacts) {
				Vector2 WallDirection = Vector2.zero;
				int CountHappenings = 0;
				foreach (GameObject WallObject in WalledToObjectsList) {
					if (contact.collider.gameObject.GetInstanceID () == WallObject.GetInstanceID ()) {
						WallDirection += contact.normal;
						CountHappenings += 1;
					}
				}
				
				if (CountHappenings > 0) {
					WallDirection = WallDirection/CountHappenings;
					if((WallDirection.x > Ground_X_MAX || WallDirection.x < Ground_X_MIN) && (WallDirection.y > Ground_Y_MAX || WallDirection.y < Ground_Y_MIN)){

						if((leftInputActive == false && isFacingRight == false) || (rightInputActive == false && isFacingRight == true)){
							this.rigidbody2D.AddForce (WallDirection * -100f);
						}
						//this Rotates the character to allign with the wall.
						Vector2 RealDirectionV2 = new Vector2(this.transform.up.x,this.transform.up.y);

						if(isFacingRight == false){
							RealDirectionV2 = RotateThisVector(RealDirectionV2,1.35f);
						}else{
							RealDirectionV2 = RotateThisVector(RealDirectionV2,-1.35f);
						}

						float TorqueTo = Vector2.Angle (WallDirection, RealDirectionV2);
						if (contact.normal.x > RealDirectionV2.normalized.x) {
							TorqueTo = TorqueTo * (-1);
						}
						if (-contact.normal.y > RealDirectionV2.normalized.y) {
							TorqueTo = TorqueTo * (-1);
						}
						this.rigidbody2D.AddTorque (TorqueTo * 450f * Time.deltaTime);
					}else{
						if((leftInputActive == false && isFacingRight == false) || (rightInputActive == false && isFacingRight == true)){
							this.rigidbody2D.AddForce (WallDirection * 100f);
						}
					}
				}
			}
		}
	}
		

	//Here we check if the player is jumping or moving away from the wall or ground.
	void OnCollisionExit2D(Collision2D coll) {
        animatorController.SetBool("Pushing", false);
		OnCollisionStayCounter = 0;
		foreach (ContactPoint2D contact in coll.contacts) {
			int CountHappenings = 0;
			int CountHappeningsWALL = 0;
			foreach(GameObject GroundedObject in GroundedToObjectsList){
				if(contact.collider.gameObject.GetInstanceID() == GroundedObject.GetInstanceID()){
					CountHappenings += 1;
				}
			}
			foreach(GameObject WalledObject in WalledToObjectsList){
				if(contact.collider.gameObject.GetInstanceID() == WalledObject.GetInstanceID()){
					CountHappeningsWALL += 1;
				}
			}

			//was the object one of the grounded to objects?
			if(CountHappenings > 0){
				GroundedToObjectsList.Remove(contact.collider.gameObject);
				if(GroundedToObjectsList.Count == 0){
					DJ_available = true;
					isGrounded = false;
					this.transform.parent = null;
					FixStateTimer = 0;
				}
			}

			//was the object one of the wall?
			if(CountHappeningsWALL > 0){
				WalledToObjectsList.Remove(contact.collider.gameObject);
				if(WalledToObjectsList.Count == 0){
					if(NoNeedForSafeJump_bool == false){
						//This makes the walljump a bit easier. Player is able to do the wall jump even few miliseconds after he let go of the wall.
						walljump_count = 0.16f;
					}
					NoNeedForSafeJump_bool = false;
					DJ_available = true;
					this.transform.parent = null;
					isWallSliding = false;
					WallGripParticles.emissionRate = 0;
					FixStateTimer = 0;
				}
			}
		}
	}


	public void CharacterDies(){
		Particles_DeathBoom.Emit (50);

        this.gameObject.transform.position = checkpointSystem.activeCheckpoint.transform.position;
		this.rigidbody2D.velocity = Vector2.zero;
	
	}

	public void CharacterKilledEnemy(){
		GroundedToObjectsList.Clear ();
		WalledToObjectsList.Clear ();
	}


	public void Button_Left_press(){
		leftInputActive = true;

        if (isDashing && isFacingRight && !isTouchingObstacle) {
            isDashing = false; //Breaks dashing animation when moving in the opposite direction to the dash
            animatorController.SetBool("Sliding", isDashing);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            Dashing_Counter = 0;
        }
	}

	public void Button_Left_release(){
		leftInputActive = false;
	}

	public void Button_Right_press(){
		rightInputActive = true;

        if (isDashing && !isFacingRight && !isTouchingObstacle) {
            isDashing = false; //Breaks dashing animation when moving in the opposite direction to the dash
            animatorController.SetBool("Sliding", isDashing);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            Dashing_Counter = 0;
        }
	}
		
	public void Button_Right_release(){
		rightInputActive = false;
	}

    public void Bounce() {
        //DJ_available = false;
        soundManager.sfx["Jump_SoundEffect"].Play();
        //JumpForceCount = 0.0f;
        if (!jumpsPerpendicularToGround) {
            transform.position += new Vector3(0, 0.1f, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, 0f) + new Vector2(VisualRoot.transform.up.x, VisualRoot.transform.up.y) * jumpForce;

        JumpParticles_floor.Emit(20);
    }

	public void Button_Jump_press(){

        if (!isTouchingObstacle) { //Prevents players from jumping when sliding under an obstacle

            isDashing = false; //Breaks dashing animation when jumping and restores collider
            animatorController.SetBool("Sliding", isDashing);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            Dashing_Counter = 0;

            jumpInputActive = true;

            //If you are on the ground. Do the Jump.
            if (isGrounded == true) {
                DJ_available = true;
                soundManager.sfx["Jump_SoundEffect"].Play();
                JumpForceCount = 0.02f;
                if (!jumpsPerpendicularToGround) {
                    transform.position += new Vector3(0, 0.1f, 0);
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, 0f) + new Vector2(VisualRoot.transform.up.x, VisualRoot.transform.up.y) * jumpForce;

                JumpParticles_floor.Emit(20);

                //If you are in the air and DoubleJump is available. Do it!
            } else if (doubleJumps == true && DJ_available == true && isWallSliding == false) {
                DJ_available = false;
                soundManager.sfx["Jump_SoundEffect"].Play();
                JumpForceCount = 0.02f;
                this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, jumpForce);
                JumpParticles_doublejump.Emit(10);
            }
        }
        

		//If you touch the wall or just let go. And are defenitly not in the ground. Do the Wall Jump!
		if ((isWallSliding == true || walljump_count > 0f) && isGrounded == false) {

			//This is to fix the bug where character was sometimes able to do double jump when leaving the wall.
			if(walljump_count <= 0f){
				NoNeedForSafeJump_bool = true;
			}

			DJ_available = true;
            soundManager.sfx["Jump_SoundEffect"].Play();
			isWallSliding = false;
			JumpForceCount = 0.02f;
			JumpParticles_wall.Emit(20);
			this.rigidbody2D.velocity = Vector2.zero;
			if(isFacingRight == false){
				this.rigidbody2D.AddForce (new Vector2 (-jumpForce*32f, 0f));
			}else{
				this.rigidbody2D.AddForce (new Vector2 (jumpForce*32f, 0f));
			}
		}

        

	}

	public void Button_Jump_release(){
		JumpForceCount = 0f;
		jumpInputActive = false;
	}

	public void Button_Dash_press(){
        if (isGrounded && !isDashing) {
            soundManager.sfx["Jump_SoundEffect"].Play();
            if (Mathf.Abs(rigidbody2D.velocity.x) < 5.0f) {
                dashingVelocity = Mathf.Sign(rigidbody2D.velocity.x) * 5.0f;
            } else {
                dashingVelocity = this.rigidbody2D.velocity.x;
            }

            isDashing = true;
        }
	}


    public void Button_Run_press() {
        runningSpeed = 1.4f;
    }

    public void Button_Run_release() {
        runningSpeed = 1.0f;
    }

	//This is a vector rotator. It can be used to rotate a Vector2 with an angle valua.
	private Vector3 RotateThisVector( this Vector2 v, float angle )
	{
		float sin = Mathf.Sin( angle );
		float cos = Mathf.Cos( angle );

		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (cos * ty) + (sin * tx);
		
		return v;
	}
	



}