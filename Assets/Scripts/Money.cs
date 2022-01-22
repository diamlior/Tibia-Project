using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Item

{
    public Sprite[] CoinsSprites = new Sprite[6];
    public SpriteRenderer coinSpriteRenderer;
    bool spriteChanged = false;

    // Start is called before the first frame update
    void Start()
    {
      
        base.Start();
        if (amount > 0 && !spriteChanged)
        {
            changeSprite();

        }
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (!spriteChanged && amount > 0)
            changeSprite();
    }

    public override void UseItem()
    {
        playermovement.GetComponent<playerStats>().AddMoney(amount);
        Destroy(this.gameObject);
    }

    public Sprite GetCoinSprite()
    {
        if (amount == 1)
        {
            return CoinsSprites[0];
        }
        else if (amount == 2)
        {
            return CoinsSprites[1];
        }
        else if (amount == 3)
        {
            return CoinsSprites[2];
        }
        else if (amount == 4)
        {
            return CoinsSprites[3];
        }
        else if (amount >= 5 && amount < 100)
        {
            return CoinsSprites[4];
        }
        else if (amount >= 100)
        {
            return CoinsSprites[5];
        }
        else
            return CoinsSprites[0];
    }
    void changeSprite()
    {
        if (amount == 1)
        {
            coinSpriteRenderer.sprite = CoinsSprites[0];
        }
        else if (amount == 2)
        {
            coinSpriteRenderer.sprite = CoinsSprites[1];
        }
        else if (amount == 3)
        {
            coinSpriteRenderer.sprite = CoinsSprites[2];
        }
        else if (amount == 4)
        {
            coinSpriteRenderer.sprite = CoinsSprites[3];
        }
        else if (amount >= 5 && amount < 100)
        {
            coinSpriteRenderer.sprite = CoinsSprites[4];
        }
        else if (amount >= 100)
        {
            coinSpriteRenderer.sprite = CoinsSprites[5];
        }
        spriteChanged = true;
    }
}
