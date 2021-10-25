using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableObject : MonoBehaviour {

	public string ItemName;
	public string ItemType;
	public string ItemDescription;
	public Sprite ItemImage;
	public AudioClip ItemInspectSound;
	public bool Pickupable;

	public void Inspect()
	{
		InspectUI.Instance.Inspect(ItemName, ItemType, ItemDescription, ItemImage, ItemInspectSound, Pickupable, this.gameObject);
	}
}
