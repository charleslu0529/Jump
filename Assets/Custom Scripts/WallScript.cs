using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {


	public enum ColorState {Normal, Green, Red}
	public ColorState myColor;
	// Use this for initialization
	void Start () {
		if(myColor == ColorState.Normal){
			GetComponent<SpriteRenderer>().color = GameManager.instance.GetNormalColour();
		}else if(myColor == ColorState.Green){
			GetComponent<SpriteRenderer>().color = GameManager.instance.GetGreenColour();
		}else if(myColor == ColorState.Red){
			GetComponent<SpriteRenderer>().color = GameManager.instance.GetRedColour();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
