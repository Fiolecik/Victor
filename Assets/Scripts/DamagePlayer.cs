using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Player")){
            
            PlayerHealthController.instance.DamagePlayer();
            Debug.Log(transform.position);
            PlayerController.instance.transform.position = Vector3.MoveTowards(PlayerController.instance.transform.position,(PlayerController.instance.transform.position + (PlayerController.instance.transform.position - transform.position).normalized * 0.6f), .6f);
            StartCoroutine(PlayerController.instance.Knockbacked());

        }
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }
}
