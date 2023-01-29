using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;
	[Header("Scripts")]
	[SerializeField] WeaponController gunController;
	[SerializeField] Inventory inventory;

	[Header("Statistics")]
	public float moveSpeed;
	public Vector2 moveInput;
	[HideInInspector] 
	public bool isKnocked = false;

	public Rigidbody2D theRB;

	public Transform gunArm;

	//private Camera theCam;

	public Animator anim;

   /* public GameObject bulletToFire;

	public Transform firePoint;

	public float timeBetweenShots;

	private float shotCounter;*/

	public SpriteRenderer bodySR;

	public float activeMoveSpeed;
	public float dashSpeed = 8f, dashLenght = .5f, dashCooldown = 1f, dashInvincibility = .5f;
	[HideInInspector]
	public float dashCounter;
	private float dashCoolCounter;

	[HideInInspector]
	public bool canMove = true;

	public List<Weapon> avalaibleGuns = new List<Weapon>();
	[HideInInspector]
	public int currentGun;

	public int characterLevel;
	public int experience;
	public int unspentSkillPoints;

	private void Awake()
	{
		instance = this;

		DontDestroyOnLoad(gameObject);
	}

	// Start is called before the first frame update
	void Start()
	{
		//theCam = Camera.main;

		//activeMoveSpeed = moveSpeed;

		UIController.instance.currentGun.sprite = avalaibleGuns[currentGun].gunUI;
		UIController.instance.gunText.text = avalaibleGuns[currentGun].weaponName;
	}

	// Update is called once per frame
	void Update()
	{
		activeMoveSpeed = moveSpeed;
		
		if(Input.GetKeyDown(KeyCode.I))
		{
			UIController.instance.switchInventory(inventory);
		}
		
		if (canMove && !LevelManager.instance.isPaused)
		{
			if(Input.GetMouseButton(0))
			{
				gunController.use();
			}
			
			if(Input.GetKeyDown(KeyCode.E))
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
				foreach(Collider2D c in colliders)
				{
					Interactable interactable = c.GetComponent<Interactable>();
					if(interactable!=null)
					{
						interactable.interract(gameObject);
					}
				}
			}
			moveInput.x = Input.GetAxisRaw("Horizontal");
			moveInput.y = Input.GetAxisRaw("Vertical");

			moveInput.Normalize();

			//transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

			theRB.velocity = moveInput * activeMoveSpeed;


			Vector3 mousePos = Input.mousePosition;
			Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);

			if (mousePos.x < screenPoint.x)
			{
				transform.localScale = new Vector3(-1f, 1f, 1f);
				gunArm.localScale = new Vector3(-1f, -1f, 1f);
			}
			else
			{
				transform.localScale = Vector3.one;
				gunArm.localScale = Vector3.one;
			}

			//rotate gun arm
			Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
			float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
			gunArm.rotation = Quaternion.Euler(0, 0, angle);

			/*if (Input.GetMouseButtonDown(0))
			{
				Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
				shotCounter = timeBetweenShots;

				AudioManager.instance.PlaySFX(8);
			}
			if (Input.GetMouseButtonDown(0))
			{
				shotCounter -= Time.deltaTime;

				if (shotCounter <= 0)
				{
					Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
					shotCounter = timeBetweenShots;

					AudioManager.instance.PlaySFX(8);
				}
			}*/

   //         if (Input.GetKeyDown(KeyCode.Tab))
			//{
   //             if(avalaibleGuns.Count > 0)
			//	{
   //                 currentGun++;
   //                 if(currentGun >= avalaibleGuns.Count)
			//		{
   //                     currentGun = 0;
			//		}

   //                 SwitchGun();

			//	} else
			//	{
   //                 Debug.LogError("Player has no guns!");
			//	}
			//}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (dashCoolCounter <= 0 && dashCounter <= 0)
				{
					activeMoveSpeed = dashSpeed;
					dashCounter = dashLenght;

					anim.SetTrigger("dash");

					PlayerHealthController.instance.MakeInvincible(dashInvincibility);

					AudioManager.instance.PlaySFX(17);
				}



			}

			if (dashCounter > 0)
			{
				dashCounter -= Time.deltaTime;
				if (dashCounter <= 0)
				{
					if (dashCoolCounter <= 0 && dashCounter <= 0)
					{
						activeMoveSpeed = moveSpeed;
						dashCoolCounter = dashCooldown;
					}

				}
			}

			if (dashCoolCounter > 0)
			{
				dashCoolCounter -= Time.deltaTime;
			}


			if (moveInput != Vector2.zero)
			{
				anim.SetBool("isMoving", true);
			}
			else
			{
				anim.SetBool("isMoving", false);
			}
		} else
		{
			theRB.velocity = Vector2.zero;
			anim.SetBool("isMoving", false);
		}
	}

	public void SwitchGun()
	{
		foreach(Weapon theGun in avalaibleGuns)
		{
			theGun.gameObject.SetActive(false);
		}

		avalaibleGuns[currentGun].gameObject.SetActive(true);

		UIController.instance.currentGun.sprite = avalaibleGuns[currentGun].gunUI;
		UIController.instance.gunText.text = avalaibleGuns[currentGun].weaponName;
	}

	public IEnumerator Knockbacked()
	{
		canMove = false;
		anim.SetBool("isKnocked", true);
		yield return new WaitForSeconds(0.2f);
		canMove = true;
		anim.SetBool("isKnocked", false);
	}

	public void GetExperience(int experienceGet)
	{
		experience += experienceGet;

		var expForNextLevel = GetMaxExp(characterLevel);

		if (experience > expForNextLevel)
		{
			experience -= expForNextLevel;
			characterLevel++;
			// TODO: Animacja Lvl up
			unspentSkillPoints++;
		}
	}

	public int GetMaxExp(int level)
	{
		// (level/x)^y
		var neededExp = Convert.ToInt32(Math.Ceiling(Math.Pow((level / 0.07), 2.0)));
		return neededExp;
	}
}
