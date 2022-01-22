using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chatManager : MonoBehaviour
{
    public int maxmsgs = 12;
    public GameObject chatPanel, textObject, publicText;
    public InputField chatBox;
    public Spell spells;
    public List<Message> messageList = new List<Message>();
    int counter = 0;
   
  
    void Update()
    {
        
        if(chatBox.text != "")
        {
                 // sending a text to chat
            if (Input.GetKeyDown(KeyCode.Return))
            {
                sendMessageToChat(chatBox.text);
                chatBox.text = "";
            }
        }
        else
        {
                // putting the focus on the chat
            if (!chatBox.isFocused)
                chatBox.ActivateInputField();
        }


    }

    public void sendMessageToChat(string text)
    {
        // Let maximum of maxMassages at anytime.
        if (messageList.Count >= maxmsgs)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }
        
        if(!spells.isSpell(text))
        {
            //add the intro to the text
            Message newMessage = new Message();
            newMessage.text = "Player: " + text;
            newMessage.id = counter;
            counter++;

            //create the Text GameObject and add it to newMessage
            GameObject newText = Instantiate(textObject, chatPanel.transform);
            newMessage.textObject = newText.GetComponent<Text>();
            newMessage.textObject.text = newMessage.text;

            messageList.Add(newMessage);
        }
        StartCoroutine(sayPublic(text));


    }

    public void sendNPCMessageToChat(string text, string NameNPC)
    {
        // Let maximum of maxMassages at anytime.
        if (messageList.Count >= maxmsgs)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        if (!spells.isSpell(text))
        {
            //add the intro to the text
            Message newMessage = new Message();
            newMessage.text = NameNPC+": " + text;
            newMessage.id = counter;
            counter++;

            //create the Text GameObject and add it to newMessage
            GameObject newText = Instantiate(textObject, chatPanel.transform);
            newMessage.textObject = newText.GetComponent<Text>();
            newMessage.textObject.text = newMessage.text;
            newText.GetComponent<Text>().color = Color.cyan;

            messageList.Add(newMessage);
        }


    }
    IEnumerator sayPublic(string text)
    {
        Message publicMessage = new Message();
        publicMessage.text = "Player says: \n" + text;

        GameObject newPublicText = Instantiate(publicText, GameObject.Find("player").transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity, GameObject.Find("WoldCanvas").transform);
        publicMessage.textObject = newPublicText.GetComponent<Text>();
        publicMessage.textObject.text = publicMessage.text;
        yield return new WaitForSecondsRealtime(4);
        Destroy(publicMessage.textObject.gameObject);


    }
    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObject;
        public int id;
    }
}
