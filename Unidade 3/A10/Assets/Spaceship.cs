using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{

	Rigidbody2D rb;
	float dirX;
	float dirY;
	float moveSpeed = 20f;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		dirX = Input.acceleration.x * moveSpeed;
		dirY = Input.acceleration.y * moveSpeed;
		transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), Mathf.Clamp(transform.position.y, -4f, 4f));
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector2(dirX, dirY);
	}

}
