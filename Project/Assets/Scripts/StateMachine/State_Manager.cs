using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using StateMachine;
using System;
public class State_Manager : NetworkBehaviour
{
    public RoleManager RM;
    public bool switchState = true;
    public float gameTimer;

    [SyncVar]
    public int seconds = 0;
    public Text UItext;

    private State<State_Manager> changeState;

    public delegate void RoleRandomizer();
    [SyncEvent]
    public static event RoleRandomizer EventRandomizeRoles;
    [SyncEvent]
    public static event RoleRandomizer EventRandomizeRoles_RPC;
    [SyncEvent]
    public static event RoleRandomizer EventSwitchRoles_RPC;
    


    //[SyncVar]
    //private State<State_Manager> newState;


    public StateMachine<State_Manager> stateMachine { get; set; }
    private void Start()
    {
        Debug.Log("Starting State Machine");
        RM = GetComponent<RoleManager>();
        stateMachine = new StateMachine<State_Manager>(this);
        stateMachine.ChangeState(PrepareState.Instance);
        seconds = stateMachine.currentState.seconds;
        //Debug.Log("State seconds" + stateMachine.currentState.seconds);
        gameTimer = Time.time;
        //EventGet += ChangeState;
    }
    public void ChangeTurn()
    {
        if(!isServer)
        {
            return;
        }
        stateMachine.ChangeState(PlayState.Instance);
        seconds = stateMachine.currentState.seconds;
    }

    public void ChangeState(State<State_Manager> _state)
    {
        if(!isServer)
        {
            return;
        }
        changeState = _state;
        CmdChangeState();
    }

    [Command]
    public void CmdChangeState()
    {
        RpcChangeState();
    }
    [ClientRpc]
    public void RpcChangeState()
    {
        stateMachine.ChangeState(changeState);
    }
    private void Update()
    {
        Debug.Log(stateMachine.currentState);
        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            stateMachine.currentState.seconds--;
            seconds = stateMachine.currentState.seconds;
            //Debug.Log("Seconds " + seconds);
        }
        UItext.text = "" + stateMachine.currentState.ReturnText() + stateMachine.currentState.seconds;
        stateMachine.Update();
    }

    public void SetRoles()
    {
        if (isServer)
        {
            CmdSetRoles();
        }
    }

    public void SwitchRoles()
    {
       CmdSwitchRoles();
    }
    public void CmdSetRoles()
    {
        Cmd_Randomize_Roles_RPC();
        Debug.Log("test");
    }

    public void CmdSwitchRoles()
    {
        Cmd_Switch_Roles_RPC();
        Debug.Log("test");
    }

    [Command]
    public void Cmd_Randomize_Roles_RPC()
    {
        EventRandomizeRoles_RPC();
    }

    [Command]
    public void Cmd_Switch_Roles_RPC()
    {
        if (isServer)
        {
            EventSwitchRoles_RPC();
        }
    }
}
