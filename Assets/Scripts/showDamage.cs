using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showDamage : MonoBehaviour
{
    public GameObject damageText;

    public void createDamage(Vector3 pos, string damage, Color color)
    {
        GameObject newText = Instantiate(damageText, pos, Quaternion.identity, GameObject.Find("WoldCanvas").transform);
        newText.GetComponent<Text>().text = damage;
        newText.GetComponent<Text>().color = color;
    }
}
