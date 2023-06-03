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
			Debug.Log("NEED rotate");
			Debug.Log("rotate:" + rotate);
			
			rotate -= 0.05f;
			transform.Rotate(0f, 0f, 1f);
		}
	}
	
	public void OnCollisionEnter2D(Collision2D collision)
	{
		rotate += Random.Range(5f, 25f);
	}
}
