using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletToFire;

    public Transform firePoint;

    public float timeBetweenShots;
    [HideInInspector] public int damage;

    private float shotCounter;

    public string weaponName;
    public Sprite gunUI;

    public int itemCost;
    public Sprite gunShopSprite;


    public void use()
    {
        if (shotCounter > 0)
            return;
        GameObject g = Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
        g.GetComponent<PlayerBullet>().damageToGive = damage;
        shotCounter = timeBetweenShots;

        AudioManager.instance.PlaySFX(8);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            if (shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            { 
                //if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                //{
                //    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                //    shotCounter = timeBetweenShots;

                //    AudioManager.instance.PlaySFX(8);
                //}
                /*if (Input.GetMouseButton(0))
                {
                    shotCounter -= Time.deltaTime;

                    if (shotCounter <= 0)
                    {
                        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        shotCounter = timeBetweenShots;

                        AudioManager.instance.PlaySFX(8);
                    }
                }*/
            }
        }
    }
}
