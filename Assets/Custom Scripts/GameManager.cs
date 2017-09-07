using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public Color GreenStateColor;
	public Color NormalStateColor;
	public Color RedStateColot;
	public GameObject Player;
	public GameObject GreenWall;
	public GameObject RedWall;
	public GameObject Death;
	public AudioSource BGM;
	public Text CenterText;
	public float RestartTimer = 3f;
	public float endInstTimer = 7f;
	public float deathSpeed = 20f;
	bool isEndGame = false;
	bool hasSpawnedDeath = false;
	bool isEndScene = false;
	bool isPlayingMusic = false;
	bool isRunning = false;
	AudioSource bgm;
	AudioSource youAreDead;

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
		Time.timeScale = 0;
		AudioSource[] audios = GetComponents<AudioSource>();
		bgm = audios[0];
		youAreDead = audios[1];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey){
			if(!isEndGame){
				if(!isEndScene){
					isRunning = true;
				}
				
			}
		}

		if(isRunning){
			Time.timeScale = 1f;
			CenterText.text = "";
			if(!isPlayingMusic){
				isPlayingMusic = true;
				bgm.Play();
			}
		}

		if(Player != null){
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
		}
			

		if(isEndGame){
			RestartTimer -= Time.deltaTime;
			CenterText.text = "Restarting in: " + Mathf.Round(RestartTimer).ToString();
		}

		if(RestartTimer < 0){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}

		if(isEndScene){
			endInstTimer -= Time.deltaTime;
			
			if(endInstTimer < 0){
				if(!hasSpawnedDeath){
					CenterText.text = "";
					bgm.Play();
					GameObject deathObject = Instantiate(Death, Player.GetComponent<Transform>().position + Vector3.up * 10f , Quaternion.identity) as GameObject;
					hasSpawnedDeath = true;
					deathObject.GetComponent<Rigidbody2D>().velocity = Vector3.down * deathSpeed;
				}
			}
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
		
		isEndGame = true;
	}

	public bool getIsEndGame(){
		return isEndGame;
	}
	
	public void endRun(){
		isRunning = false;
	}

	public bool getIsRunning(){
		return isRunning;
	}

	public void endScene(){
		isRunning = false;
		bgm.Stop();
		isEndScene = true;
		youAreDead.Play();
		CenterText.text = "!";
	}
}
