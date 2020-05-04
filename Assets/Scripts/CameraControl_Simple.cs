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
		// escape quits app
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		// right mouse button rotates camera
		if (Input.GetMouseButtonDown(1))
		{
			// started dragging
			click = true;
			oldMouse = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(1))
		{
			// stopped dragging
			click = false;
		}

		// if right mouse button is clicked,
		// rotates camera with mouse movements
		if (click)
		{
			Vector3 mouse = Input.mousePosition;

			// scaled delta movement
			float mx = (mouse.x - oldMouse.x) * 0.1f;
			float my = (mouse.y - oldMouse.y) * 0.1f;

			// we use a fps-like camera, with euler rotations
			fpsRot.x -= my;
			fpsRot.y += mx;
			fpsRot.x = Mathf.Min(85, Mathf.Max(-85, fpsRot.x));
			transform.eulerAngles = fpsRot;

			oldMouse = mouse;
		}

		// default movement speed
		float speed = 5;

		// left-shift key for turbo speed
		if (Input.GetKey (KeyCode.LeftShift))
		{
			speed *= 5;
		}

		// scales speed with time elapsed
		speed *= Time.deltaTime;

		// forward move
		if (Input.GetKey (KeyCode.W))
		{
			transform.position += transform.forward*speed;
		}

		// backward move
		if (Input.GetKey (KeyCode.S))
		{
			transform.position -= transform.forward*speed;
		}

		// left strafe
		if (Input.GetKey (KeyCode.A))
		{
			transform.position -= transform.right*speed;
		}

		// right strafe
		if (Input.GetKey (KeyCode.D))
		{
			transform.position += transform.right*speed;
		}

		// moves down
		if (Input.GetKey(KeyCode.Q))
		{
			transform.position -= Vector3.up*speed;
		}

		// moves up
		if (Input.GetKey(KeyCode.E))
		{
			transform.position += Vector3.up*speed;
		}
	}
}
