using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playerStats : MonoBehaviour
{
    public Vector3 realPos;
    public int level, experience, money, maxHealth, currentHealth, maxMana, currentMana, direction;
    private playerMovement movementScript;
    HealthBar healthBar;
    public double expToNextLevel;

    public BarsUI bars;
    public TMPro.TextMeshProUGUI statsText;
    public UIMoneyUpdate moneyUIScript;



    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        maxHealth = 100;
        maxMana = 500;
        currentHealth = maxHealth;
        currentMana = maxMana;
        direction = 2;
        movementScript = GetComponent<playerMovement>();
        level = 1;
        expToNextLevel = expToNextLevelCalc(level);
        experience = 0;
        healthBar = GetComponentInChildren<HealthBar>();
        bars.manabar.maxValue = maxMana;
        bars.manabar.value = bars.manabar.maxValue;
        bars.healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        bars.manaText.text = currentMana.ToString() + "/" + maxMana.ToString();
        statsText.text = "Lvl: " + level + " Exp: " + experience;
        moneyUIScript.UpdateCoins(money);
        StartCoroutine(autoIncrease());
    }
    public void AddMoney(int coins)
    {
        money += coins;
        moneyUIScript.UpdateCoins(money);

    }
    double expToNextLevelCalc(int currentLvl)
    {
        int nextLvl = currentLvl+1;
        double output = nextLvl*nextLvl*nextLvl - 6*nextLvl*nextLvl + 17*nextLvl - 12;
        return output*50/3;
    }
    // Update is called once per frame
    void Update()
    {
        direction = movementScript.dir;
        realPos = movementScript.realPos;
    }

    void UpgradeLevel()
    {
        level++;
        expToNextLevel = expToNextLevelCalc(level);
        maxHealth = level * 100;
        currentHealth = maxHealth;
        maxMana = level * 100;
        currentMana = maxMana;
        bars.manabar.maxValue = maxMana;
        bars.manabar.value = bars.manabar.maxValue;
        healthBar.setMaxHealth(maxHealth);
        bars.healthbar.maxValue = maxHealth;
    }
    public void UpdateExperience(int exp)
    {
        experience += exp;
        while(experience>expToNextLevel)
        {
            UpgradeLevel();
        }
        statsText.text = "Lvl: " + level + " Exp: " + experience;

    }

    public void UpdateHealth(int health)
    {
        if (health > maxHealth)
            health = maxHealth;
        healthBar.setHealth(health);
        currentHealth = health;
        bars.healthbar.value=health;
        bars.healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        if (currentHealth <= 0)
            Die();
    }
    public void DecreaseManaBy(int mana)
    {
        int newMana = currentMana - mana;
        if (newMana > maxMana)
            newMana = maxMana;
        if (newMana < 0)
            newMana = 0;
        currentMana = newMana;
        bars.manabar.value = newMana;
        bars.manaText.text = currentMana.ToString() + "/" + maxMana.ToString();
    }
    public void IncreaseHealhBy(int health)
    {
        int newHealth = health + currentHealth;
        if (newHealth > maxHealth)
            newHealth = maxHealth;
        UpdateHealth(newHealth);
    }

    public void UpdateMaxHealth(int max)
    {
        maxHealth = max;
        healthBar.setMaxHealth(max);
        bars.healthbar.maxValue = max;

    }
    public void UpdateMaxMana(int max)
    {
        maxMana = max;
        bars.manabar.maxValue = max;
    }
    void Die()
    {
        Debug.Log("You are dead!");
        UpdateHealth(maxHealth);
        Vector3 temple = new Vector3(2f, 10f, 2f);
        transform.position = temple;
        movementScript.realPos = temple;
        GameObject DeathText = Instantiate(movementScript.unwalkabletext, GameObject.Find("ScreenUI").transform);
        DeathText.GetComponent<UnityEngine.UI.Text>().text = "You are dead!";
    }
    IEnumerator autoIncrease()
    {
        yield return new WaitForSecondsRealtime(4f);
        IncreaseHealhBy(10);
        DecreaseManaBy(-10);
        StartCoroutine(autoIncrease());
    }
}
