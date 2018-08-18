//
// Camera Control (Simple)
// written by Vander 'imerso' Nunes, Jan/2018
//
// Bare-bones WSAD/QE plus right mouse dragging for camera control
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraControl_Simple : MonoBehaviour
{
	// euler rotations
	Vector3 fpsRot = new Vector3(0,0,0);
	bool click = false;
	Vector3 oldMouse;


	// Use this for initialization
	void Start ()
	{
		fpsRot = transform.rotation.eulerAngles;
	}


	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}


		if (Input.GetMouseButtonDown(1))
		{
			click = true;
			oldMouse = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(1))
		{
			click = false;
		}

		if (click)
		{
			Vector3 mouse = Input.mousePosition;

			float mx = (mouse.x - oldMouse.x) * 0.1f;
			float my = (mouse.y - oldMouse.y) * 0.1f;

			fpsRot.x -= my;
			fpsRot.y += mx;
			fpsRot.x = Mathf.Min(85, Mathf.Max(-85, fpsRot.x));
			transform.eulerAngles = fpsRot;

			oldMouse = mouse;
		}

		float speed = 5;
		if (Input.GetKey (KeyCode.LeftShift))
		{
			speed *= 5;
		}

		speed *= Time.deltaTime;

		if (Input.GetKey (KeyCode.W))
		{
			transform.position += transform.forward*speed;
		}

		if (Input.GetKey (KeyCode.S))
		{
			transform.position -= transform.forward*speed;
		}

		if (Input.GetKey (KeyCode.A))
		{
			transform.position -= transform.right*speed;
		}

		if (Input.GetKey (KeyCode.D))
		{
			transform.position += transform.right*speed;
		}

		if (Input.GetKey(KeyCode.Q))
		{
			transform.position -= Vector3.up*speed;
		}

		if (Input.GetKey(KeyCode.E))
		{
			transform.position += Vector3.up*speed;
		}
	}
}
