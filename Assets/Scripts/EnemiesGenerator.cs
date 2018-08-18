//
// Enemies Generator
// written by Vander 'imerso' Nunes, Jan/2018
//
// This is a simple random "enemies" generator
// which places a few cubes around to test the targeting system.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
	public Material enemyMat;
	public Material friendMat;
	public Material neutralMat;


	void Start ()
	{
		for (int i = 0; i < 100; i++)
		{
			GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			newObj.AddComponent<EnemiesAI>();
			newObj.transform.position = Random.onUnitSphere * (20 + Random.value*20);
			newObj.transform.rotation = Quaternion.Euler(Random.insideUnitSphere*360);

			Renderer renderer = newObj.GetComponent<Renderer>();

			float rnd = Random.value;

			if (rnd <= 0.25f)
			{
				newObj.tag = "Enemy";
				renderer.sharedMaterial = enemyMat;
				newObj.name = "Enemy" + i;
			}
			else if (rnd <= 0.5f)
			{
				newObj.tag = "Friend";
				renderer.sharedMaterial = friendMat;
				newObj.name = "Friend" + i;
			}
			else
			{
				newObj.tag = "Neutral";
				renderer.sharedMaterial = neutralMat;
				newObj.name = "Neutral" + i;
			}
		}
	}
}
