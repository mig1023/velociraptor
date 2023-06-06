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

	public static Score PlayerScore;
	public static Score EnemyScore;
	
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
	
	public static void Score(string border)
	{
		GetScoreEntity(border, out Score change, out Score opponent);
		
		if (change.Points < 30)
		{
			change.Points += 15;
		}
		else if (change.Points < 40)
		{
			change.Points += 10;
		}
		else
		{
			change.Points = 0;
			change.Games += 1; 
			
			if ((change.Games >= 6) && (change.Games >= opponent.Games + 2))
			{
				change.Games = 0;
				change.Sets += 1;
			}
		}
    }
	
	private static void GetScoreEntity(string border, out Score change, out Score opponent)
	{
		if (border == "Right")
		{
			change = EnemyScore;
			opponent = PlayerScore;
		}
		else
		{
			change = PlayerScore;
			opponent = EnemyScore;
		}
	}
	
	private Vector3 GetWorldPoint(Vector3 vector) =>
		camera.ScreenToWorldPoint(vector);
	
	void Borders()
	{
		top.size = new Vector2(GetWorldPoint(new Vector3(Screen.width,0f,0f)).x * 2f * 2,1);
		top.offset = new Vector2(0f, GetWorldPoint(new Vector3(0f,Screen.height,0f)).y + 0.5f);

		bottom.size = new Vector2(GetWorldPoint(new Vector3(Screen.width, 0f, 0f)).x * 2f * 2, 1);
		bottom.offset = new Vector2(0f, -1 * GetWorldPoint(new Vector3(0f, Screen.height, 0f)).y - 0.5f);

		left.size = new Vector2(1f, GetWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
		left.offset = new Vector2(-1 * GetWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - 2.5f, 0f);

		right.size = new Vector2(1f, GetWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
		right.offset = new Vector2(GetWorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 2.5f, 0f);
	}
	
	private string GetOneScoreLine(string name, int score) =>
		score > 0 ? string.Format("{0}: {1}\n", name, score) : string.Empty;
	
	private string GetScoreLine(string name, Score score)
	{
		string points = GetOneScoreLine("points", score.Points);
		string games = GetOneScoreLine("games", score.Games);
		string sets = GetOneScoreLine("sets", score.Sets);
		
		return string.Format("{0}:\n{1}{2}{3}", name, sets, games, points);
	}
	
	void OnGUI()
	{
		string enemy = GetScoreLine("Computer", EnemyScore);
		string player = GetScoreLine("Player", PlayerScore);
		
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
