using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Creature : MonoBehaviour
{
    public int maxHealth;
    public int experience;
    public int currentHealth;
    public bool alive;
    public GameObject showDamageScript;
    public Sprite MainSprite;
    public playerStats playerStats;
    protected BattleWindowManager battleWindow;
    public virtual void Start()
    {
        battleWindow = GameObject.Find("BattleWindowManager").GetComponent<BattleWindowManager>();
        playerStats = GameObject.Find("player").GetComponent<playerStats>();
    }
    //virtual - Check if alive
    public virtual bool isAlive()
    {
        return alive;
    }

    public GameObject getDamageScript()
    {
        return showDamageScript;
    }
        
    //virtual - make Damage (must be overrided)
    public virtual void makeDamage(int dmg, Color color)
    {
        Debug.Log("this function was not overrided!");
        }
    public virtual void makeDamageText(int dmg, Color color)
    {
        showDamageScript.GetComponent<showDamage>().createDamage(transform.position, dmg.ToString(), color);
    }

    public virtual Sprite GetMainSprite()
    {
        return MainSprite;
    }
}
