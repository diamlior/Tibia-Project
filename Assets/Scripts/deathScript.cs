using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class deathScript : MonoBehaviour
{

    public GameObject corpse;
    public GameObject[] possibleLoot = new GameObject[7];
    public float[] ChanceForLootUpToHundred = new float[7];
    public int maxMoneyDrop = 0;
    public GameObject money;

    public void Die()
    {
        //change alive to false
        Creature creature = GetComponentInParent<Creature>();
        creature.alive = false;

        //create a corpse where the creature was
        Vector3 location = this.gameObject.transform.position;
        location = new Vector3(math.round(location.x), math.round(location.y), math.round(location.z));
        GameObject corpseObject = Instantiate(corpse, location, Quaternion.identity);
        Inventory corpseInvetory = corpseObject.GetComponent<Inventory>();
        corpseInvetory.Start();
        for(int i = 0; i<possibleLoot.Length;i++)
        {
            if(possibleLoot[i]!=null)
            {
                if (UnityEngine.Random.Range(0, 100) < ChanceForLootUpToHundred[i])
                {
                    GameObject newFish = Instantiate(possibleLoot[i], location, Quaternion.identity);
                    Item FishItem = newFish.GetComponent<Item>();
                    corpseInvetory.AddItem(newFish, FishItem.id, FishItem.type, FishItem.description, newFish.GetComponent<SpriteRenderer>().sprite);
                }
            }
        }
        if (money == null)
            Debug.Log("Money GameObject was not assigned!");
        else
        {
            int moneyDrop = UnityEngine.Random.Range(-1, maxMoneyDrop);
            if(moneyDrop>0)
            {
                GameObject moneyGO = Instantiate(money, location, Quaternion.identity);
                Money coins = moneyGO.GetComponent<Money>();
                coins.amount = moneyDrop;
                coins.Start();
                corpseInvetory.AddItem(moneyGO,0,"","",coins.GetCoinSprite());
            }
        }
        Debug.Log("A creature was killed.");
        //destroy creature
        Destroy(this.transform.parent.gameObject);
       // Destroy(this.gameObject);
    }
}
