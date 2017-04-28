using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Список переходов автомата</summary>
public enum PlayerTransition
{
    Idle,
    Fail,
    Ground,
    Landing,
    LandingEnd,
    

    Left,
    Right,
    //Down,
    //Up,
    //Fire,
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
    Idle,

    /// <summary>падение</summary>
    Fail,
    /// <summary>презимление</summary>
    Landing,

    RunLeft,
    RunRight,

    //StairsIdle,
    //StairsUp,
    //StairsDown,

    //RopeIdle,
    //RopeLeft,
    //RopeRight,

}


public partial class PlayerScript : MonoBehaviour
{
    /// <summary>Инициализация конечного автомата</summary>
    public void SatetItit()
    {
        FsmState idle = new PS_Idle(gameObject);
        FsmState fail = new PS_Fail(gameObject);
        FsmState landing = new PS_Landing(gameObject);
        FsmState left = new PS_RunLeft(gameObject);
        FsmState right = new PS_RunRight(gameObject);

        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.Idle] = idle;
        PlayerStateTable[(int)PlayerTransition.Fail, (int)PlayerState.Idle] = fail;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.Idle] = left;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.Idle] = right;

        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.Fail] = landing;
        PlayerStateTable[(int)PlayerTransition.Fail, (int)PlayerState.Fail] = fail;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.Fail] = fail;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.Fail] = fail;

        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.Landing] = landing;
        PlayerStateTable[(int)PlayerTransition.LandingEnd, (int)PlayerState.Landing] = idle;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.Landing] = landing;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.Landing] = landing;


        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.RunLeft] = idle;
        PlayerStateTable[(int)PlayerTransition.Fail, (int)PlayerState.RunLeft] = fail;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.RunLeft] = left;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.RunLeft] = idle;

        PlayerStateTable[(int)PlayerTransition.Idle, (int)PlayerState.RunRight] = idle;
        PlayerStateTable[(int)PlayerTransition.Fail, (int)PlayerState.RunRight] = fail;
        PlayerStateTable[(int)PlayerTransition.Left, (int)PlayerState.RunRight] = idle;
        PlayerStateTable[(int)PlayerTransition.Right, (int)PlayerState.RunRight] = right;


    }


    public class PS_Idle : FsmState
    {
        public PS_Idle(GameObject curObj) : base(PlayerState.Idle, curObj) { }

        public override void StateEnter(Enum prvState)
        {
            CurAnimator.Play("IdleAnimation");
        }

        public override void Update()
        {
            return;
        }
    }

    public class PS_Fail : FsmState
    {
        public PS_Fail(GameObject curObj) : base(PlayerState.Fail, curObj) { }

        public override void StateEnter(Enum prvState)
        {
            CurAnimator.Play("FailAnimation");
        }

        public override void Update()
        {
           CurObject.transform.localPosition=new Vector3(CurObject.transform.localPosition.x, CurObject.transform.localPosition.y-Const.Gravity*Time.deltaTime);
        }
    }

    public class PS_Landing : FsmState
    {
        public PS_Landing(GameObject curObj) : base(PlayerState.Landing, curObj) { }

        public override void StateEnter(Enum prvState)
        {
            CurAnimator.Play("LandingAnimation");
        }

        public override void Update()
        {
            return;
        }
    }

    /// <summary>Вызывается по коончании анимации вставания </summary>
    public void LandingEnd()
    {
        PlayerFST.ChangeState(PlayerTransition.LandingEnd);
    }

    public class PS_RunLeft : FsmState
    {
        public PS_RunLeft(GameObject curObj) : base(PlayerState.RunLeft, curObj) { }

        public override void StateEnter(Enum prvState)
        {
            CurAnimator.Play("RunAnimation");
        }

        public override void Update()
        {
            CurObject.transform.localPosition = new Vector3(CurObject.transform.localPosition.x-Const.PlayerSpeed*Time.deltaTime, CurObject.transform.localPosition.y);
        }
    }

    public class PS_RunRight : FsmState
    {
        public PS_RunRight(GameObject curObj) : base(PlayerState.RunRight, curObj) { }

        public override void StateEnter(Enum prvState)
        {
            CurAnimator.Play("RunAnimation");
        }

        public override void Update()
        {
            CurObject.transform.localPosition = new Vector3(CurObject.transform.localPosition.x + Const.PlayerSpeed * Time.deltaTime, CurObject.transform.localPosition.y);
        }
    }

}
