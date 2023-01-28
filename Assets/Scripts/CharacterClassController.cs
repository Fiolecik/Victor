using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClassController : MonoBehaviour
{
    [SerializeField] SpriteRenderer characterBody;
    public CharacterClass characterClass;
    SkillController skillController;

    private void Start()
    {
        skillController = GetComponent<SkillController>();
    }

    public void changeSkin(Sprite newBody)
    {
        characterBody.sprite = newBody;
    }

    public void changeClass(CharacterClass characterClass)
    {       
        this.characterClass = characterClass;
        changeSkin(characterClass.character);
        skillController.changeSkillSet(characterClass.type);
        switch (characterClass.type)
        {
            case CharacterClassType.priest:
                break;
            case CharacterClassType.scientist:
                break;
            case CharacterClassType.detective:
                break;
            case CharacterClassType.gravedigger:
                break;
            case CharacterClassType.nun:
                break;
            default:
                break;
        }
    }
}
