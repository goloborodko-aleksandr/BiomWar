using System;
using System.Collections.Generic;
using Characters.Interfaces;
using Characters.Mono;
using GenerateAndCreateMap.Mono;
using UnityEngine;

namespace Characters.Classes
{
    public class Idle: IState
    {
        private Player player;
        private Action callbackProgress;
        public Fsm fsm { get; }

        public Idle(Fsm fsm,  Player player, Action callbackProgress)
        {
            this.callbackProgress = callbackProgress;
            this.fsm = fsm; 
            this.player = player;
        }
        public void EnterState()
        {
            callbackProgress?.Invoke();
        }

        public void ExitState()
        {
            
        }

        public void Update()
        {
            
        }
    }
}