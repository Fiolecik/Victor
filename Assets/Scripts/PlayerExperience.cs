using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int experience;

 

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //if (Player.instance.Experience + experience >= Player.instance.maxExperience)
            //{
                //player has reached max experience
               // Player.instance.Experience = Player.instance.maxExperience;
           //}
            //else
            //{
            //    Player.instance.Experience += experience;
            //}

            PlayerController.instance.GetExperience(experience);

            Destroy(gameObject);
        }
    }
}
