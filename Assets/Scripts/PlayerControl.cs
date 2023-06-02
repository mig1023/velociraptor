using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public KeyCode moveUp;
	public KeyCode moveDown;

	public float speed;
	float inhibitor;
	
	void Update()
	{
		if (moveUp == KeyCode.None)
		{
			inhibitor -= Time.deltaTime;
			
			if (inhibitor > 0)
				return;
			
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, ComputerMove());
			
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
	
	private float ComputerMove()
	{
		float ballPos = GameObject.Find("Ball").GetComponent<Rigidbody2D>().transform.position.y;
		float computerPos = GetComponent<Rigidbody2D>().transform.position.y;
		float direction = ballPos > computerPos ? 1 : -1;
		
		return ComputerSpeed(ballPos, computerPos) * direction;	
	}

	private float ComputerSpeed(float ballPos, float computerPos)
	{
		float diff = Mathf.Abs(computerPos - ballPos);
		
		if (diff > 0.7)
			return 8f;
		
		else if (diff > 0.5)
			return 5f;
		
		else if (diff > 0.3)
			return 2.5f;
		
		else if (diff > 0.1)
			return 1f;
		
		else
			return 0f;
	}
}
