using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public Transform Player;
	public float CameraSmoothTime = 5f;
	Vector3 cameraVelocity = Vector3.zero;
	Vector3 playerPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update(){
	}

	void FixedUpdate () {

		if(Player != null){
			playerPosition  = Player.TransformPoint(new Vector3(0,0,-10));
			transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref cameraVelocity, CameraSmoothTime/10);
		}
		else
		{
			if(!GameManager.instance.getIsEndGame()){
				Player = GameManager.instance.GetPlayerObject().GetComponent<Transform>();
			}
		}
		
		//transform.Translate(offset);
		/*if(offset.x > 3f || offset.x < -3f){
			transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref cameraVelocity, CameraSmoothTime);
		}*/
		
	}
}