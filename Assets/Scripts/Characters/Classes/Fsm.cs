using System;
using System.Collections.Generic;

namespace Characters.Classes
{
    public class Fsm
    {
        private IState currentState{get; set;}
        private Dictionary<Type, IState> states;

        public void AddState(IState state)
        {
            states.Add(state.GetType(), state);
        }

        public Fsm(Dictionary<Type, IState> states)
        {
            this.states = states;
        }
        
        public Fsm():this(new Dictionary<Type, IState>()){}

        public void ChangeState<T>() where T : IState
        {
            var type = typeof(T);
            if(currentState.GetType() == type) return;
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