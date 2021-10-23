using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [Header("Pistol")]
    public GameObject PistolObject;
    public bool HasPistol;

    [Header("SMG")]
    public GameObject SMGObject;
    public bool HasSMG;

    [Header("Flashlight")]
    public GameObject FlashlightObject;
    public bool HasFlashlight;

    [HideInInspector] public int CurrentSelection {get; private set;}

    private void Awake()
    {
        HasPistol = true;
        ChangeWeapon();
    }

    // Update is called once per frame
    private void Update()
    {
        int LastFrameSelection = CurrentSelection;

        if(Input.mouseScrollDelta.y > 0)
        {
            CurrentSelection++;
            if(CurrentSelection > 2) CurrentSelection = 0;
        }

        if(Input.mouseScrollDelta.y < 0)
        {
            CurrentSelection--;
            if(CurrentSelection < 0) CurrentSelection = 2;
        }

        // Cases in which the player doesn't have the weapon that's internally selected.
        switch (CurrentSelection)
        {
            case 0:
                if(!HasPistol) {
                    if(LastFrameSelection > CurrentSelection) CurrentSelection = 2;
                    if(LastFrameSelection < CurrentSelection) CurrentSelection = 1;
                }
                break;

            case 1:
                if(!HasSMG) {
                    if(LastFrameSelection > CurrentSelection) CurrentSelection = 0;
                    if(LastFrameSelection < CurrentSelection) CurrentSelection = 2;
                }
                break;

            case 2:
                if(!HasFlashlight) {
                    if(LastFrameSelection > CurrentSelection) CurrentSelection = 1;
                    if(LastFrameSelection < CurrentSelection) CurrentSelection = 0;
                }
                break;


            default:
                CurrentSelection = 0;
                break;
        }

        if(Input.mouseScrollDelta.y != 0) ChangeWeapon();
    }

    public void ChangeWeapon()
    {
        switch(CurrentSelection)
        {
            case 0:
                if(HasPistol)
                {
                    PistolObject.SetActive(true);
                    SMGObject.SetActive(false);
                    FlashlightObject.SetActive(false);
                }
                break;
            
            case 1:
                if(HasSMG)
                {
                    SMGObject.SetActive(true);
                    PistolObject.SetActive(false);
                    FlashlightObject.SetActive(false);
                }
                break;
            
            case 2:
                if(HasFlashlight)
                {
                    FlashlightObject.SetActive(true);
                    PistolObject.SetActive(false);
                    SMGObject.SetActive(false);
                }
                break;

            default:
                CurrentSelection = -1;
                PistolObject.SetActive(false);
                SMGObject.SetActive(false);
                FlashlightObject.SetActive(false);
                break;
        }
    }
}
