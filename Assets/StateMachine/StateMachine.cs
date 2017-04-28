using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>Состояние автомата</summary>
public abstract class FsmState
{
    protected GameObject CurObject;
    protected Animator CurAnimator;
    

    /// <summary>Текущее состояние</summary>
    public Enum CurState { get; private set; }

    /// <summary>Логика для входа в новое состояние </summary>
    /// <param name="prvState">предыдущее состояние</param>
    public virtual void StateEnter(Enum prvState) { }

    /// <summary>Логика для выхода из текущего состояния  в новое</summary>
    /// <param name="newState">будущее состояние</param>
    public virtual void StateExit(Enum newState) { }

    /// <summary>Вызывается все время пока данное состояние активно </summary>
    public abstract void Update();

    /// <summary>Конструктор</summary>
    public FsmState(Enum state, GameObject curObj)
    {
        CurObject = curObj;
        CurAnimator = CurObject.GetComponent<Animator>();
        CurState = state;
    }
}

public class StateMachine
{
    /// <summary>Таблица состояний автомата</summary>
    //по х - сообщения 
    //по y - состояния 
    public FsmState[,] StateTable;
    //new FsmState[Enum.GetNames(typeof(FsmEnumMsg)).Length, Enum.GetNames(typeof(FsmEnumState)).Length];

    /// <summary>Предыдущее состояние</summary>
    public Enum PrvState { get; private set; }

    /// <summary>Текущее состояние</summary>
    public Enum CurState { get; private set; }

    /// <summary>Ссылка на текущее состояние</summary>
    public FsmState State;

    /// <summary>Сменить состояние </summary>
    public void ChangeState(Enum transition)
    {
        Debug.Log("__PrvState:=" + PrvState + "   CurState:=" + CurState + "    TransitionMsg:=" + transition + "\n");

        //если текущее состояние не изменилось то ниче не делаем
        if (CurState == StateTable[Convert.ToInt32(transition), Convert.ToInt32(CurState)].CurState) return;

        Debug.Log("PrvState:=" + PrvState + "   CurState:="+CurState + "    TransitionMsg:=" + transition + "   NewState:=" + StateTable[Convert.ToInt32(transition), Convert.ToInt32(CurState)].CurState+"\n");

        //выполнить выход из текущего состояния
        State.StateExit(transition);
        //взять новое состояние
        State = StateTable[Convert.ToInt32(transition), Convert.ToInt32(CurState)];
        //выполнить вход в новое состояние
        State.StateEnter(PrvState);

        PrvState = CurState;
        CurState = State.CurState;
    }

    //конструктор
    public StateMachine(FsmState[,] stateTable, Enum state)
    {
        StateTable = stateTable;
        CurState = state;
        PrvState = state;
        State = StateTable[0, 0];
    }



}
