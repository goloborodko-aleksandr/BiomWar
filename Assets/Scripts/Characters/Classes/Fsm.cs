using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Classes
{
    public class Fsm
    {
        private IState currentState{get; set;}
        private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

        public void AddState(IState state)
        {
            states.Add(state.GetType(), state);
        }
        
        public void OverrideState(IState oldState, IState newState)
        {
            if (states.ContainsKey(oldState.GetType()))
            {
                states[oldState.GetType()] = newState;
            }
            else
            {
                AddState(newState);
            }
        }

        public void ChangeState<T>() where T : IState
        {
            var type = typeof(T);
            if(currentState?.GetType() == type)return;
            if (states.TryGetValue(type, out var newCurrentState))
            {
                currentState?.ExitState();
                currentState = newCurrentState;
                currentState?.EnterState();
            }
        }

        public void Update()
        {
            currentState?.Update();
        }
    }
}