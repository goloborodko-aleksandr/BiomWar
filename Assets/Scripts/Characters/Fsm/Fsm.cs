using System;
using System.Collections.Generic;
using R3;

namespace Characters.Fsm
{
    public class Fsm
    {
        private IState _currentState;
        private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private readonly Subject<Type> _onStateChanged = new Subject<Type>();
        public Observable<Type> OnStateChanged => _onStateChanged;
        public IState CurrentState => _currentState;

        public void AddState(IState state)
        {
            _states.Add(state.GetType(), state);
        }
        
        public void OverrideState(IState oldState, IState newState)
        {
            if (_states.ContainsKey(oldState.GetType()))
            {
                _states[oldState.GetType()] = newState;
            }
            else
            {
                AddState(newState);
            }
        }

        public void ChangeState<T>() where T : IState
        {
            var type = typeof(T);
            if(_currentState?.GetType() == type)return;
            if (_states.TryGetValue(type, out var newCurrentState))
            {
                _currentState?.ExitState();
                _currentState = newCurrentState;
                _onStateChanged.OnNext(type);
                _currentState?.EnterState();
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}