using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;
public class PrepareState : State<State_Manager>
{
    private static PrepareState _instance;

    private PrepareState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static PrepareState Instance {
        get {
            if (_instance == null)
            {
                new PrepareState();
            }
            return _instance;
        }

    }
    public override void EnterState(State_Manager _owner)
    {
        seconds = 10;
        if (EndState.Instance.SecondTurn)
        {
            _owner.SwitchRoles();
        }
        Debug.Log("Entering Player State");
    }

    public override void ExitState(State_Manager _owner)
    {
        Debug.Log("Exiting Player State");
    }

    public override string ReturnText()
    {
        return ("Time to to take position: ");
    }
    public override void UpdateState(State_Manager _owner)
    {
        if (seconds < 0)
        {
            _owner.stateMachine.ChangeState(PlayState.Instance);
        }
    }
}
