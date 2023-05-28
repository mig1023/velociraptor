using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public KeyCode moveUp;
	public KeyCode moveDown;

	public float speed = 10;
	float inhibitor;
	
	void Update()
	{
		if (moveUp == KeyCode.None)
		{
			inhibitor -= Time.deltaTime;
			
			if (inhibitor > 0)
				return;
			
			float ballPos = GameObject.Find("Ball").GetComponent<Rigidbody2D>().transform.position.y;
			float playerPos = GetComponent<Rigidbody2D>().transform.position.y;
			float move = ballPos > playerPos ? 5 : -5;

			GetComponent<Rigidbody2D>().velocity = new Vector2(0, move);
			
			inhibitor = 0.2f;
		}
		else
		{
			if (Input.GetKey(moveUp))
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);

			else if (Input.GetKey(moveDown))
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * speed);

			else if (GetComponent<Rigidbody2D>().velocity.y != 0)
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		}
	}
}
