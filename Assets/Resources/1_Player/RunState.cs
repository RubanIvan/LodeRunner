using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public partial class PlayerScript : MonoBehaviour
{

    public class PS_Run : PsmState
    {
        protected Vector2 Side;
        protected float Dir;

        public PS_Run(PlayerState state, GameObject Obj) : base(state, Obj){}

        public override void StateEnter(Enum prvState)
        {
            Animator.Play("RunAnimation");
        }

        public override void Update()
        {
            //проверка можем ли мы идти в эту сторону
            hit = Physics2D.Raycast(player.gameObject.transform.position, Side, 18.0f, Const.WallMask);
            if (hit.collider != null) return;

            player.gameObject.transform.localPosition = new Vector3(player.gameObject.transform.localPosition.x + Dir * Const.PlayerRunSpeed * Time.deltaTime, player.gameObject.transform.localPosition.y);

            player.GroundCheck();
        }

    }

    public class PS_RunLeft : PS_Run
    {
        public PS_RunLeft(GameObject curObj) : base(PlayerState.RunLeft, curObj)
        {
            Side = Vector2.left;
            Dir = -1;
        }
    }

    public class PS_RunRight : PS_Run
    {
        public PS_RunRight(GameObject curObj) : base(PlayerState.RunLeft, curObj)
        {
            Side = Vector2.right;
            Dir = 1;
        }
    }
}
