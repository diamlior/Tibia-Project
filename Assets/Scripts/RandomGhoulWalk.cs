using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class RandomGhoulWalk : MonoBehaviour
{
    public Boolean isAttacking = true;
    public Vector3 nextStep;
    private Transform rb;
    Vector2 direction;
    private Animator animator;
    private Vector2 target = new Vector2(0, 0);
    public Vector3 realPos;
    public float speed = 3f;
    public GameObject player;
    private showDamage showdamage;
    public float stoppingDis = 0.07f;
    public float waitingAttackTime = 0.3f;
    public playerMovement playermovement;
    public int dir = 2;
    private CircleCollider2D circleCollider;
    public float attackingSpeed;

    
    public int state = 0; //1-9 by the side keyboard
    private int[] directions = { 2, 4, 6, 8 };

    [SerializeField]
    public enemyAI pathfinding;


    System.Random rand = new System.Random();

    // Start is called before the first frame update

    public void Start()
    {
        if (attackingSpeed == 0)
            attackingSpeed = 1;
        player = GameObject.Find("player");
        playermovement = player.GetComponent<playerMovement>();
        showdamage = GameObject.Find("ShowDamage").GetComponent<showDamage>();
        animator = GetComponent<Animator>();
        rb = this.transform;
        circleCollider = this.GetComponent<CircleCollider2D>();
        pathfinding = this.GetComponent<enemyAI>();
        pathfinding.playerMovement = player.GetComponent<playerMovement>();
        InvokeRepeating("AttackMode", 0.2f, 0.2f);
        InvokeRepeating("Attack", attackingSpeed, attackingSpeed);
        target = new Vector2(rb.position.x, rb.position.y);
        realPos = new Vector3((float)Math.Round(rb.position.x), (float)Math.Round(rb.position.y), rb.transform.position.z);
    }

    void OnEnalbed()
    {
        stopWalk();
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        stoppingDis = 0.07f * (speed / 0.03f); //change stoppingDis reletavely so if speed = 0.03, stoppingDis = 0.07

        if (state == 0)
        {

            switch (dir)
            {
                case 1:
                    {
                        target = (Vector2)rb.position + Vector2.left + Vector2.down;
                        state = dir;
                        break;
                    }
                case 2:
                    {
                        target = (Vector2)rb.position + Vector2.down;
                        state = dir;
                        break;
                    }
                case 3:
                    {
                        target = (Vector2)rb.position + Vector2.right + Vector2.down;
                        state = dir;
                        break;
                    }
                case 4:
                    {
                        target = (Vector2)rb.position + Vector2.left;
                        state = dir;
                        break;
                    }
                case 6:
                    {
                        target = (Vector2)rb.position + Vector2.right;
                        state = dir;
                        break;
                    }
                case 7:
                    {
                        target = (Vector2)rb.position + Vector2.left + Vector2.up;
                        state = dir;
                        break;
                    }
                case 8:
                    {
                        target = (Vector2)rb.position + Vector2.up;
                        state = dir;
                        break;
                    }
                case 9:
                    {
                        target = (Vector2)rb.position + Vector2.right + Vector2.up;
                        state = dir;
                        break;
                    }
            }
        }
        else
            dir = state;

        circleCollider.offset = ((Vector2)realPos - (Vector2)rb.position);

        switch (state)
        {
            case 6:
                direction = Vector2.right;
                break;

            case 4:
                direction = Vector2.left;
                break;

            case 8:
                direction = Vector2.up;
                break;

            case 2:
                direction = Vector2.down;
                break;
            case 1:
                direction = Vector2.left + Vector2.down;
                break;
            case 3:
                direction = Vector2.right + Vector2.down;
                break;
            case 7:
                direction = Vector2.left + Vector2.up;
                break;
            case 9:
                direction = Vector2.right + Vector2.up;
                break;
        }



        DoWalk();
    }





    void RandWalk()
    {
        if (!isAttacking)
        {
            if (!isAttacking)
            {

                dir = directions[rand.Next(0, directions.Length)];
                if (checkMovement(2) || checkMovement(4) || checkMovement(6) || checkMovement(8))
                {
                    while (checkAndMove(dir))
                        dir = directions[rand.Next(0, directions.Length)];
                    state = 0;

                }
            }

        }


    }
    public void AttackMode()
    {
        if (isAttacking && state == 5)
        {
            if (pathfinding.waypoints.Count > 2)
            {

                if (nextStep == Vector3.left)
                {
                    dir = 4;
                    state = 0;
                }

                else if (nextStep == Vector3.right)
                {
                    dir = 6;
                    state = 0;
                }
                else if (nextStep == Vector3.up)
                {
                    dir = 8;
                    state = 0;
                }
                else if (nextStep == Vector3.down)
                {
                    dir = 2;
                    state = 0;
                }
                else if (nextStep == Vector3.down + Vector3.left)
                {
                    dir = 1;
                    state = 0;
                }
                else if (nextStep == Vector3.down + Vector3.right)
                {
                    dir = 6;
                    state = 0;
                }
                else if (nextStep == Vector3.up + Vector3.left)
                {
                    dir = 7;
                    state = 0;
                }
                else if (nextStep == Vector3.up + Vector3.right)
                {
                    dir = 9;
                    state = 0;
                }
                else
                {
                    stopWalk();
                    return;
                }

                checkAndMove(dir);
            }
        }
    }
    public void Attack()
    {
        if (isAttacking && state == 5)
        {
            if (Math.Abs(realPos.x - pathfinding.playerMovement.realPos.x) <= 1 && Math.Abs(realPos.y - pathfinding.playerMovement.realPos.y) <= 1)
            {
                int dmg = rand.Next(1, 15);
                playerStats playerstats = player.GetComponentInChildren<playerStats>();
                playerstats.IncreaseHealhBy(dmg * -1);
                showdamage.createDamage(pathfinding.playerMovement.realPos, dmg.ToString(), Color.red);
            }
        }
    }




    Boolean checkMovement(int dir)
    {

        if (state != 0 && state != 5)
            return true;

        Vector3 directionVector = new Vector3(0f, 0f, 0f);
        switch (dir)
        {
            case 2:
                directionVector = Vector3.down;
                break;
            case 4:
                directionVector = Vector3.left;
                break;
            case 6:
                directionVector = Vector3.right;
                break;
            case 8:
                directionVector = Vector3.up;
                break;
            case 1:
                directionVector = Vector3.down + Vector3.left;
                break;
            case 3:
                directionVector = Vector3.down + Vector3.right;
                break;
            case 7:
                directionVector = Vector3.up + Vector3.left;
                break;
            case 9:
                directionVector = Vector3.up + Vector3.right;
                break;
        }

        RaycastHit2D hit = Physics2D.Raycast(realPos + directionVector, -directionVector, 0.2f);

        return (!hit);
    }
    Boolean checkAndMove(int dir)
    {
        if (state == 0 || state == 5)
        {
            Vector3 directionVector = new Vector3(0f, 0f, 0f);
            switch (dir)
            {
                case 2:
                    directionVector = Vector3.down;
                    break;
                case 4:
                    directionVector = Vector3.left;
                    break;
                case 6:
                    directionVector = Vector3.right;
                    break;
                case 8:
                    directionVector = Vector3.up;
                    break;
                case 1:
                    directionVector = Vector3.down + Vector3.left;
                    break;
                case 3:
                    directionVector = Vector3.down + Vector3.right;
                    break;
                case 7:
                    directionVector = Vector3.up + Vector3.left;
                    break;
                case 9:
                    directionVector = Vector3.up + Vector3.right;
                    break;
            }
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
            return false;
        }
        return true;
    }
    void stopWalk()
    {
        direction = Vector2.zero;
        animator.SetFloat("Speed", 0f);
        rb.position = (Vector2)realPos;
        if (isAttacking && pathfinding.waypoints.Count > 2)
        {
            nextStep = pathfinding.waypoints[1];
            nextStep.x = (float)Math.Round(nextStep.x);
            nextStep.y = (float)Math.Round(nextStep.y);
            nextStep = nextStep - realPos;
            nextStep.z = 0;
        }
        pathfinding.UpdatePath();
        state = 5;
    }


    void DoWalk()
    {
        if (Vector2.Distance(target, (Vector2)rb.position) < stoppingDis || Vector2.Distance(target, (Vector2)rb.position) > 1.5f)
            stopWalk();
        else
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);
            animator.SetInteger("Direction", dir);
            rb.position = rb.position + (Vector3)direction * speed;
        }
    }
}




