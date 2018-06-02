using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;
public class SecondState : State<State_Manager>
{
    private static SecondState _instance;

    private SecondState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static SecondState Instance {
        get {
            if (_instance == null)
            {
                new SecondState();
            }
            return _instance;
        }

    }
    public override void EnterState(State_Manager _owner)
    {
        Debug.Log("Entering Second State");
    }

    public override void ExitState(State_Manager _owner)
    {
        Debug.Log("Exiting Second State");
    }

    public override string ReturnText()
    {
        throw new NotImplementedException();
    }

    public override void UpdateState(State_Manager _owner)
    {
        if(_owner.switchState)
        {
            _owner.stateMachine.ChangeState(FirstState.Instance);
        }
    }
}
