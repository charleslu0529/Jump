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
	bool isOnWallLeft = false;
	bool isOnWall = false;
	bool isInAir = false;
	float timeHeld = 0f;
	bool maxJump = false;
	bool canWallJump = false;
	Color originalColor;
	Rigidbody2D rb;
	RaycastHit2D castLeft;
	RaycastHit2D castRight;
	RaycastHit2D castDown;
	// Use this for initialization
	void Start () {
		originalColor = GetComponent<SpriteRenderer>().color;
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if(!canWallJump){
			if(!isInAir){
				if( Input.GetKeyUp("space") ){
					Jump();
					isInAir = true;
				}
			}
		}

		if( Input.GetKey("space") ){
			timeHeld += Time.deltaTime;
		}else{
			timeHeld = 0;
		}

		if(timeHeld > 1){
			maxJump = true;
		}else{
			maxJump = false;
		}

		if(maxJump){
			GetComponent<SpriteRenderer>().color = MaxJumpColor;
		}else{
			GetComponent<SpriteRenderer>().color = originalColor;
		}

		//rb.velocity = new Vector2(0,rb.velocity.y);
		if(Input.GetAxisRaw("Horizontal") == 0){
			if(isInAir){
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y,0f);
			}else{
				rb.velocity = new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y,0f);
			}
			
		}else{
			rb.velocity = new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y,0f);
		}
		

		castLeft = Physics2D.Raycast(transform.position + new Vector3(-0.51f,0,0), Vector3.left, 0.01f);
		castRight = Physics2D.Raycast(transform.position + new Vector3(0.51f,0,0), Vector3.right, 0.01f);
		castDown = Physics2D.Raycast(transform.position + new Vector3(0,-0.51f,0), Vector3.down, 0.1f);
        if(castDown.collider == null){ 	// if player is off the ground
        	isInAir = true;
        	if(castLeft.collider != null){		//if there is a wall on the left in mid air
        		isOnWall = true;
        		isOnWallLeft = true;
        		canWallJump = true;
        		Debug.Log("On Wall Left");
        	}else if(castRight.collider != null){	// if there is a wall on the right in mid air
        		isOnWall = true;
        		isOnWallLeft = false;
        		canWallJump = true;
        		Debug.Log("On Wall Right");
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
        	if(rb.velocity.x < 0){
        		rb.velocity = new Vector3(0, rb.velocity.y, 0f);
        	}
        }

        if(castRight.collider != null){
        	if(rb.velocity.x > 0){
        		rb.velocity = new Vector3(0, rb.velocity.y, 0f);
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
						if(maxJump){
							rb.velocity = rb.velocity * WallJumpSpeed * MaxJumpMultiplier;
						}else{
							rb.velocity = rb.velocity * WallJumpSpeed;
						}
					}
        		}
        	}
        }

		Debug.DrawRay(transform.position + new Vector3(-0.52f,0,0), Vector3.left,Color.green);
		Debug.DrawRay(transform.position + new Vector3(0.52f,0,0), Vector3.right,Color.green);
		Debug.DrawRay(transform.position + new Vector3(0,-0.52f,0), Vector3.down,Color.green);

	}

	void OnCollisionEnter2D(Collision2D col2D){
	}

	void OnCollisionStay2D(Collision2D col2D) {
        
    }

   

	void Jump(){
		if(maxJump){
			rb.velocity = transform.up * JumpSpeed * MaxJumpMultiplier;
		}else{
			rb.velocity = transform.up * JumpSpeed;
		}
		
	}

}
