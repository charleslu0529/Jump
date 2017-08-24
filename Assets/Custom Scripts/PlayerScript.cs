using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float JumpSpeed = 5f;
	public float MoveSpeed = 2f;
	public Color MaxJumpColor;
	bool isInAir = false;
	float timeHeld = 0f;
	bool maxJump = false;
	Color originalColor;
	Rigidbody2D rb;
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
			

		//transform.Translate(Vector3.right* Time.deltaTime * MoveSpeed * Input.GetAxisRaw("Horizontal"));
		rb.velocity = new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y,0f);
	}

	void OnCollisionEnter2D(Collision2D col2D){
		isInAir = false;
	}

	void OnCollisionStay(Collision collisionInfo) {
		
	}

	void Jump(){
		if(maxJump){
			rb.velocity = transform.up * JumpSpeed * 1.2f;
		}else{
			rb.velocity = transform.up * JumpSpeed * 0.7f;
		}
		
	}

}
