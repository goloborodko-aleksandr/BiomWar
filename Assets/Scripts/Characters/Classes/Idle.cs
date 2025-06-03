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
        public Fsm fsm { get; }

        public Idle(Fsm fsm,  Player player)
        {
            this.fsm = fsm; 
            this.player = player;
        }
        public void EnterState()
        {
            Debug.Log($"Entering state: {this}");
            player.ShowWay.Show(player.EligibleFloors);
        }

        public void ExitState()
        {
            
        }

        public void Update()
        {
            player.ProgressTime += Time.deltaTime * player.CharacterSpeed;
        }
    }
}