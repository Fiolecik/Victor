using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : Interactable
{
    public Pickable[] potentialGuns;

    public SpriteRenderer theSR;
    public Sprite chestOpen;

    private bool isOpen;

    public Transform spawnPoint;

    public float scaleSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen)
		{
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed);
		}
    }

    public override void interract(GameObject g)
    {
        if (isOpen)
            return;
        int gunSelect = Random.Range(0, potentialGuns.Length);

        Instantiate(potentialGuns[gunSelect], spawnPoint.position, spawnPoint.rotation);

        theSR.sprite = chestOpen;

        isOpen = true;

        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
}
