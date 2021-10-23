using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	public AudioClip ShootSound;
	public int MagSize = 7;
	public int Mag;
	public float RateOfFire;
	public bool Automatic;
	public float AnimationAcceleration;
	public GameObject Casing;
	public Transform CasingSpawn;
	public Vector3 CasingForce;

	// Private variables.
	private ParticleSystem[] ps;
	private bool aiming;
	private bool jumping;
	private bool reloading;
	private float curSpeed;
	private Animator a;
	private MovementSystem ms;
	private AudioSource au;
	private float shotTimer;

	private void Start()
	{
		a = gameObject.GetComponentInChildren<Animator>();
		au = gameObject.GetComponent<AudioSource>();

		ps = gameObject.GetComponentsInChildren<ParticleSystem>();

		NoAmmo(Mag <= 0);
	}

	private void OnEnable()
	{
	    if(a != null) NoAmmo(Mag <= 0);
	}
	
	private void Update()
	{
		if (shotTimer > 0)
			shotTimer -= Time.deltaTime;

		if (ms == null)
		{
			ms = gameObject.GetComponentInParent<MovementSystem>();
		}

		int targetSpeed = 0;

		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		{
			targetSpeed = 1;

			if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") != 0))
			{
				targetSpeed = 2;
			}
		}

		if (targetSpeed != 2)
		{
			aiming = Input.GetKey(KeyCode.Mouse1);

			if (Mag > 0)
			{
				if (Automatic)
				{
					// If the trigger is held and we can shoot.
					if (Input.GetKey(KeyCode.Mouse0) && shotTimer <= 0)
					{
						Shoot();
						shotTimer = RateOfFire;
					}
				}else
				{
					if (Input.GetKeyDown(KeyCode.Mouse0) && shotTimer <= 0)
					{
						Shoot();
						shotTimer = RateOfFire;
					}
				}
			}
		} else
		{
			aiming = false;
		}

		jumping = Input.GetKey(KeyCode.Space);

		reloading = Input.GetKeyDown(KeyCode.Mouse0) && Mag <= 0 || Input.GetKey(KeyCode.R);

		curSpeed = Mathf.Lerp(curSpeed, targetSpeed, Time.deltaTime * AnimationAcceleration);

		a.SetBool("aiming", aiming);
		a.SetFloat("curSpeed",curSpeed);
		a.SetBool("jumping", jumping);
		a.SetBool("reloading", reloading);

		if (ms != null)
			a.SetBool("grounded", ms.IsGrounded);
		else
			a.SetBool("grounded", true);
	}

	private void Shoot()
	{
		if (aiming)
			a.CrossFade("AimShoot", 0.02f, 0, 0);
		else
			a.CrossFade("Shoot", 0.02f, 0, 0);

		if (Mag > 0)
			Mag--;

		if (ShootSound != null && au != null)
			au.PlayOneShot(ShootSound);

		for (int i = 0;i < ps.Length;i++)
		{
			ps[i].Emit(10);
		}
	}

	public void LoadMag()
	{
		Mag = MagSize;
		NoAmmo(false);
	}

	public void Eject()
	{
		if (Mag == 0)
		{
			NoAmmo(true);
		}

		if (Casing != null && CasingSpawn != null)
		{
			GameObject c = (GameObject)Instantiate(Casing, CasingSpawn.position, CasingSpawn.rotation);
			c.GetComponent<Rigidbody>().AddForce(CasingSpawn.transform.TransformDirection(CasingForce*Random.Range(0.9f,1.1f)));
			c.transform.SetParent(transform);
		}
	}


	public void NoAmmo(bool s)
	{
		// In some weapons, there is an animation to show that the Magazine is empty.
		// This animation is being played constantly on the second layer of the animation controller.
		// This layer will override the base layer.
		if (s)
		{
			a.SetLayerWeight(1, 1);
		}else
		{
			a.SetLayerWeight(1, 0);
		}
	}
}
