using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction;
    public Animator animator;
    public Vector2 target = new Vector2(0, 0);
    public Vector3 realPos;
    public CircleCollider2D circleCollider;
    public float speed = 0.1f;
    public int state = 0; //1-9 by the side keyboard
    public int dir=2;
    public GameObject unwalkabletext;
    public List<GameObject> specialFloorsCollision = new List<GameObject>();
    float stoppingDis = 0.2f;
    public GameObject energyEffect;


    // Start is called before the first frame update

    private void Start()
    {
        Instantiate(energyEffect, this.transform.position, Quaternion.identity);
        circleCollider = GetComponent<CircleCollider2D>();
        target = new Vector2(rb.position.x, rb.position.y);
        realPos = new Vector3((float)Math.Round(rb.position.x), (float)Math.Round(rb.position.y), rb.transform.position.z);
        
    }



    void FixedUpdate()
    {
        stoppingDis = 0.15f * (speed / 6f); //change stopping distance reletavely so if speed = 6f, stoppingDis = 0.15f
        if (state == 0)
            if (Input.GetKey(KeyCode.LeftControl))
            {
                //look left
                if (Input.GetKey(KeyCode.LeftArrow))
                {

                    animator.SetFloat("Horizontal", direction.x);
                    animator.SetFloat("Vertical", direction.y);
                    animator.SetFloat("Speed", direction.sqrMagnitude);
                    animator.SetInteger("Direction", 4);
                    dir = 4;
                }
                //look right
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    animator.SetFloat("Horizontal", direction.x);
                    animator.SetFloat("Vertical", direction.y);
                    animator.SetFloat("Speed", direction.sqrMagnitude);
                    animator.SetInteger("Direction", 6);
                    dir = 6;

                }
                //look down
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    animator.SetFloat("Horizontal", direction.x);
                    animator.SetFloat("Vertical", direction.y);
                    animator.SetFloat("Speed", direction.sqrMagnitude);
                    animator.SetInteger("Direction", 2);
                    dir = 2;
                }
                //look up
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    animator.SetFloat("Horizontal", direction.x);
                    animator.SetFloat("Vertical", direction.y);
                    animator.SetFloat("Speed", direction.sqrMagnitude);
                    animator.SetInteger("Direction", 8);
                    dir = 8;
                }

            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (checkMovement(Vector3.right))
                    {
                        updateRealPos(Vector3.right);
                        state = 6;
                        target = rb.position + Vector2.right;
                    }

                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (checkMovement(Vector3.left))
                    {
                        updateRealPos(Vector3.left);
                        state = 4;
                        target = rb.position + Vector2.left;
                    }

                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (checkMovement(Vector3.up))
                    {
                        updateRealPos(Vector3.up);
                        state = 8;
                        target = rb.position + Vector2.up;
                    }

                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (checkMovement(Vector3.down))
                    {
                        updateRealPos(Vector3.down);
                        state = 2;
                        target = rb.position + Vector2.down;
                    }

                }
                else if (Input.GetKey(KeyCode.End))
                {
                    if (checkMovement(Vector3.left + Vector3.down))
                    {
                        updateRealPos(Vector3.left + Vector3.down);
                        state = 1;
                        target = rb.position + Vector2.left + Vector2.down;
                    }

                }
                else if (Input.GetKey(KeyCode.PageDown))
                {
                    if (checkMovement(Vector3.right + Vector3.down))
                    {
                        updateRealPos(Vector3.right + Vector3.down);
                        state = 3;
                        target = rb.position + Vector2.right + Vector2.down;
                    }

                }
                else if (Input.GetKey(KeyCode.Home))
                {
                    if (checkMovement(Vector3.left + Vector3.up))
                    {
                        updateRealPos(Vector3.left + Vector3.up);
                        state = 7;
                        target = rb.position + Vector2.left + Vector2.up;
                    }
                }
                else if (Input.GetKey(KeyCode.PageUp))
                {
                    if (checkMovement(Vector3.right + Vector3.up))
                    {
                        updateRealPos(Vector3.right + Vector3.up);
                        state = 9;
                        target = rb.position + Vector2.right + Vector2.up;
                    }

                }
            }
        else
            dir = state;



        switch (state)
        {
            case 6:
                moveRight();
                break;

            case 4:
                moveLeft();
                break;

            case 8:
                moveUp();
                break;

            case 2:
                moveDown();
                break;
            case 1:
                moveDownLeft();
                break;
            case 3:
                moveDownRight();
                break;
            case 7:
                moveUpLeft();
                break;
            case 9:
                moveUpRight();
                break;
        }

        circleCollider.offset = ((Vector2)realPos - rb.position);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        {
            Debug.Log(col.name);
            if (col.GetComponent<specialFloor>() != null)
            {
                if (!specialFloorsCollision.Contains(col)) specialFloorsCollision.Add(col);
            }
            else if (col.tag == "creature")
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                stopWalk();
            }
        }


    }

    void stopWalk()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        direction = Vector2.zero;
        animator.SetFloat("Speed", 0);
        rb.position = realPos;
        state = 0;

        foreach (GameObject col in specialFloorsCollision)
        {
            Debug.Log(col.name);
            if (col.GetComponent<specialFloor>() != null)
            {
                col.GetComponent<specialFloor>().Do(this.gameObject);
                specialFloorsCollision.Remove(col);
                return;
            }
        }

    }

    void doWalk()
    {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
        animator.SetInteger("Direction", dir);
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        if (Math.Abs(target.x - rb.position.x) > 3 || Math.Abs(target.x - rb.position.x) > 3)
            stopWalk();
    }
    
    
    void moveRight()
    {
        direction.x = 1;
        direction.y = 0;
        if (Math.Abs(target.x - rb.position.x) < stoppingDis)
            stopWalk();
        else
        {
            direction.x = 1;
            direction.y = 0;
        }
        doWalk();
    }

    Boolean isWalkable(GameObject step)
    {
        Item item = step.GetComponent<Item>();
        return !(step.tag != "hole" && ((item == null) || !item.isWalkable()));
    }
    Boolean checkMovement(Vector3 directionVector)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(realPos + directionVector, Vector3.down, 0f);
        if (hit.Length > 0)
        {
            foreach (RaycastHit2D cast in hit)
            {

                if (!isWalkable(cast.collider.gameObject))
                {
                    GameObject newText = GameObject.Find("unwalkableText");
                    if (newText == null)
                    {
                        GameObject tempText = GameObject.Find("unwalkableText(Clone)");
                        if (!tempText)
                            Instantiate(unwalkabletext, GameObject.Find("ScreenUI").transform);
                    }
                    return false;

                }
                

            }

        }
        return true;
    }
    void updateRealPos(Vector3 direction)
    {
        realPos = realPos + direction;
    }
    void moveLeft()
    {

        if (Math.Abs(target.x - rb.position.x)<stoppingDis)
            stopWalk();

        else
        {
            direction.x = -1;
            direction.y = 0;
        }
        doWalk();
    }
    void moveUp()
    {

    if (Math.Abs(target.y - rb.position.y)<stoppingDis)
        stopWalk();
    else
    {
        direction.x = 0;
        direction.y = 1;
    }
        doWalk();
    }
    void moveDown()
    {

        if (Math.Abs(target.y - rb.position.y)<stoppingDis)
            stopWalk();
        else
        {
            direction.x = 0;
            direction.y = -1;
        }
        doWalk();
    }
    void moveDownLeft()
    {
        direction.x = -1;
        direction.y = -1;
        if (Math.Abs(target.x - rb.position.x)<stoppingDis)
            stopWalk();
        else
        {
            direction.x = -1;
            direction.y = -1;
        }
        doWalk();
    }
    void moveDownRight()
    {
        direction.x = 1;
        direction.y = -1;
        if (Math.Abs(target.x - rb.position.x)<stoppingDis)
            stopWalk();
        else
        {
            direction.x = 1;
            direction.y = -1;
        }
        doWalk();
    }
    void moveUpLeft()
    {
        direction.x = -1;
        direction.y = 1;
        if (Math.Abs(target.x - rb.position.x)<stoppingDis)
            stopWalk();
        else
        {
            direction.x = -1;
            direction.y = 1;
        }
        doWalk();
    }
    void moveUpRight()
    {
        direction.x = 1;
        direction.y = 1;
        if (Math.Abs(target.x - rb.position.x)<stoppingDis)
            stopWalk();
        else
        {
            direction.x = 1;
            direction.y = 1;
        }
        doWalk();
    }

}
