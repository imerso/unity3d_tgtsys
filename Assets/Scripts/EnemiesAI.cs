//
// Enemies AI
// written by Vander 'imerso' Nunes, Jan/2018
//
// Dumb, placeholder enemies AI for system testing.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAI : MonoBehaviour
{
	// Cached Vector3
	Vector3 move = new Vector3();

	// Some randomized vars
	float speed;
	Vector3 freq;


	// Initialize
	void Start()
	{
		speed = Random.value * 0.05f;
		freq = new Vector3(0.1f + Random.value, 0.1f + Random.value, 0.1f + Random.value);
	}

	// Move enemies in some dumb way
	void Update ()
	{
		move.Set(speed * Mathf.Sin(Time.time * freq.x), speed * Mathf.Sin(Time.time * freq.y), speed * Mathf.Cos(Time.time * freq.z));
		transform.position += move;
	}
}
