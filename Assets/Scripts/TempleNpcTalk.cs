using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TempleNpcTalk : MonoBehaviour
{
    public chatManager chat;
    int lastMsgId = -1;
    bool talking = false;
    bool inScreen = false;
    playerMovement playerMovement;
    playerStats player;
    public GameObject p, energyEffect;
    RandomWalker npc;
    int stageOfConv = 0;

    // Start is called before the first frame update
    RandomWalker walker;
    private void Start()
    {
        walker = GetComponent<RandomWalker>();
        
        
        playerMovement = p.GetComponent<playerMovement>();
        player = p.GetComponent<playerStats>();
        npc = GetComponent<RandomWalker>();
    }
    // Update is called once per frame
   void OnDisable()
    {
        finishConv();
    }
    void OnEnable()
    {
        
        StartCoroutine(CheckConversation());
    }
    bool ComparePlayerMsgWith(string msg)
    {
        if (chat.messageList.Count == 0)
            return false;
        if (string.Compare(chat.messageList[chat.messageList.Count - 1].text.Substring(0, 6), "Player") != 0)
            return false;
        
        string lastPlayerMsg = chat.messageList[chat.messageList.Count - 1].text.Substring(8);
        return (string.Compare(lastPlayerMsg, msg) == 0);
    }

    IEnumerator CheckConversation()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        inScreen = (Math.Abs(playerMovement.realPos.x - npc.realPos.x) < 5 && Math.Abs(playerMovement.realPos.y - npc.realPos.y) < 5);
        if (inScreen)
        {
            if (stageOfConv > 0 && ComparePlayerMsgWith("bye"))
                finishConv();
            else switch (stageOfConv)
                {
                    case 0:
                        {
                            if (ComparePlayerMsgWith("hi") && talking == false)
                            {
                                talking = true;
                                npc.randWalk = false;
                                chat.sendNPCMessageToChat("Hello there and welcome to the Rookguard! There are plenty of creatures scattered on this island. Help us clean as much as you can and come back when you have at least 6400 experience points and 100 coins.", "NPC");
                                lastMsgId = chat.messageList[chat.messageList.Count - 1].id;
                                stageOfConv++;
                            }
                            break;
                        }

                    case 1:
                        {
                            if (ComparePlayerMsgWith("done"))
                            {
                                if (player.experience >= 6400 && player.money > 100)
                                {
                                    chat.sendNPCMessageToChat("You did it, Well done! Are you ready to continue to the mainland? it will cost you 100 coins.", "NPC");
                                    lastMsgId = chat.messageList[chat.messageList.Count - 1].id;
                                    stageOfConv++;
                                }
                                else
                                {

                                    string txt = "Unfortunatelly you don't have enough money or experience.";
                                    chat.sendNPCMessageToChat(txt, "NPC");
                                    finishConv();
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            if (ComparePlayerMsgWith("yes"))
                            {
                                Vector3 newPos = new Vector3(0, 100, 1); ;
                                Instantiate(energyEffect, newPos, Quaternion.identity);
                                p.transform.position = newPos;
                                playerMovement.realPos = newPos;
                                player.AddMoney(-100);

                            }
                            else if (ComparePlayerMsgWith("no"))
                                finishConv();
                            break;
                        }
                }
        }
        else
            finishConv();


        StartCoroutine(CheckConversation());
    }
    void finishConv()
    {
        if(stageOfConv>0)
            chat.sendNPCMessageToChat("Good bye.", "NPC");
        npc.randWalk = true;
        stageOfConv = 0;
        talking = false;
    }
}
