using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    item,
    coin,
    health,
    weapon
}

public class Pickable : MonoBehaviour
{
    [SerializeField] PickupType type;

    [SerializeField] Item pickupItem;
    [SerializeField] Weapon gun;
    [SerializeField] int ammout;
    
    [SerializeField] float waitToBeCollected=0.5f;

    void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToBeCollected <= 0)
        {
            switch (type)
            {
                case PickupType.item:
                    if (!other.gameObject.GetComponent<Inventory>().addItem(pickupItem))
                        return;
                    break;
                case PickupType.coin:
                    LevelManager.instance.GetCoins(ammout);
                    break;
                case PickupType.health:
                    PlayerHealthController.instance.HealPlayer(ammout);
                    break;
                case PickupType.weapon:
                    bool hasGun = false;
                    foreach (Weapon gunToCheck in PlayerController.instance.avalaibleGuns)
                    {
                        if (gun.weaponName == gunToCheck.weaponName)
                        {
                            hasGun = true;
                        }
                    }

                    if (!hasGun)
                    {
                        Weapon gunClone = Instantiate(gun);
                        gunClone.transform.parent = PlayerController.instance.gunArm;
                        gunClone.transform.position = PlayerController.instance.gunArm.position;
                        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        gunClone.transform.localScale = Vector3.one;

                        PlayerController.instance.avalaibleGuns.Add(gunClone);
                        PlayerController.instance.currentGun = PlayerController.instance.avalaibleGuns.Count - 1;
                        PlayerController.instance.SwitchGun();


                    }
                    break;
                default:
                    break;
            }
            LevelManager.instance.GetCoins(ammout);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(14);
        }
    }
}
