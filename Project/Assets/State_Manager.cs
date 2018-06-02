using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using StateMachine;
using System;
public class State_Manager : NetworkBehaviour
{

    public bool switchState = true;
    public float gameTimer;
    [SyncVar]
    public int seconds = 0;

    public StateMachine<State_Manager> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<State_Manager>(this);
        stateMachine.ChangeState(PlayerState.Instance);
        seconds = stateMachine.currentState.seconds;
        Debug.Log("State seconds" + stateMachine.currentState.seconds);
        gameTimer = Time.time;
    }
    public void ChangeTurn()
    {
        stateMachine.ChangeState(FirstState.Instance);
        seconds = stateMachine.currentState.seconds;
    }
    private void Update()
    {
        Debug.Log(stateMachine.currentState);
        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            stateMachine.currentState.seconds--;
            seconds = stateMachine.currentState.seconds;
            Debug.Log("Seconds " + seconds);
        }
        if (seconds <= 0)
        {
            stateMachine.ChangeState(FirstState.Instance);
            seconds = stateMachine.currentState.seconds;
        }
        stateMachine.Update();
    }
}
