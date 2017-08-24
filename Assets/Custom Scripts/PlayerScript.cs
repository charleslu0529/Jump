using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour {

	public float JumpSpeed = 5f;
	public float MoveSpeed = 2f;
	public float MaxJumpMultiplier = 1.3f;
	public Color MaxJumpColor;
	bool isInAir = false;
	float timeHeld = 0f;
	bool maxJump = false;
	bool canWallJump = false;
	Color originalColor;
	Rigidbody2D rb;
	RaycastHit2D castLeft;
	RaycastHit2D castRight;
	// Use this for initialization
	void Start () {
		originalColor = GetComponent<SpriteRenderer>().color;
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if(!isInAir){
			if( Input.GetKeyUp("space") ){
				Jump();
				isInAir = true;
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

		rb.velocity = new Vector2(0,rb.velocity.y);
			
		rb.velocity = new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y,0f);

		castLeft = Physics2D.Raycast(transform.position + new Vector3(-0.55f,0,0), Vector3.left, 0.1f);
		castRight = Physics2D.Raycast(transform.position + new Vector3(0.55f,0,0), Vector3.right, 0.1f);
        if(castLeft.collider != null){
        	if(rb.velocity.x < 0){
        		rb.velocity = new Vector3(0, rb.velocity.y, 0f);
        	}
        }

        if(castRight.collider != null){
        	if(rb.velocity.x < 0){
        		rb.velocity = new Vector3(0, rb.velocity.y, 0f);
        	}
        }

		Debug.DrawRay(transform.position + new Vector3(-0.55f,0,0), Vector3.left,Color.green);

	}

	void OnCollisionEnter2D(Collision2D col2D){
		isInAir = false;
	}

	void OnCollisionStay2D(Collision2D col2D) {
        
        RaycastHit2D castDown = Physics2D.Raycast(transform.position + new Vector3(0,-0.5f,0), Vector3.down, 0.1f);
        if(castDown.collider == null){
        	rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/1.5f, 0f);
        	canWallJump = true;
        }else{
        	canWallJump = false;
        }
        
        Debug.Log(castDown);
    }

   

	void Jump(){
		if(maxJump){
			if(canWallJump){
				rb.velocity = new Vector3(1,1,0).normalized * JumpSpeed * MaxJumpMultiplier;
			}else{
				rb.velocity = transform.up * JumpSpeed * MaxJumpMultiplier;
			}
		}else{
			rb.velocity = transform.up * JumpSpeed;
		}
		
	}

}
