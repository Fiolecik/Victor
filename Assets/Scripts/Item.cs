using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    nothig,
    armor,
    weapon,
    shoes,
    gloves,
    trinket
}

[System.Serializable]
public struct Statistics
{
    public int strenght;
    public int thoughtness;
    public int agility;
    public int inteligence;

    public int damage;
    public float timeBetweenShots;

    public static Statistics operator+(Statistics s1, Statistics s2)
    {
        Statistics newS = new Statistics();
        newS.agility = s1.agility + s2.agility;
        newS.damage = s1.damage + s2.damage;
        newS.inteligence = s1.inteligence + s2.inteligence;
        newS.strenght = s1.strenght + s2.strenght;
        newS.thoughtness = s1.thoughtness + s2.thoughtness;
        newS.timeBetweenShots = s1.timeBetweenShots - s2.timeBetweenShots;
        return newS;
    }

    public static Statistics operator-(Statistics s1, Statistics s2)
    {
        Statistics newS = new Statistics();
        newS.agility = s1.agility - s2.agility;
        newS.damage = s1.damage - s2.damage;
        newS.inteligence = s1.inteligence - s2.inteligence;
        newS.strenght = s1.strenght - s2.strenght;
        newS.thoughtness = s1.thoughtness - s2.thoughtness;
        newS.timeBetweenShots = s1.timeBetweenShots + s2.timeBetweenShots;
        return newS;
    }
}

[CreateAssetMenu(fileName = "Item", menuName = "Objects/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string name;
    public ItemType type;
    public Sprite sprite;
    public GameObject dropPrefab;
    public GameObject weaponPrefab;

    public static Item copy(Item item)
    {
        Item i = CreateInstance<Item>();
        i.dropPrefab = item.dropPrefab;
        i.id = item.id;
        i.name = item.name;
        i.sprite = item.sprite;
        i.type = item.type;
        i.weaponPrefab = item.weaponPrefab;
        i.statistics = item.statistics;
        return i;
    }

    public Statistics statistics;
}
