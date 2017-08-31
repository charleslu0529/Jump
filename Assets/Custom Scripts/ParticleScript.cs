using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {

	public enum ColorState {Normal, Green, Red}
	public ColorState myColor;
	// Use this for initialization
	void Start () {

		ParticleSystem ps = GetComponent<ParticleSystem>();
		var ma = ps.main;


		if(myColor == ColorState.Normal){
			ma.startColor = GameManager.instance.GetNormalColour();
		}else if(myColor == ColorState.Green){
			ma.startColor = GameManager.instance.GetGreenColour();
		}else if(myColor == ColorState.Red){
			ma.startColor = GameManager.instance.GetRedColour();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
