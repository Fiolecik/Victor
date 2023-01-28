using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            message.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            message.SetActive(false);
    }

    public virtual void interract(GameObject g)
    {
        Debug.Log("Interacting");
    }
}
