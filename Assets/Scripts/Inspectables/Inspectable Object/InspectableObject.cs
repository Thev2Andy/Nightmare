using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableObject : MonoBehaviour {

	public InspectableObjectData ObjectData;

	public void Inspect()
	{
		InspectUI.Instance.Inspect(ObjectData, this.gameObject);
	}

	// void Update()
	// {
	// 	if(Input.GetKeyDown(KeyCode.Y)) Inspect();
	// }
}
