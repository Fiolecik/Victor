using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] Skill[] skills = new Skill[4];
    
    public void useSkill(int id)
    {
        if(skills[id]!=null)
        {
            skills[id].use(transform);
        }
    }

    public void changeSkillSet(CharacterClassType characterClassType)
    {
        foreach (Skill s in skills)
        {
            Destroy(s);
        }
        switch (characterClassType)
        {
            case CharacterClassType.priest:
                break;
            case CharacterClassType.scientist:
                break;
            case CharacterClassType.detective:
                break;
            case CharacterClassType.gravedigger:
                break;
            default:
                break;
        }
    }
}
