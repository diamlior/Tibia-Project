using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghoul : Creature
{
    public HealthBar healthbar;
    public deathScript ds;
    private void Start()
    {
        base.Start();
        this.gameObject.name = "Ghoul";
        alive = true;
        if(experience==0)
            experience = 80;
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
        playerStats = GameObject.Find("player").GetComponent<playerStats>();
        battleWindow.AllCreatures.Add(this);

    }
        //update creature with damage
    public override void makeDamage(int dmg, Color color)
    {
        //create a random damage between (2,dmg)
        System.Random rand = new System.Random();
        dmg = rand.Next(2, dmg);

        if (dmg >= currentHealth)
            dmg = currentHealth;
        currentHealth -= dmg; //update new health
        healthbar.setHealth(currentHealth); //update Health Bar
        makeDamageText(dmg, color); 
        if (currentHealth == 0)
        {
            playerStats.UpdateExperience(experience);
            battleWindow.AllCreatures.Remove(this);
            ds.Die();
            alive = false;
        }
            
    }

    public override bool isAlive()
    {
        return base.isAlive();
    }




}
