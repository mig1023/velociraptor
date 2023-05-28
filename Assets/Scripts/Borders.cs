using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            Level.CountScore(transform.name);
            GameObject.Find("Ball").GetComponent<BallControl>().Launch();
        }
    }
}
