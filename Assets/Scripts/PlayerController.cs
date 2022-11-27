using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform GunHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * MoveSpeed, moveInput.y * Time.deltaTime * MoveSpeed, 0f);

        theRB.velocity = moveInput * MoveSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        //rotate gun arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y,offset.x) * Mathf.Rad2Deg;
        GunHand.rotation = Quaternion.Euler(0, 0, angle);
    }
}
