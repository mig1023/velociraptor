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
