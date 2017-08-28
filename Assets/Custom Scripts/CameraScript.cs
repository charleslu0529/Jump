using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public GameObject Player;
	public float CameraSmoothTime = 5f;
	Vector3 offset;
	Vector3 cameraVelocity = Vector3.zero;
	Vector3 playerPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		offset = transform.position - new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
		playerPosition  = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
		transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref cameraVelocity, CameraSmoothTime);
		//transform.Translate(offset);
		/*if(offset.x > 3f || offset.x < -3f){
			transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref cameraVelocity, CameraSmoothTime);
		}*/
		
	}
}