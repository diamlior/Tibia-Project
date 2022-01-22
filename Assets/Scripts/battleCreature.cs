using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleCreature : MonoBehaviour
{
    public GameObject creature, redSquare, attackScript, healthBar;
    private GameObject whiteSquare;
    private Transform creatureName, miniWhiteSquare;
    private bool active = false;
    public bool created = false;
    public bool creatureIsAttacked = false;
    private int maxHealth, currentHealth;
    private HealthBar hb;
    private GameObject miniRedSquare;
    public Text nameText;
 
    bool toBeUpdated;

    private void Start()
    {
        toBeUpdated = creature.CompareTag("creature");
        creatureName = transform.Find("creatureName");
        attackScript = GameObject.Find("Attack");
        hb = healthBar.GetComponent<HealthBar>();
        miniWhiteSquare = transform.Find("WhiteSquare");
        miniWhiteSquare.gameObject.SetActive(false);
        miniRedSquare = Instantiate(miniWhiteSquare.gameObject, miniWhiteSquare.position, Quaternion.identity, miniWhiteSquare.transform.parent);
        miniRedSquare.GetComponent<Image>().color = Color.red;
        miniRedSquare.transform.SetAsFirstSibling();

    }
    private void Update()
    {
        if (toBeUpdated)
        {
            if (creatureIsAttacked && creature != null && creature.GetComponent<Creature>().isAlive() && attackScript.GetComponent<attackCreature>().isAttacking && attackScript.GetComponent<attackCreature>().attackedCreature.gameObject == this.creature.gameObject)
                miniRedSquare.gameObject.SetActive(true);
            else
                miniRedSquare.gameObject.SetActive(false);

            //if first time that creature is not null, turn created to true.
            if (!created && creature != null)
            {
                created = true;
                maxHealth = creature.GetComponent<Creature>().maxHealth;
                currentHealth = maxHealth;
                hb.setMaxHealth(maxHealth);
            }



            //check if creature has died
            if (created && (creature == null || !creature.GetComponent<Creature>().isAlive()))
            {
                if (whiteSquare != null)
                    Destroy(whiteSquare.gameObject);
                Destroy(this.gameObject);
            }

            //if creature is alive, check if current health is updated.
            else if (creature.GetComponent<Creature>().currentHealth != currentHealth)
            {
                currentHealth = creature.GetComponent<Creature>().currentHealth;
                hb.setHealth(currentHealth);
            }
        }
        


    }
    public void MarkWhiteAdd()
    {
        //Add white box around creature
        if (toBeUpdated)
        {
            if (creature != null && creature.GetComponent<Creature>().isAlive())
            {
                active = true;
                creatureName.GetComponent<Text>().color = Color.white;
                whiteSquare = Instantiate(redSquare, creature.transform.position, Quaternion.identity);
                whiteSquare.GetComponent<RedSquareFollow>().boxcolor = Color.white;
                whiteSquare.GetComponent<RedSquareFollow>().playerToFollow = creature;
                miniWhiteSquare.gameObject.SetActive(true);
            }
        }
    }
    public void MarkWhiteRemove()
    {
        if (toBeUpdated)
        {
            //remove main white box
            if (active)
            {

                miniWhiteSquare.gameObject.SetActive(false);
                active = false;
                hb.setHealth(currentHealth);
                Destroy(whiteSquare.gameObject);
            }
        }
    }
    public void MakeAttack()
    {
        attackCreature attackComp = attackScript.GetComponent<attackCreature>();
        attackComp.startAttack(creature);
        if (attackComp.isAttacking && attackComp.attackedCreature.gameObject == this.creature.gameObject)
            creatureIsAttacked = true;
        else
            creatureIsAttacked = false;


    }

    public void setName(string st)
    {
        nameText.text = st;
    }

    public void setImage(Sprite sp)
    {
        Transform Img = transform.Find("creatureImage");
        Img.GetComponent<UnityEngine.UI.Image>().sprite = sp;

    }
}
