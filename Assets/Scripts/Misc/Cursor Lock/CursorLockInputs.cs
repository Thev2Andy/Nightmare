using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLockInputs : MonoBehaviour {
	
	public KeyCode[] CursorLockInputKeys;

	private void Update ()
	{
		for (int i = 0; i < CursorLockInputKeys.Length; i++)
		{
			if (Input.GetKeyDown(CursorLockInputKeys[i]) && !InspectUI.Instance.CurrentlyInspecting)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}
}
