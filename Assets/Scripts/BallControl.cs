using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
	AudioSource audioPlayer;
	AudioSource audioBorder;
	
	void Start()
	{
		AudioSource[] allAudioSources = GetComponents<AudioSource>();
		audioPlayer = allAudioSources[0];
		audioBorder = allAudioSources[1];
		
		Launch();
	}
	
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			audioPlayer.pitch = Random.Range(0.4f, 2f);
			audioPlayer.Play();
			
			GetComponent<Animator>().Play("Collision");
		}
		else
		{
			audioBorder.pitch = Random.Range(0.8f, 1.2f);
			audioBorder.Play();
		}
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
