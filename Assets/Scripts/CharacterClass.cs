using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterClassType
{
    priest,
    scientist,
    detective,
    gravedigger,
    nun
}

[CreateAssetMenu(fileName ="Character Class", menuName = "Objects/Character Class")]
public class CharacterClass : ScriptableObject
{
    public Statistics statistics;

    public Sprite character;

    public CharacterClassType type;
}
