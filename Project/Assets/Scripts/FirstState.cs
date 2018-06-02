using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;
public class FirstState : State<State_Manager>
{
    private static FirstState _instance;

    private FirstState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FirstState Instance
    {
        get {
            if(_instance == null)
            {
                new FirstState();
            }
            return _instance;
        }

    }
    public override void EnterState(State_Manager _owner)
    {
        seconds = 12;
        Debug.Log("Entering First State");
    }

    public override void ExitState(State_Manager _owner)
    {
        Debug.Log("Exiting First State");
    }

    public override string ReturnText()
    {
        return ("Time to to take position: " + seconds);
    }

    public override void UpdateState(State_Manager _owner)
    {
        if (!_owner.switchState)
        {
            _owner.stateMachine.ChangeState(SecondState.Instance);
        }
    }
}
