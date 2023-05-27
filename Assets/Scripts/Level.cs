using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	public BoxCollider2D top;
    public BoxCollider2D bottom;
    public BoxCollider2D left;
    public BoxCollider2D right;
	
	public new Camera camera;
	
    void Start()
    {
        Wall();
    }

    void Update()
    {
        Wall();
    }
	
	void Wall()
    {
        top.size = new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width,0f,0f)).x * 2f * 2,1);
        top.offset = new Vector2(0f, camera.ScreenToWorldPoint(new Vector3(0f,Screen.height,0f)).y + 0.5f);

        bottom.size = new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x * 2f * 2, 1);
        bottom.offset = new Vector2(0f, -1 * camera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y - 0.5f);

        left.size = new Vector2(1f, camera.ScreenToWorldPoint(new Vector3(0f, Screen.height*2f, 0f)).y);
        left.offset = new Vector2(-1 * camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - 0.5f, 0f);

        right.size = new Vector2(1f, camera.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        right.offset = new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 0.5f, 0f);
    }
}
