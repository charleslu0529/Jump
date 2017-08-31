using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public Color GreenStateColor;
	public Color NormalStateColor;
	public Color RedStateColot;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}else if(instance != this)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public Color GetGreenColour(){
		return GreenStateColor;
	}

	public Color GetNormalColour(){
		return NormalStateColor;
	}

	public Color GetRedColour(){
		return RedStateColot;
	}
}
