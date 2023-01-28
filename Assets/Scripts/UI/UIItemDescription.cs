using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDescription : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Text itemName;
    [SerializeField] Text itemDescription;
    bool hidding;
    float alpha=0;

    // Update is called once per frame
    void Update()
    {
        if(hidding && alpha>0)
        {
            alpha -= Time.deltaTime * 2;
        }
        else if (alpha < 1)
        {
            alpha += Time.deltaTime * 2;
        }
        canvasGroup.alpha = alpha;
        if(alpha>0)
        {
            gameObject.transform.position = Input.mousePosition;
        }
        if (hidding && alpha <= 0)
            gameObject.SetActive(false);
    }

    public void showDescription(Item i)
    {
        hidding = false;
        itemName.text = i.name;
        string description = "";
        description += i.type + "\n";
        if(i.statistics.timeBetweenShots!=0)
            setInDescription(ref description, "AS", 1/i.statistics.timeBetweenShots, Color.yellow);
        setInDescription(ref description, "DMG", i.statistics.damage, Color.red);
        setInDescription(ref description, "agility", i.statistics.agility, Color.magenta);
        setInDescription(ref description, "inteligence", i.statistics.inteligence, Color.green);
        setInDescription(ref description, "thoughtness", i.statistics.thoughtness, Color.cyan);
        setInDescription(ref description, "strenght", i.statistics.strenght, Color.red);
        itemDescription.text = description;
    }

    void setInDescription(ref string description, string name , float value, Color color = new Color())
    {
        if(value>0)
        {
            string varColor = "<color=#" + ColorUtility.ToHtmlStringRGB(color) + "" + ">";
            description += varColor+ name + ":" + value + "</color>" + "\n";
        }
    }

    public void hideDescription()
    {
        hidding = true;
    }
}
