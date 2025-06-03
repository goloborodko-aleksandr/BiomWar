using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Characters.Classes
{
    public class Fsm
    {
        private IState currentState;
        private Dictionary<Type, IState> states = new Dictionary<Type, IState>();
        private readonly Subject<Type> onStateChanged = new Subject<Type>();
        public Observable<Type> OnStateChanged => onStateChanged;
        public IState CurrentState => currentState;

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
                onStateChanged.OnNext(type);
                currentState?.EnterState();
            }
        }

        public void Update()
        {
            currentState?.Update();
        }
    }
}