﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerScript : MonoBehaviour
{

    private SpriteRenderer Sprite;

    public GameMasterScript GameMaster;

    ////RayCast ground
    private Vector2 LeftCorner = new Vector2(-7.5f, -16f);
    private Vector2 RightCorner = new Vector2(7.5f, -16f);
    private RaycastHit2D hitleft;
    private RaycastHit2D hitright;

    private float moveH;
    private float moveV;

    ///// <summary>Конечный автомат состояний </summary>
    public StateMachine PlayerFST;
    public PsmState[,] PlayerStateTable = new PsmState[Enum.GetNames(typeof(PlayerTransition)).Length, Enum.GetNames(typeof(PlayerState)).Length];

    void Awake()
    {
        SatetItit();
        PlayerFST = new StateMachine(PlayerStateTable, PlayerState.Stand);
        Sprite = GetComponent<SpriteRenderer>();
    }

   
    //// Update is called once per frame
    void Update()
    {

        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        if (moveH > 0)
        {
            PlayerFST.ChangeState(PlayerTransition.Right);
            Sprite.flipX = true;
        }
        else if (moveH < 0)
        {
            PlayerFST.ChangeState(PlayerTransition.Left);
            Sprite.flipX = false;
        }
        else if (moveV > 0)
        {
            PlayerFST.ChangeState(PlayerTransition.Up);
        }
        else if (moveV < 0)
        {
            PlayerFST.ChangeState(PlayerTransition.Down);
        }
        else
        {
            PlayerFST.ChangeState(PlayerTransition.Idle);
        }


        PlayerFST.State.Update();


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerFST.State.OnTriggerEnter2D(other);

        if (other.tag == "Chest")
        {
            GameMaster.OnchestCollect();
            GameObject.Destroy(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerFST.State.OnTriggerExit2D(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerFST.State.OnTriggerStay2D(other);
    }
    

    public void GroundCheck()
    {
        hitleft = Physics2D.Raycast(transform.position, LeftCorner, 18.6f, Const.WallMask | Const.StairsMask);
        hitright = Physics2D.Raycast(transform.position, RightCorner, 18.6f, Const.WallMask | Const.StairsMask);


        if ((hitleft.collider == null) && (hitright.collider == null))
        {
            PlayerFST.ChangeState(PlayerTransition.Fall);
        }
    }

}
