﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSystem : MonoBehaviour {

	public Light LightObject;
	public bool IsOn;
	[Range(0, 100)] public float EnergyLevel;
	public Transform AttackOrigin;
	public float AttackPushForce;
	public float AttackRange;
	public Animator FlashlightAnimator;
	public AudioClip SwitchSound;

	// Private variables.
	[HideInInspector] public bool IsAttacking;

	// Use this for initialization
	private void Start ()
	{
		SetFlashlight(IsOn, false);
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if(Input.GetKeyDown(KeyCode.F)) SetFlashlight(!IsOn, true);
		if(IsOn) EnergyLevel -= (Time.deltaTime / 7.5f);
		EnergyLevel = Mathf.Clamp(EnergyLevel, 0f, 100f);
		LightObject.GetComponent<LightFlickerEffect>().enabled = (EnergyLevel <= 20f);
		if(EnergyLevel <= 0) SetFlashlight(false, false);

		if(Input.GetKeyDown(KeyCode.Mouse0) && !IsAttacking && Time.timeScale > 0) {
			FlashlightAnimator.SetTrigger("Attack");
			IsAttacking = true;
		}
	}

	public void Attack()
	{
		RaycastHit Hit;
		if (Physics.Raycast(AttackOrigin.position, AttackOrigin.forward, out Hit, AttackRange))
		{
			if (Hit.rigidbody != null)
			{
				Hit.rigidbody.AddForceAtPosition((-Hit.normal * AttackPushForce), Hit.point);
			}
		}

		IsAttacking = false;
	}

	public void SetFlashlight(bool Value, bool TriggerSound)
	{
		if(TriggerSound) Camera.main.GetComponent<AudioSource>()?.PlayOneShot(SwitchSound);
		LightObject.gameObject.SetActive(Value);
		IsOn = Value;
	}
}
