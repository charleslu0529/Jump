using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour {

	public float JumpSpeed = 5f;
	public float WallJumpSpeed = 10f;
	public float MoveSpeed = 2f;
	public float MaxJumpMultiplier = 1.3f;
	public Color MaxJumpColor;
	public enum ColorState {Normal, Green, Red}
	public ColorState myColor;
	public int colorNumber = 0;
	public LayerMask CastLayer;
	public LayerMask CastLayerRed;
	public LayerMask CastLayerGreen;
	public float wallJumptimer = 0.2f;
	bool isDead = false;
	bool hasWallJumped = false;
	bool isOnWallLeft = false;
	bool isOnWall = false;
	bool isInAir = false;
	float timeHeld = 0f;
	bool maxJump = false;
	bool canWallJump = false;
	float tempWallJumpTime;
	Color originalColor;
	Rigidbody2D rb;
	RaycastHit2D castLeft;
	RaycastHit2D castRight;
	RaycastHit2D castDown;
	RaycastHit2D castRightRed;
	RaycastHit2D castLeftRed;
	RaycastHit2D castDownRed;
	RaycastHit2D castLeftGreen;
	RaycastHit2D castRightGreen;
	RaycastHit2D castDownGreen;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		if(myColor == ColorState.Normal){
			GetComponent<SpriteRenderer>().color = GameManager.instance.GetNormalColour();
		}else if(myColor == ColorState.Green){
			GetComponent<SpriteRenderer>().color = GameManager.instance.GetGreenColour();
		}else if(myColor == ColorState.Red){
			GetComponent<SpriteRenderer>().color = GameManager.instance.GetRedColour();
		}

		originalColor = GetComponent<SpriteRenderer>().color;
		colorNumber = 0;
		tempWallJumpTime = wallJumptimer;
	}
	
	// Update is called once per frame
	void Update () {

		if(GameManager.instance.getIsRunning()){
			if(!canWallJump){
				if(!isInAir){
					if( Input.GetKeyUp("space") ){
						Jump();
						isInAir = true;
					}
				}
			}

			castLeftRed = Physics2D.Raycast(transform.position + new Vector3(-0.51f,0,0), Vector3.right, 0.1f, CastLayerRed);
			castRightRed = Physics2D.Raycast(transform.position + new Vector3(0.51f,0,0), Vector3.right, 0.1f, CastLayerRed);
			castDownRed = Physics2D.Raycast(transform.position + new Vector3(0,-0.51f,0), Vector3.right, 0.01f, CastLayerRed);
			castLeftGreen = Physics2D.Raycast(transform.position + new Vector3(-0.51f,0,0), Vector3.right, 0.1f, CastLayerGreen);
			castRightGreen = Physics2D.Raycast(transform.position + new Vector3(0.51f,0,0), Vector3.right, 0.1f, CastLayerGreen);
			castDownGreen = Physics2D.Raycast(transform.position + new Vector3(0,-0.51f,0), Vector3.right, 0.01f, CastLayerGreen);

			

			if( Input.GetKey("space") ){
				timeHeld += Time.deltaTime;
				if(timeHeld > 0.5f){
					GetComponent<ParticleSystem>().Play();
				}
				
			}else{
				timeHeld = 0;
				GetComponent<ParticleSystem>().Stop();
			}

			if(timeHeld > 1f){
				maxJump = true;
			}else{
				maxJump = false;
			}

			if(maxJump){
				GetComponent<SpriteRenderer>().color = MaxJumpColor;
			}else{
				GetComponent<SpriteRenderer>().color = originalColor;
			}

			if(Input.GetAxisRaw("Horizontal") == 0){
				if(!isInAir){
					rb.velocity = new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y,0f);
				}
			}else{
				if(hasWallJumped){
					if(tempWallJumpTime < 0){
						rb.velocity = new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y,0f);
					}
				}else{
					rb.velocity = new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y,0f);
				}
			}

			if(hasWallJumped){
				tempWallJumpTime -= Time.deltaTime;
			}
			

			castLeft = Physics2D.Raycast(transform.position + new Vector3(-0.51f,0,0), Vector3.left, 0.05f, CastLayer);
			castRight = Physics2D.Raycast(transform.position + new Vector3(0.51f,0,0), Vector3.right, 0.05f, CastLayer);
			castDown = Physics2D.Raycast(transform.position + new Vector3(0,-0.51f,0), Vector3.down, 0.05f, CastLayer);
	        if(castDown.collider == null){ 	// if player is off the ground
	        	isInAir = true;
	        	if(castLeft.collider != null){		//if there is a wall on the left in mid air
	        		isOnWall = true;
	        		isOnWallLeft = true;
	        		canWallJump = true;
	        		hasWallJumped = false;
	        	}else if(castRight.collider != null){	// if there is a wall on the right in mid air
	        		isOnWall = true;
	        		isOnWallLeft = false;
	        		canWallJump = true;
	        		hasWallJumped = false;
	        	}else{
	        		isOnWall = false;
	        		canWallJump = false;
	        	}
	        }else{
	        	canWallJump = false;
	        	isOnWall = false;
	        	isInAir = false;
	        }
	        if(castLeft.collider != null){
	        	if(Input.GetAxisRaw("Horizontal") < 0){
	        		rb.velocity = new Vector3(0, rb.velocity.y, 0f);
	        	}
	        }

	        if(castRight.collider != null){
	        	if(Input.GetAxisRaw("Horizontal") > 0){
	        		rb.velocity = new Vector3(0, rb.velocity.y, 0f);
	        	}
	        }

	        if(myColor == ColorState.Normal){
				originalColor = GameManager.instance.GetNormalColour();
				colorNumber = 0;
				if(castLeftGreen.collider != null || castLeftRed.collider != null){
					if(Input.GetAxisRaw("Horizontal")< 0){
	        			rb.velocity = new Vector3(0, rb.velocity.y, 0f);
		        	}
				}
				if(castRightGreen.collider != null || castRightRed.collider != null){
					if(Input.GetAxisRaw("Horizontal") > 0){
	        			rb.velocity = new Vector3(0, rb.velocity.y, 0f);
		        	}
				}
			}else if(myColor == ColorState.Green){
				originalColor = GameManager.instance.GetGreenColour();
				colorNumber = 1;
				if(castDownRed.collider == null && castDown.collider == null){
					isInAir = true;
		        	if(castLeftRed.collider != null || castLeft.collider != null){		//if there is a wall on the left in mid air
		        		isOnWall = true;
		        		isOnWallLeft = true;
		        		canWallJump = true;
		        		hasWallJumped = false;
		        	}else if(castRightRed.collider != null || castRight.collider != null){	// if there is a wall on the right in mid air
		        		isOnWall = true;
		        		isOnWallLeft = false;
		        		canWallJump = true;
		        		hasWallJumped = false;
		        	}else{
		        		isOnWall = false;
		        		canWallJump = false;
		        	}
				}else{
	        		canWallJump = false;
	        		isOnWall = false;
	        		isInAir = false;
	        	}
				if(castLeftRed.collider != null){
					if(Input.GetAxisRaw("Horizontal") < 0){
	        			rb.velocity = new Vector3(0, rb.velocity.y, 0f);
		        	}
				}
				if(castRightRed.collider != null){
					if(Input.GetAxisRaw("Horizontal") > 0){
	        			rb.velocity = new Vector3(0, rb.velocity.y, 0f);
		        	}
				}
				
			}else if(myColor == ColorState.Red){
				originalColor = GameManager.instance.GetRedColour();
				colorNumber = 2;
				if(castDownGreen.collider == null && castDown.collider == null){
					isInAir = true;
		        	if(castLeftGreen.collider != null || castLeft.collider != null){		//if there is a wall on the left in mid air
		        		isOnWall = true;
		        		isOnWallLeft = true;
		        		canWallJump = true;
		        		hasWallJumped = false;
		        	}else if(castRightGreen.collider != null || castRight.collider != null){	// if there is a wall on the right in mid air
		        		isOnWall = true;
		        		isOnWallLeft = false;
		        		canWallJump = true;
		        		hasWallJumped = false;
		        	}else{
		        		isOnWall = false;
		        		canWallJump = false;
		        	}
				}else{
	        		canWallJump = false;
	        		isOnWall = false;
	        		isInAir = false;
	        	}
				if(castLeftGreen.collider != null){
					if(Input.GetAxisRaw("Horizontal") < 0){
	        			rb.velocity = new Vector3(0, rb.velocity.y, 0f);
		        	}
				}
				if(castRightGreen.collider != null){
					if(Input.GetAxisRaw("Horizontal") > 0){
	        			rb.velocity = new Vector3(0, rb.velocity.y, 0f);
		        	}
				}
			}

	        if(isOnWall){
	        	if(isOnWallLeft){
	        		if(Input.GetAxisRaw("Horizontal") < 0){
	        			rb.velocity = new Vector3(rb.velocity.x,-0.5f,0f);
	        		}
	        		if(canWallJump){
	        			if( Input.GetKeyUp("space") ){
							rb.velocity = new Vector3(1f,1f,0f).normalized;
							hasWallJumped = true;
							tempWallJumpTime = wallJumptimer;
							if(maxJump){
								rb.velocity = rb.velocity * WallJumpSpeed * MaxJumpMultiplier;
							}else{
								rb.velocity = rb.velocity * WallJumpSpeed;
							}
						}
					}
	        	}else{
	        		if(Input.GetAxisRaw("Horizontal") > 0){
	        			rb.velocity = new Vector3(rb.velocity.x,-0.5f,0f);
	        		}
	        		if(canWallJump){
	        			if( Input.GetKeyUp("space") ){
							rb.velocity = new Vector3(-1f,1f,0f).normalized;
							hasWallJumped = true;
							tempWallJumpTime = wallJumptimer;
							if(maxJump){
								rb.velocity = rb.velocity * WallJumpSpeed * MaxJumpMultiplier;
							}else{
								rb.velocity = rb.velocity * WallJumpSpeed;
							}
						}
	        		}
	        	}
	        }
	    }else{
	    	rb.velocity = Vector3.zero;
	    }

	    if(GameManager.instance.getIsEndGame()){
	    	Destroy(gameObject);
	    }
	}

	void OnCollisionEnter2D(Collision2D col2D){
		if(col2D.gameObject.tag == "Spike" ){
			GameManager.instance.relocate();
			myColor = ColorState.Normal;
		}

		if(col2D.gameObject.tag == "Check Point"){
			GameManager.instance.updateCheckPoint(col2D.gameObject.transform.position);
		}

		if(col2D.gameObject.tag == "Chest"){
			GameManager.instance.endScene();
		}

		if(col2D.gameObject.tag == "Death"){
			GameManager.instance.endGame();
			GameManager.instance.endRun();
		}

		if(castLeft.collider != null){
        	rb.velocity = new Vector3(0, 0, 0f);
	    }

        if(castRight.collider != null){
        	rb.velocity = new Vector3(0, 0, 0f);
        }
	}

	void OnCollisionStay2D(Collision2D col2D) {
        
    }

    void OnParticleCollision(GameObject other){
		// Debug.Log(other);
    	if(other.GetComponent<ParticleSystem>()){
			if(myColor == ColorState.Green){
				if(other.GetComponent<ParticleSystem>().startColor.r == GameManager.instance.GetNormalColour().r ){
					// Debug.Log("NormalColorLoop: startColor.r = " + other.GetComponent<ParticleSystem>().startColor.r);
					if(other.GetComponent<ParticleSystem>().startColor.g == GameManager.instance.GetNormalColour().g){
						// Debug.Log("NormalColorLoop: startColor.g = " + other.GetComponent<ParticleSystem>().startColor.g);
						if(other.GetComponent<ParticleSystem>().startColor.b == GameManager.instance.GetNormalColour().b){
							// Debug.Log("NormalColorLoop: startColor.b = " + other.GetComponent<ParticleSystem>().startColor.b);
							if(other.GetComponent<ParticleSystem>().startColor.a == GameManager.instance.GetNormalColour().a){
								// Debug.Log("NormalColorLoop: startColor.a = " + other.GetComponent<ParticleSystem>().startColor.a);
								myColor = ColorState.Normal;
							}
						}
					}
				}else if(other.GetComponent<ParticleSystem>().startColor.r == GameManager.instance.GetRedColour().r ){
					if(other.GetComponent<ParticleSystem>().startColor.g == GameManager.instance.GetRedColour().g){
						if(other.GetComponent<ParticleSystem>().startColor.b == GameManager.instance.GetRedColour().b){
							if(other.GetComponent<ParticleSystem>().startColor.a == GameManager.instance.GetRedColour().a){
								myColor = ColorState.Red;
							}
						}
					}
				}
			}else if(myColor == ColorState.Red){
				if(other.GetComponent<ParticleSystem>().startColor.r == GameManager.instance.GetGreenColour().r ){
					// Debug.Log("GreenColorLoop: startColor.r = " + other.GetComponent<ParticleSystem>().startColor.r);
					if(other.GetComponent<ParticleSystem>().startColor.g == GameManager.instance.GetGreenColour().g){
						// Debug.Log("GreenColorLoop: startColor.g = " + other.GetComponent<ParticleSystem>().startColor.g);
						if(other.GetComponent<ParticleSystem>().startColor.b == GameManager.instance.GetGreenColour().b){
							// Debug.Log("GreenColorLoop: startColor.b = " + other.GetComponent<ParticleSystem>().startColor.b);
							if(other.GetComponent<ParticleSystem>().startColor.a == GameManager.instance.GetGreenColour().a){
								// Debug.Log("GreenColorLoop: startColor.a = " + other.GetComponent<ParticleSystem>().startColor.a);
								myColor = ColorState.Green;
							}
						}
					}
				}else if(other.GetComponent<ParticleSystem>().startColor.r == GameManager.instance.GetNormalColour().r ){
					// Debug.Log("NormalColorLoop: startColor.r = " + other.GetComponent<ParticleSystem>().startColor.r);
					if(other.GetComponent<ParticleSystem>().startColor.g == GameManager.instance.GetNormalColour().g){
						// Debug.Log("NormalColorLoop: startColor.g = " + other.GetComponent<ParticleSystem>().startColor.g);
						if(other.GetComponent<ParticleSystem>().startColor.b == GameManager.instance.GetNormalColour().b){
							// Debug.Log("NormalColorLoop: startColor.b = " + other.GetComponent<ParticleSystem>().startColor.b);
							if(other.GetComponent<ParticleSystem>().startColor.a == GameManager.instance.GetNormalColour().a){
								// Debug.Log("NormalColorLoop: startColor.a = " + other.GetComponent<ParticleSystem>().startColor.a);
								myColor = ColorState.Normal;
							}
						}
					}
				}
			}else if(myColor == ColorState.Normal){
				if(other.GetComponent<ParticleSystem>().startColor.r == GameManager.instance.GetGreenColour().r ){
					// Debug.Log("GreenColorLoop: startColor.r = " + other.GetComponent<ParticleSystem>().startColor.r);
					if(other.GetComponent<ParticleSystem>().startColor.g == GameManager.instance.GetGreenColour().g){
						// Debug.Log("GreenColorLoop: startColor.g = " + other.GetComponent<ParticleSystem>().startColor.g);
						if(other.GetComponent<ParticleSystem>().startColor.b == GameManager.instance.GetGreenColour().b){
							// Debug.Log("GreenColorLoop: startColor.b = " + other.GetComponent<ParticleSystem>().startColor.b);
							if(other.GetComponent<ParticleSystem>().startColor.a == GameManager.instance.GetGreenColour().a){
								// Debug.Log("GreenColorLoop: startColor.a = " + other.GetComponent<ParticleSystem>().startColor.a);
								myColor = ColorState.Green;
							}
						}
					}
				}else if(other.GetComponent<ParticleSystem>().startColor.r == GameManager.instance.GetRedColour().r ){
					if(other.GetComponent<ParticleSystem>().startColor.g == GameManager.instance.GetRedColour().g){
						if(other.GetComponent<ParticleSystem>().startColor.b == GameManager.instance.GetRedColour().b){
							if(other.GetComponent<ParticleSystem>().startColor.a == GameManager.instance.GetRedColour().a){
								myColor = ColorState.Red;
							}
						}
					}
				}
			}
		}

    }
    
	void Jump(){
		if(maxJump){
			rb.velocity = transform.up * JumpSpeed * MaxJumpMultiplier;
		}else{
			rb.velocity = transform.up * JumpSpeed;
		}
		
	}

}
