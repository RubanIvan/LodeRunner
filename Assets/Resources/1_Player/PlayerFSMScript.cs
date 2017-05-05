﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Список переходов автомата</summary>
public enum PlayerTransition
{
    Idle,
    Left,
    Right,
    Up,
    Down,
    //Fire,


    Fall,
    Ground,
    LandingEnd,

    RopeTake,

    //FireLeft,
    //FireRight,
    //OnStairsEnter,
    //OnStairsExit,
    //OnRopeEnter,
    //OnRopeExit,


}

/// <summary>Список состояний автомата</summary>
public enum PlayerState
{
    Stand,

    /// <summary>падение</summary>
    Falling,
    /// <summary>презимление</summary>
    Landing,

    RunLeft,
    RunRight,


    RopeIdle,
    RopeLeft,
    RopeRight,
    
    //StairsIdle,
    //StairsUp,
    //StairsDown,
    
}


public partial class PlayerScript : MonoBehaviour
{
    ///// <summary>Инициализация конечного автомата</summary>
    public void SatetItit()
    {
        PsmState stand = new PS_Stand(gameObject);
        PsmState falling = new PS_Falling(gameObject);
        PsmState landing = new PS_Landing(gameObject);
        PsmState runLeft = new PS_RunLeft(gameObject);
        PsmState runRight = new PS_RunRight(gameObject);
        PsmState ropeIdle = new PS_RopeIdle(gameObject);
        PsmState ropeLeft = new PS_RopeLeft(gameObject);
        PsmState ropeRight = new PS_RopeRight(gameObject);


        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.Stand] = stand;
        PlayerStateTable[(int)PlayerTransition.Fall, (int)PlayerState.Stand] = falling;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.Stand] = runLeft;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.Stand] = runRight;
        PlayerStateTable[(int)PlayerTransition.Up, (int)PlayerState.Stand] = stand;
        PlayerStateTable[(int)PlayerTransition.Down, (int)PlayerState.Stand] = stand;



        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.Falling] = falling;
        PlayerStateTable[(int)PlayerTransition.Fall, (int)PlayerState.Falling] = falling;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.Falling] = falling;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.Falling] = falling;
        PlayerStateTable[(int)PlayerTransition.Down, (int)PlayerState.Falling] = falling;
        PlayerStateTable[(int)PlayerTransition.Up, (int)PlayerState.Falling] = falling;
        PlayerStateTable[(int)PlayerTransition.Ground, (int)PlayerState.Falling] = landing;
        PlayerStateTable[(int)PlayerTransition.RopeTake, (int)PlayerState.Falling] = ropeIdle;


        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.Landing] = landing;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.Landing] = landing;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.Landing] = landing;
        PlayerStateTable[(int)PlayerTransition.Up, (int)PlayerState.Landing] = landing;
        PlayerStateTable[(int)PlayerTransition.Down, (int)PlayerState.Landing] = landing;
        PlayerStateTable[(int)PlayerTransition.LandingEnd, (int)PlayerState.Landing] = stand;

        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.RunLeft] = stand;
        PlayerStateTable[(int)PlayerTransition.Fall, (int)PlayerState.RunLeft] = falling;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.RunLeft] = runLeft;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.RunLeft] = runRight;

        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.RunRight] = stand;
        PlayerStateTable[(int)PlayerTransition.Fall, (int)PlayerState.RunRight] = falling;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.RunRight] = runLeft;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.RunRight] = runRight;
        

        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.RopeIdle] = ropeIdle;
        //PlayerStateTable[(int)PlayerTransition.Fall, (int)PlayerState.RopeIdle] = ropeIdle;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.RopeIdle] = ropeLeft;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.RopeIdle] = ropeRight;
        PlayerStateTable[(int)PlayerTransition.Down, (int)PlayerState.RopeIdle] = falling;

        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.RopeLeft] = ropeIdle;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.RopeLeft] = ropeLeft;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.RopeLeft] = ropeRight;
        PlayerStateTable[(int)PlayerTransition.Down, (int)PlayerState.RopeLeft] = falling;

        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.RopeRight] = ropeIdle;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.RopeRight] = ropeLeft;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.RopeRight] = ropeRight;
        PlayerStateTable[(int)PlayerTransition.Down, (int)PlayerState.RopeRight] = falling;





    }


    public class PS_Stand : PsmState
    {
        public PS_Stand(GameObject curObj) : base(PlayerState.Stand, curObj) { }

        public override void StateEnter(Enum prvState)
        {
            Animator.Play("StandAnimation");
        }

        public override void Update()
        {
            player.GroundCheck();
        }
    }

    public class PS_Falling : PsmState
    {
        public PS_Falling(GameObject curObj) : base(PlayerState.Falling, curObj){}

        private RaycastHit2D hitleft;
        private RaycastHit2D hitright;

        private Vector2 LeftCorner = new Vector2(-7.5f, -16f);
        private Vector2 RightCorner = new Vector2(7.5f, -16f);

        public override void StateEnter(Enum prvState)
        {
            Animator.Play("Fall1Animation");
        }

        public override void Update()
        {

            hitleft = Physics2D.Raycast(player.gameObject.transform.position, LeftCorner, 18.6f, Const.WallMask | Const.StairsMask);
            hitright = Physics2D.Raycast(player.gameObject.transform.position, RightCorner, 18.6f, Const.WallMask | Const.StairsMask);


            if ((hitleft.collider != null) || (hitright.collider != null))
            {
                player.PlayerFST.ChangeState(PlayerTransition.Ground);

                if(hitleft.collider != null) player.gameObject.transform.localPosition = new Vector3(player.gameObject.transform.localPosition.x,hitleft.collider.gameObject.transform.localPosition.y+16f);
                if (hitright.collider != null) player.gameObject.transform.localPosition = new Vector3(player.gameObject.transform.localPosition.x, hitright.collider.gameObject.transform.localPosition.y+16f );
                return;
            }

            player.gameObject.transform.localPosition = new Vector3(player.gameObject.transform.localPosition.x, player.gameObject.transform.localPosition.y - Const.Gravity * Time.deltaTime);
        }

        public override void  OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("RopeTake");
            if (other.tag == "Rope") player.PlayerFST.ChangeState(PlayerTransition.RopeTake);
        }
    }

    public class PS_Landing : PsmState
    {
        public PS_Landing(GameObject curObj) : base(PlayerState.Landing, curObj) { }

        public override void StateEnter(Enum prvState)
        {
            Animator.Play("LandingHiAnimation");
        }
    }

    /// <summary>Вызывается по коончании анимации вставания </summary>
    public void LandingEnd()
    {
        PlayerFST.ChangeState(PlayerTransition.LandingEnd);
    }

    public class PS_RunLeft : PsmState
    {
        private RaycastHit2D hitleft;


        public PS_RunLeft(GameObject curObj) : base(PlayerState.RunLeft, curObj) { }

        public override void StateEnter(Enum prvState)
        {
            Animator.Play("RunAnimation");
        }

        public override void Update()
        {
            //проверка можем ли мы идти в эту сторону
            hitleft = Physics2D.Raycast(player.gameObject.transform.position, Vector2.left, 18.0f, Const.WallMask);
            if (hitleft.collider != null) return;

            player.gameObject.transform.localPosition = new Vector3(player.gameObject.transform.localPosition.x - Const.PlayerRunSpeed * Time.deltaTime, player.gameObject.transform.localPosition.y);

            player.GroundCheck();
        }
    }

    public class PS_RunRight : PsmState
    {
        public PS_RunRight(GameObject curObj) : base(PlayerState.RunRight, curObj) { }

        private RaycastHit2D hitright;

        public override void StateEnter(Enum prvState)
        {
            Animator.Play("RunAnimation");
        }

        public override void Update()
        {
            //проверка можем ли мы идти в эту сторону
            hitright = Physics2D.Raycast(player.gameObject.transform.position, Vector2.right, 18.0f, Const.WallMask);
            if (hitright.collider != null) return;

            player.gameObject.transform.localPosition = new Vector3(player.gameObject.transform.localPosition.x + Const.PlayerRunSpeed * Time.deltaTime, player.gameObject.transform.localPosition.y);

            player.GroundCheck();
        }
    }

    public class PS_RopeIdle : PsmState
    {
        public PS_RopeIdle(GameObject curObj) : base(PlayerState.RopeIdle, curObj){}

        public override void StateEnter(Enum prvState)
        {
            Animator.Play("RopeIdleAnimation");
        }
    }

    public class PS_RopeLeft : PsmState
    {
        private RaycastHit2D hitleft;


        public PS_RopeLeft(GameObject curObj) : base(PlayerState.RopeLeft, curObj) { }

        public override void StateEnter(Enum prvState)
        {
            Animator.Play("RopeMoveAnimation");
        }

        public override void Update()
        {
            ////проверка можем ли мы идти в эту сторону
            //hitleft = Physics2D.Raycast(CurObject.transform.position, Vector2.left, 18.0f, Const.WallMask);
            //if (hitleft.collider != null && hitleft.collider.tag == "Wall") return;

            player.gameObject.transform.localPosition = new Vector3(player.gameObject.transform.localPosition.x - Const.PlayerRopeSpeed * Time.deltaTime, player.gameObject.transform.localPosition.y);
        }
    }

    public class PS_RopeRight : PsmState
    {
        private RaycastHit2D hitleft;


        public PS_RopeRight(GameObject curObj) : base(PlayerState.RopeRight, curObj) { }

        public override void StateEnter(Enum prvState)
        {
            Animator.Play("RopeMoveAnimation");
        }

        public override void Update()
        {
            ////проверка можем ли мы идти в эту сторону
            //hitleft = Physics2D.Raycast(CurObject.transform.position, Vector2.left, 18.0f, Const.WallMask);
            //if (hitleft.collider != null && hitleft.collider.tag == "Wall") return;

            player.gameObject.transform.localPosition = new Vector3(player.gameObject.transform.localPosition.x + Const.PlayerRopeSpeed * Time.deltaTime, player.gameObject.transform.localPosition.y);
        }
    }

}
