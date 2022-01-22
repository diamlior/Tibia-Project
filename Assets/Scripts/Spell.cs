using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int manaCost;
    public playerStats stats;
    public virtual void Start()
    {
        stats = GameObject.Find("player").GetComponent<playerStats>();
    }
    public virtual bool isSpell(string name)
    {
        foreach (Transform child in transform)
            if (child.name == name)
            {
                if (child.gameObject.active == true)
                {
                    child.GetComponent<Spell>().Cast();
                    return true;
                }
            }

        return false;
    }    
    public virtual void Cast()
    {
        Debug.Log("this spell was not overrided!");
    }

    public virtual bool enoughMana()
    {
        if (stats.currentMana > manaCost)
        {
            stats.DecreaseManaBy(manaCost);
            return true;
        }
            
        GameObject noManaText = Instantiate(stats.GetComponent<playerMovement>().unwalkabletext, GameObject.Find("ScreenUI").transform);
        noManaText.GetComponent<UnityEngine.UI.Text>().text = "No Enough Mana!";
        return false;
    }
}
