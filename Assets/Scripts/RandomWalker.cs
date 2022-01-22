
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class RandomWalker : MonoBehaviour
{

    Animator animator;
    public Vector2 realPos, direction;
    public int dir = 6;
    public bool inTheMiddleOfWalking = false;
    public bool randWalk = true;
    public float speed, stoppingDis;
    CircleCollider2D circleCollider;

   void Start()
    {
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnEnable()
    {

        if (animator != null)
            stopWalk();
        realPos = new Vector2((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.y));
        direction.x = 1;
        direction.y = 0;
        
        
        if (speed == 0)
            speed = 0.03f;
        
        StartCoroutine(RandWalk());
    }
    private void FixedUpdate()
    {
        stoppingDis = 0.07f * (speed / 0.03f); //change stoppingDis reletavely so if speed = 0.03, stoppingDis = 0.07
        if (inTheMiddleOfWalking)
        {
            if (direction.Equals(Vector2.left))
                dir = 4;
            else if (direction.Equals(Vector2.right))
                dir = 6;
            else if (direction.Equals(Vector2.up))
                dir = 8;
            else if (direction.Equals(Vector2.down))
                dir = 2;
        }
        circleCollider.offset = ((Vector2)realPos - (Vector2)transform.position);
        DoWalk();

    }

    IEnumerator RandWalk()
    {
      
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(2f, 4f));
        if (randWalk)
        {
            Vector2 directionTry = ConvertNumToDir(UnityEngine.Random.Range(0f, 3.999f));
            while (CheckAndMove(directionTry))
                directionTry = ConvertNumToDir(UnityEngine.Random.Range(0f, 3.999f));
            direction = directionTry;
            inTheMiddleOfWalking = true;
        }
        StartCoroutine(RandWalk());
    }
    Vector2 ConvertNumToDir(double num)
    {
        num = Math.Floor(num);
        switch (num)
        {
            case 0:
                return Vector2.up;
            case 1:
                return Vector2.down;
            case 2:
                return Vector2.left;
            case 3:
                return Vector2.right;
        }
        return Vector2.zero;
    }

    bool CheckAndMove(Vector2 directionVector)
    {
        if (inTheMiddleOfWalking)
            return true;

        RaycastHit2D[] hits = Physics2D.RaycastAll(realPos + directionVector, -directionVector, 0.2f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.layer == 8)
                return true;
            if (hit.collider.gameObject.CompareTag("creature"))
                return true;

            Item item = hit.collider.GetComponent<Item>();
            if (item != null && !item.isWalkable())
                return true;
        }
        realPos = realPos + directionVector;
        inTheMiddleOfWalking = true;
        return false;
    }
    void DoWalk()
    {
        if (Math.Pow((transform.position.x - realPos.x), 2) + Math.Pow((transform.position.y - realPos.y), 2) < Math.Pow(stoppingDis, 2))
        {
            stopWalk();
        }
        else
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", 1);
            animator.SetInteger("Direction", dir);
            transform.position = transform.position + ((Vector3)direction * speed);
        }
        
    }
    void stopWalk()
    {
        direction = Vector2.zero;
        animator.SetFloat("Speed", 0f);
        animator.SetInteger("Direction", dir);
        transform.position = (Vector2)realPos;
        inTheMiddleOfWalking = false;
    }
}




