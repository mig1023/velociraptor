using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problems : MonoBehaviour
{
	private float rotate = 0f;
	private float move = 0f;
		
	void Update()
	{
		if (rotate > 0f)
		{
			rotate -= 0.05f;
			transform.Rotate(0f, 0f, 1f);
		}
		else if (move > 0f)
		{
			move -= 0.01f;
			float moving = 0;
			
			if (transform.position.y < -5.3f)
				moving = 0.05f;
			else if ((transform.position.y < 0f) && (transform.position.y > -4.8f))
				moving = -0.05f;
			else if ((transform.position.y > 0f) && (transform.position.y < 4.8f))
				moving = 0.05f;
			else if (transform.position.y > 5.3f)
				moving = -0.05f;
			
			transform.position = new Vector2(transform.position.x, transform.position.y + moving);
		}
	}
	
	public void OnCollisionEnter2D(Collision2D collision)
	{
		rotate += Random.Range(5f, 25f);
		move += Random.Range(1f, 5f);
	}
}
