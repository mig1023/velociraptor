using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    void Start()
    {
        Launch();
    }
	
	public void Launch()
    {
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().transform.position = new Vector2(0, 0);
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

		float random = Random.Range(0f, 1f);
		float randomDirection = random <= 0.5f ? -250f : 250f;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(randomDirection, 120f * (0.5f - random)));
    }
}
