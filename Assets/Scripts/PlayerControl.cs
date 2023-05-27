using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public KeyCode moveUp;
    public KeyCode moveDown;

    public float speed = 10;
	
    void Update()
    {
		if (moveUp == KeyCode.None)
		{
			float move;

			float ballPos = GameObject.Find("Ball").GetComponent<Rigidbody2D>().transform.position.y;
			float playerPos = GetComponent<Rigidbody2D>().transform.position.y;

			if (ballPos > playerPos)
				move = 5;
			else if (ballPos < playerPos)
				move = -5;
			else
				move = 0;

			GetComponent<Rigidbody2D>().velocity = new Vector2(0, move);
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
