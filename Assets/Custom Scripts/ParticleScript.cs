using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {

	public enum ColorState {Normal, Green, Red}
	public ColorState myColor;
	// Use this for initialization
	void Start () {
		if(myColor == ColorState.Normal){
			GetComponent<ParticleSystem>().startColor = GameManager.instance.GetNormalColour();
		}else if(myColor == ColorState.Green){
			GetComponent<ParticleSystem>().startColor = GameManager.instance.GetGreenColour();
		}else if(myColor == ColorState.Red){
			GetComponent<ParticleSystem>().startColor = GameManager.instance.GetRedColour();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
