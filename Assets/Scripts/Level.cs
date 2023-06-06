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

	static public Score PlayerScore;
	static public Score EnemyScore;
	
	void Start()
	{
		Borders();
		
		PlayerScore = new Score
		{
			Points = 0,
			Games = 0,
			Sets = 0,
		};
		
		EnemyScore = new Score
		{
			Points = 0,
			Games = 0,
			Sets = 0,
		};
	}

	void Update()
	{
		Borders();
	}
	
	static public void Score(string border)
	{
		if (border == "Right")
			EnemyScore.Points += 1;
		
		else if (border == "Left")
			PlayerScore.Points += 1;
    }
	
	private Vector3 WorldPoint(Vector3 vector) =>
		camera.ScreenToWorldPoint(vector);
	
	void Borders()
	{
		top.size = new Vector2(WorldPoint(new Vector3(Screen.width,0f,0f)).x * 2f * 2,1);
		top.offset = new Vector2(0f, WorldPoint(new Vector3(0f,Screen.height,0f)).y + 0.5f);

		bottom.size = new Vector2(WorldPoint(new Vector3(Screen.width, 0f, 0f)).x * 2f * 2, 1);
		bottom.offset = new Vector2(0f, -1 * WorldPoint(new Vector3(0f, Screen.height, 0f)).y - 0.5f);

		left.size = new Vector2(1f, WorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
		left.offset = new Vector2(-1 * WorldPoint(new Vector3(Screen.width, 0f, 0f)).x - 2.5f, 0f);

		right.size = new Vector2(1f, WorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
		right.offset = new Vector2(WorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 2.5f, 0f);
	}
	
	void OnGUI()
	{
		string enemy = string.Format("Computer: {0}", EnemyScore.Points);
		string player = string.Format("Player: {0}", PlayerScore.Points);
		
		GUI.skin = skin;
		GUI.Label(new Rect(Screen.width / 2 - 280, 20, 100, 100), enemy);
		GUI.Label(new Rect(Screen.width / 2 + 170, 20, 100, 100), player);

		if (GUI.Button(new Rect(Screen.width / 2 - 140, 55, 124, 32), "Serve ball"))
		{
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
}
