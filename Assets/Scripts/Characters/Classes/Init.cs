using System.Collections.Generic;
using Characters.Interfaces;
using Characters.Mono;
using GenerateAndCreateMap.Mono;
using UnityEngine;

namespace Characters.Classes
{
    public class Init: IState
    {
        private Player player;
        float cooldown;
        public Fsm fsm { get; }

        public Init(Fsm fsm, Player player, float cooldown)
        {
            this.fsm = fsm;
            this.player = player;
            this.cooldown = cooldown;
        }
        public void EnterState()
        {
            Debug.Log($"Entering state: {this}");
            player.TargetFloor.ComeCharacter(player);
            player.transform.position = player.CurrentFloor.transform.position + Vector3.up;
            player.ProgressTime = cooldown;
            player.ShowWay.Show(player.EligibleFloors);
            fsm.ChangeState<Idle>();
        }

        public void ExitState()
        {
            
        }

        public void Update()
        {
            
        }
    }
}