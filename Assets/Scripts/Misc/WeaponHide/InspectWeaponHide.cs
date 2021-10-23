using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectWeaponHide : MonoBehaviour {
	
	public Camera WeaponCamera;

	private void Update() 
	{
		WeaponCamera.enabled = !InspectUI.Instance.CurrentlyInspecting;
	}
}
