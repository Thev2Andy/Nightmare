using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InspectableData", menuName = "ScriptableObjects/Object Data/Inspectable Object Data")]
public class InspectableObjectData : ScriptableObject {

	public string ItemName;
	public string ItemType;
	public string ItemDescription;
	public Sprite ItemImage;
	public AudioClip ItemInspectSound;
	public bool Pickupable;
}
