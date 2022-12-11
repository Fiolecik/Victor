using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float MoveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform GunHand;

    private Camera theCam;

    public Animator anim;

    public GameObject bulletToFire;

    public Transform FirePoint;

    public float timeBetweenShots;

    private float shotCounter;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        //transform.position += new Vector3(moveInput.x * Time.deltaTime * MoveSpeed, moveInput.y * Time.deltaTime * MoveSpeed, 0f);

        theRB.velocity = moveInput * MoveSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        if (mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            GunHand.localScale = new Vector3(-1f, -1f, 1f);
        } else 
        {
            transform.localScale = Vector3.one;
            GunHand.localScale = Vector3.one;
        }

        //rotate gun arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y,offset.x) * Mathf.Rad2Deg;
        GunHand.rotation = Quaternion.Euler(0, 0, angle);

        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, FirePoint.position, FirePoint.rotation);
            shotCounter = timeBetweenShots;
        }

        if(Input.GetMouseButtonDown(0))
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, FirePoint.position, FirePoint.rotation);

                shotCounter = timeBetweenShots;
            }
        }

        if (moveInput != Vector2.zero) 
        {
            anim.SetBool("isMoving", true);
        } else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
