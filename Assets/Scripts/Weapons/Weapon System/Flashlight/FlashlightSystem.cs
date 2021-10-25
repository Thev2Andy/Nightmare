using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSystem : MonoBehaviour {

	public Light LightObject;
	public bool IsOn;
	[Range(0, 100)] public float EnergyLevel;
	public AudioSource EquipSoundSource;
	public Transform AttackOrigin;
	public float AttackPushForce;
	public float AttackRange;
	public Animator FlashlightAnimator;
	public AudioClip SwitchSound;
	public AudioClip SwingSound;

	// Private variables.
	[HideInInspector] public bool IsAttacking;

	// Use this for initialization
	private void Start() {
		SetFlashlight(IsOn, false);
	}

	private void OnEnable() {
	   if(IsOn) EquipSoundSource.Play();
	}
	
	// Update is called once per frame
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.F)) SetFlashlight(!IsOn, true);
		if(IsOn) EnergyLevel -= (Time.deltaTime / 7.5f);
		EnergyLevel = Mathf.Clamp(EnergyLevel, 0f, 100f);
		LightObject.GetComponent<LightFlickerEffect>().enabled = (EnergyLevel <= 20f);
		AmmoUIController.Instance.DisplayCustomAmmo((Mathf.CeilToInt(EnergyLevel).ToString("0") + "%"), (!IsOn || EnergyLevel <= 72.35f));
		if(EnergyLevel <= 0) SetFlashlight(false, false);

		CrosshairController.Instance.SetCrosshairActive(true);

		if(Input.GetKeyDown(KeyCode.Mouse0) && !IsAttacking && Time.timeScale > 0) {
			BeginAttack();
		}
	}

	public void BeginAttack()
	{
		FlashlightAnimator.SetTrigger("Attack");
		IsAttacking = true;
	}

	public void Attack()
	{
		FlashlightAnimator.ResetTrigger("Attack");
		RaycastHit Hit;
		if (Physics.Raycast(AttackOrigin.position, AttackOrigin.forward, out Hit, AttackRange))
		{
			if (Hit.rigidbody != null)
			{
				Hit.rigidbody.AddForceAtPosition((-Hit.normal * AttackPushForce), Hit.point);
			}
		}

		Camera.main.GetComponent<AudioSource>()?.PlayOneShot(SwingSound);

		IsAttacking = false;
	}

	public void SetFlashlight(bool Value, bool TriggerSound)
	{
		if(TriggerSound) Camera.main.GetComponent<AudioSource>()?.PlayOneShot(SwitchSound);
		LightObject.gameObject.SetActive(Value);
		IsOn = Value;
	}

	private void OnDisable() {
		CrosshairController.Instance.SetCrosshairActive(false);
	}
}
