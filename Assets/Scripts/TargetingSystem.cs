//
// Targeting System
// written by Vander 'imerso' Nunes, Jan/2018
//
// This system is designed to be as generic as possible,
// so it does not depend much from the outside (a list of enemies for example).
//
// It is meant just as a Unity3D programming example.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetingSystem : MonoBehaviour
{
	// Range of the automatic nearest enemy targeting
	public float range;

	// Lock HUD sprite
	public Texture2D lockHUD;

	// We will keep a cache of enemies
	// for performance reasons.
	List<GameObject> enemiesList;

	// Currently locked object
	GameObject lockedObject = null;
	Rect lockRect = new Rect();

	// TAB switcher
	int tabIndex = 0;
	Vector3 lastCameraPos = new Vector3();


	// Read input and take actions
	void Update ()
	{
		bool lockTarget = Input.GetButtonDown("Fire1");
		bool nextTarget = Input.GetButtonDown("NextTarget");

		if (lockTarget)
		{
			// unlock previously locked object
			lockedObject = null;

			// reset tab index
			tabIndex = 0;

			// shoot a ray from mouse position
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			// check if hit something
			if (Physics.Raycast(ray, out hit))
			{
				// lock object
				lockedObject = hit.transform.gameObject;
			}
		}
		else if (nextTarget)
		{
			// rebuild enemies list -- on a real game,
			// we could just have this list as a global info, instead
			// of rebuilding every time the player press TAB
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			enemiesList = new List<GameObject>(enemies);

			// sort all enemies by distance
			enemiesList.Sort(
				delegate (GameObject obj1, GameObject obj2)
				{
					float dst1 = Vector3.Distance(obj1.transform.position, Camera.main.transform.position);
					float dst2 = Vector3.Distance(obj2.transform.position, Camera.main.transform.position);
					return dst1.CompareTo(dst2);
				}
			);

			lockedObject = null;
			bool wrapped = false;

			// if camera moved since last time TAB was pressed,
			// reset tab index
			if (Camera.main.transform.position != lastCameraPos)
			{
				tabIndex = 0;
				lastCameraPos = Camera.main.transform.position;
			}

			// try to find one that is within range
			while (lockedObject == null)
			{
				// select the one by tabIndex
				lockedObject = enemiesList[tabIndex];

				Vector3 screenPos = Camera.main.WorldToViewportPoint(lockedObject.transform.position);
				bool isVisible = screenPos.x > 0 && screenPos.x < 1 && screenPos.y > 0 && screenPos.y < 1 && screenPos.z > 0;

				// check if it is inside the range
				float dist = Vector3.Distance(lockedObject.transform.position, Camera.main.transform.position);

				if (!isVisible || dist > range)
				{
					// enemy is invisible or outside range, can't select it
					// what we will do is, wrap to tab 0 again,
					// and if it also fails, bail out without selecting an enemy

					// deselect
					lockedObject = null;

					// if it wrapped already, give up
					if (wrapped)
					{
						break;
					}

					// if scanning too far already, reset tab index and force wrap
					if (dist > range)
					{
						tabIndex = -1;
						wrapped = true;
					}
				}

				// next tab index
				tabIndex++;
				if (tabIndex >= enemiesList.Count)
				{
					// wrap around
					tabIndex = 0;
					wrapped = true;
				}
			}
		}
	}


	// HUD rendering
	void OnGUI()
	{
		// show some debug info
		GUILayout.Label("WASD QE to move camera (SHIFT for faster move)");
		GUILayout.Label("Mouse RIGHT-HOLD & DRAG to rotate camera");
		GUILayout.Label("Mouse LEFT-CLICK to lock an enemy target");
		GUILayout.Label("TAB to cycle between nearest enemies");
		if (lockedObject != null) GUILayout.Label("Locked: " + lockedObject.transform.name);

		if (Event.current.type.Equals(EventType.Repaint))
		{
			// if there is a locked enemy,
			// draw a lock hud over it
			if (lockedObject != null)
			{
				Vector3 lockPos = Camera.main.WorldToScreenPoint(lockedObject.transform.position);
				if (lockPos.z > 0)
				{
					lockPos.y = Screen.height - lockPos.y;
					lockRect.Set(lockPos.x - 32, lockPos.y - 32, 64, 64);
					Graphics.DrawTexture(lockRect, lockHUD);

					lockRect.y -= 32;
					GUI.color = Color.yellow;
					GUI.Label(lockRect, lockedObject.transform.name);
				}
			}
		}
	}
}
