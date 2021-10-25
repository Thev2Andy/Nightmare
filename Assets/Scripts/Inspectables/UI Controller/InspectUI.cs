using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectUI : MonoBehaviour {

	[Header("Item Display.")]
	public GameObject InspectUIObject;
	public GameObject GameUIObject;
	public Text ItemNameText;
	public Text ItemTypeText;
	public Image ItemImageUI;
	public Text ItemDescriptionText;

	[Header("Prompts.")]
	public Text PickupPrompt;

	// Private & hidden variables:
	private GameObject InspectedObject;
	[HideInInspector] public bool CurrentlyInspecting;

	// Singleton:
	public static InspectUI Instance;
	void Awake()
	{
		if (Instance != this)
		{
			Destroy(Instance);
			Instance = this;
		}
	}

	public void Inspect(string ItemName, string ItemType, string ItemDescription, Sprite ItemImage, AudioClip ItemInspectSound, bool Pickupable, GameObject InspectableObjectDataHolder)
	{
		CurrentlyInspecting = true;

		Time.timeScale = 0f;

		InspectedObject = InspectableObjectDataHolder;
		if (InspectedObject != null)
		{
			InspectedObject.SetActive(false);
		}

		InspectUIObject.SetActive(true);
		ItemNameText.text = ItemName;
		ItemTypeText.text = ItemType;
		ItemImageUI.sprite = ItemImage;
		ItemDescriptionText.text = ItemDescription;

		PickupPrompt.gameObject.SetActive((Pickupable ? true : false));

		if (ItemInspectSound != null) Camera.main.GetComponent<AudioSource>()?.PlayOneShot(ItemInspectSound);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = 1f;
			InspectUIObject.SetActive(false);
			if (InspectedObject != null)
			{
				InspectedObject.SetActive(true);

				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			InspectedObject = null;
			CurrentlyInspecting = false;
		}

		if (CurrentlyInspecting)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		GameUIObject.SetActive(!InspectUIObject.activeInHierarchy);
	}
}
