using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InspectableData", menuName = "ScriptableObjects/Object Data/Inspectable Object Data")]
public class InspectableObject : ScriptableObject {

	public string ItemName;
	public string ItemType;
	public string ItemDescription;
	public Sprite ItemImage;
	public KeyCode InspectKey = KeyCode.Y;
}
