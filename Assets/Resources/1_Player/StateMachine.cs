using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>Состояние автомата</summary>
public abstract class PsmState
{
    protected PlayerScript player;
    protected Animator Animator;

    /// <summary>Текущее состояние</summary>
    public PlayerState CurState { get; private set; }

    /// <summary>Логика для входа в новое состояние </summary>
    /// <param name="prvState">предыдущее состояние</param>
    public virtual void StateEnter(Enum prvState) { }

    /// <summary>Логика для выхода из текущего состояния  в новое</summary>
    /// <param name="newState">будущее состояние</param>
    public virtual void StateExit(Enum newState) { }

    /// <summary>Cобытия колайдеров</summary>
    public virtual void OnTriggerEnter2D(Collider2D other) { }
    public virtual void OnTriggerExit2D(Collider2D other) { }
    public virtual void OnTriggerStay2D(Collider2D other) { }

    /// <summary>Вызывается все время пока данное состояние активно </summary>
    public virtual void Update(){}

    /// <summary>Конструктор</summary>
    public PsmState(PlayerState state,GameObject Obj)
    {
        Animator = Obj.GetComponent<Animator>();
        player = Obj.GetComponent<PlayerScript>();
        CurState = state;
    }
}

public class StateMachine
{
    /// <summary>Таблица состояний автомата</summary>
    //по х - сообщения 
    //по y - состояния 
    public PsmState[,] StateTable;


    /// <summary>Предыдущее состояние</summary>
    public PlayerState PrvState { get; private set; }

    /// <summary>Текущее состояние</summary>
    public PlayerState CurState { get; private set; }

    /// <summary>Ссылка на текущее состояние</summary>
    public PsmState State;

    /// <summary>Сменить состояние </summary>
    public void ChangeState(PlayerTransition transition)
    {
        //Debug.Log("CurState:=" + CurState + "    TransitionMsg:=" + transition + "\n");

        //если текущее состояние не изменилось то ниче не делаем
        if (CurState == StateTable[(int)transition, (int)CurState].CurState) return;

        Debug.Log("PrvState:=" + PrvState + "   CurState:=" + CurState + "    TransitionMsg:=" + transition + "   NewState:=" + StateTable[Convert.ToInt32(transition), Convert.ToInt32(CurState)].CurState + "\n");

        //выполнить выход из текущего состояния
        State.StateExit(transition);
        //взять новое состояние
        State = StateTable[(int)transition, (int)CurState];
        //выполнить вход в новое состояние
        State.StateEnter(PrvState);

        PrvState = CurState;
        CurState = State.CurState;
    }

    //конструктор
    public StateMachine(PsmState[,] stateTable, PlayerState state)
    {
        StateTable = stateTable;
        CurState = state;
        PrvState = state;
        State = StateTable[0, 0];
    }



}
