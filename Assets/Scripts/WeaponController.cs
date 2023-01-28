using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Inventory playerInventory;
    [SerializeField] Transform gunSlot;
    [SerializeField] Weapon holdingGun;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory.onGunChanged += playerChangedGun;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void swapGun(Weapon newGun)
    {

    }

    public void removeGun()
    {

    }

    void playerChangedGun()
    {
        ItemSlot[] weapon = playerInventory.getSlotsByType(ItemType.weapon);
        if (holdingGun != null)
            Destroy(holdingGun.gameObject);
        if(weapon[0].holdingItem!=null)
        {
            GameObject go = Instantiate(weapon[0].holdingItem.weaponPrefab, gunSlot);
            go.transform.localPosition = Vector3.zero;
            holdingGun = go.GetComponent<Weapon>();
            holdingGun.timeBetweenShots = weapon[0].holdingItem.statistics.timeBetweenShots;
            holdingGun.damage = weapon[0].holdingItem.statistics.damage;
        }
    }

    public void use()
    {
        if (holdingGun == null)
            return;

        holdingGun.use();
    }

    public Weapon getGun()
    {
        return holdingGun;
    }
}
