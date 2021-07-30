using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteractor : MonoBehaviour {

	public float Range;
	public Text TooltipText;
	
	private void Update ()
	{
		RaycastHit hit;
		if (Physics.Raycast(this.transform.position, transform.forward, out hit, Range))
		{
			if (hit.collider.gameObject.GetComponent<InspectableObject>() && !InspectUI.Instance.CurrentlyInspecting)
			{
				TooltipText.gameObject.SetActive(true);
				TooltipText.text = "Press [Y] to inspect.";
				if (Input.GetKeyDown(KeyCode.Y))
				{
					hit.collider.gameObject.GetComponent<InspectableObject>().Inspect();
				}
			}else
			{
				TooltipText.gameObject.SetActive(false);
			}
		}else
		{
			TooltipText.gameObject.SetActive(false);
		}

		// TODO: Add support for all interabtable objects. (weapon pickups, ammo pickups, etc) For now we only have the inspectable objects.
		// TODO: Use layer masks to not include the player. For now, the player is on the ignore raycast layer, which limits the possible enemy attacks.
	}
}
