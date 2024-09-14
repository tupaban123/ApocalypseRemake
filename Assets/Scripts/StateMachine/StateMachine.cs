using System;
using System.Collections.Generic;
using UnityEngine;

namespace Apocalypse.StatesMachine
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        private Dictionary<Type, State> _states = new Dictionary<Type, State>();

        public void Update()
        {
            CurrentState?.Update();
        }

        public void AddState(State state)
        {
            if (!_states.ContainsKey(state.GetType()))
            {
                _states.Add(state.GetType(), state);
                Debug.Log($"state {state.GetType()} added");
            }
            else
                Debug.LogWarning($"State {state.GetType()} already added!");
        }

        public void RemoveState<T>() where T : State
        {
            Type type = typeof(T);

            if (_states.ContainsKey(type))
                _states.Remove(type);
            else
                Debug.LogWarning($"State {type} doesn't exist!");

        }

        public void SetState<T>() where T: State
        {
            Type stateType = typeof(T);

            if (CurrentState?.GetType() == stateType)
                return;

            if(_states.TryGetValue(stateType, out State newState))
            {
                CurrentState?.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }
    }
}