using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteractor : MonoBehaviour {

	public LayerMask ObjectMask;
	public float Range;
	
	private void Update ()
	{
		RaycastHit Hit;
		if (Physics.Raycast(this.transform.position, transform.forward, out Hit, Range, ObjectMask))
		{
			if (Hit.collider.gameObject.GetComponent<InspectableObject>() && !InspectUI.Instance.CurrentlyInspecting)
			{
				MessageUIController.Instance.ShowPrompt("Press <color=lightBlue>E</color> to inspect.", 3f);
				if (Input.GetKeyDown(KeyCode.E))
				{
					Hit.collider.gameObject.GetComponent<InspectableObject>()?.Inspect();
				}
			}else
			{
				MessageUIController.Instance.ShowPrompt("", 0f);
			}
		}else
		{
			MessageUIController.Instance.ShowPrompt("", 0f);
		}

		// TODO: Add support for all interabtable objects. (weapon pickups, ammo pickups, etc) For now we only have the inspectable objects.
	}
}
