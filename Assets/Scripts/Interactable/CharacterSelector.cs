using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : Interactable
{
    [SerializeField] CharacterClass characterClass;
    public PlayerController playerToSpawn;

    public bool shouldUnlock;

    // Start is called before the first frame update
    void Start()
    {
        if (shouldUnlock)
        {
            if (PlayerPrefs.HasKey(playerToSpawn.name))
            {
                if (PlayerPrefs.GetInt(playerToSpawn.name) == 1)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public override void interract(GameObject g)
    {
        //Vector3 playerPos = PlayerController.instance.transform.position;

        CharacterClassController classController = g.GetComponent<CharacterClassController>();

        //classController.changeSkin(newSkin);
        classController.changeClass(characterClass);

        //Destroy(PlayerController.instance.gameObject);

        //PlayerController newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);
        //PlayerController.instance = newPlayer;

        gameObject.SetActive(false);

        //CameraController.instance.target = newPlayer.transform;

        //CharacterSelectManager.instance.activePlayer = newPlayer;
        //CharacterSelectManager.instance.activeCharSelect.gameObject.SetActive(true);
        //CharacterSelectManager.instance.activeCharSelect = this;
    }
}
