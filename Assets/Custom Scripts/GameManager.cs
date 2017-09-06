using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public Color GreenStateColor;
	public Color NormalStateColor;
	public Color RedStateColot;
	public GameObject Player;
	public GameObject GreenWall;
	public GameObject RedWall;

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
		switch (Player.GetComponent<PlayerScript>().colorNumber)
		{
			case 1:
				Physics2D.IgnoreLayerCollision(10, 8, true);
				Physics2D.IgnoreLayerCollision(10, 9, false);
				break;
			case 2:
				Physics2D.IgnoreLayerCollision(10, 9, true);
				Physics2D.IgnoreLayerCollision(10, 8, false);
				break;
			default:
				Physics2D.IgnoreLayerCollision(10, 8, false);
				Physics2D.IgnoreLayerCollision(10, 9, false);
				break;
		}

		/*if(Player.GetComponent<PlayerScript>().myColor == GreenWall.GetComponent<WallScript>().myColor){

			Physics.IgnoreLayerCollision(10, 8, true);

			}else if(Player.GetComponent<PlayerScript>().myColor == RedWall.GetComponent<WallScript>().myColor){

				Physics.IgnoreLayerCollision(10, 9, true);

				}else{
					Physics.IgnoreLayerCollision(10, 8, false);
					Physics.IgnoreLayerCollision(10, 9, false);
				}*/
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

	public void endGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
