using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectUI : MonoBehaviour {

	[Header("Item Display.")]
	public GameObject InspectUIObject;
	public Text ItemNameText;
	public Text ItemTypeText;
	public Image ItemImage;
	public Text ItemDescriptionText;

	[Header("Prompts.")]
	public Text PickupPrompt;

	[Header("Audio.")]
	public AudioSource CameraAudio;

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

	public void Inspect(InspectableObjectData ObjectToInspect, GameObject InspectableObjectDataHolder)
	{
		CurrentlyInspecting = true;

		Time.timeScale = 0f;

		InspectedObject = InspectableObjectDataHolder;
		if (InspectedObject != null)
		{
			InspectedObject.SetActive(false);
		}

		InspectUIObject.SetActive(true);
		ItemNameText.text = ObjectToInspect.ItemName;
		ItemTypeText.text = ObjectToInspect.ItemType;
		ItemImage.sprite = ObjectToInspect.ItemImage;
		ItemDescriptionText.text = ObjectToInspect.ItemDescription;

		PickupPrompt.gameObject.SetActive((ObjectToInspect.Pickupable ? true : false));

		if (ObjectToInspect.ItemInspectSound != null && CameraAudio != null) CameraAudio.PlayOneShot(ObjectToInspect.ItemInspectSound);
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
	}
}
