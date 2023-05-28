using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	public BoxCollider2D top;
	public BoxCollider2D bottom;
	public BoxCollider2D left;
	public BoxCollider2D right;
	
	public new Camera camera;
	
	public GUISkin skin;
	
	public Transform enemy;
	public Transform player;
	public Transform ball;

	static public int playerScore = 0;
	static public int enemyScore = 0;
	
	void Start()
	{
		Borders();
	}

	void Update()
	{
		Borders();
	}
	
	static public void CountScore(string border)
	{
		if (border == "Right")
			enemyScore += 1;
		
		else if (border == "Left")
			playerScore += 1;
    }
	
	void Borders()
	{
		top.size = new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width,0f,0f)).x * 2f * 2,1);
		top.offset = new Vector2(0f, camera.ScreenToWorldPoint(new Vector3(0f,Screen.height,0f)).y + 0.5f);

		bottom.size = new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x * 2f * 2, 1);
		bottom.offset = new Vector2(0f, -1 * camera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y - 0.5f);

		left.size = new Vector2(1f, camera.ScreenToWorldPoint(new Vector3(0f, Screen.height*2f, 0f)).y);
		left.offset = new Vector2(-1 * camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - 0.5f, 0f);

		right.size = new Vector2(1f, camera.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
		right.offset = new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 0.5f, 0f);
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		GUI.Label(new Rect(Screen.width / 2 - 280, 20, 100, 100), "Left: " + enemyScore);
		GUI.Label(new Rect(Screen.width / 2 + 170, 20, 100, 100), "Right: " + playerScore);

		if (GUI.Button(new Rect(Screen.width / 2 - 140, 55, 124, 32), "Restart"))
		{
			ScoreReset();
			PlayerReset();
			GameObject.Find("Ball").GetComponent<BallControl>().Launch();
		}

		if (GUI.Button(new Rect(Screen.width / 2, 55, 124, 32), "Exit"))
		{
			Application.Quit();
		}
	}
	
	void PlayerReset()
	{
		enemy.position = new Vector2(-1 * camera.ScreenToWorldPoint(new Vector3(Screen.width - 50f, 0f, 0f)).x, 0);
		player.position = new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width - 50f, 0f, 0f)).x, 0);
	}
	
	void ScoreReset()
	{
		enemyScore = 0;
		playerScore = 0;
	}
}
