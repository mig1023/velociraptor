using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    void Start()
    {
        Launch();
    }

    void Update()
    {
		float move;

		float ballPos = GameObject.Find("Ball").GetComponent<Rigidbody2D>().transform.position.y;
		float playerPos = GameObject.Find("LeftPlayer").GetComponent<Rigidbody2D>().transform.position.y;

		if (ballPos > playerPos)
			move = 5;
		else if (ballPos < playerPos)
			move = -5;
		else
			move = 0;

		GameObject.Find("LeftPlayer").GetComponent<Rigidbody2D>().velocity = new Vector2(0, move);
    }
	
	public void Launch()
    {
        float random = Random.Range(0f, 1f);

        GameObject.Find("Ball").GetComponent<Rigidbody2D>().transform.position = new Vector2(0, 0);
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        if (random <= 0.5f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-250f, 120f * (0.5f - random)));
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(250f, 120f * (0.5f - random)));
        }
    }
}
