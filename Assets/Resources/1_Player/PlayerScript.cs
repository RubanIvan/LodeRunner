using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerScript : MonoBehaviour
{
    
    private SpriteRenderer Sprite;

    public GameMasterScript GameMaster;


    //RayCast ground
    private Vector2 LeftCorner = new Vector2(-7.5f, -16f);
    private Vector2 RightCorner = new Vector2(7.5f, -16f);
    private RaycastHit2D hitleft;
    private RaycastHit2D hitright;

    /// <summary>Конечный автомат состояний </summary>
    public StateMachine PlayerFST;
    public FsmState[,] PlayerStateTable = new FsmState[Enum.GetNames(typeof(PlayerTransition)).Length, Enum.GetNames(typeof(PlayerState)).Length];

    /// <summary>движение в горизонтальной плоскости </summary>
    public float moveH;

    void Awake()
    {
        SatetItit();
        PlayerFST = new StateMachine(PlayerStateTable, PlayerState.Idle);
        Sprite = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.DrawRay(transform.position, LeftCorner * 1.1f, Color.green);
        //Debug.DrawRay(transform.position, RightCorner * 1.1f, Color.green);

        hitleft = Physics2D.Raycast(transform.position, LeftCorner, 18.6f);
        hitright = Physics2D.Raycast(transform.position, RightCorner, 18.6f);

        moveH = Input.GetAxis("Horizontal");

        if ((hitleft.collider == null) && (hitright.collider == null))
        {
            PlayerFST.ChangeState(PlayerTransition.Fail);
        }
        else
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
        else
        {
            PlayerFST.ChangeState(PlayerTransition.Idle);
        }


        PlayerFST.State.Update();


    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.tag);

        if (other.tag=="Chest")
        {
            GameMaster.OnchestCollect();
            GameObject.Destroy(other.gameObject);

        }
    }

   

}
