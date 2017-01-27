using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TrafficLightFSM
{
    public enum LightState
    {
        Init = 0,
        Red = 1,
        Green = 2,
        Yellow = 3,
        Exit = 9000,
    }
    class State
    {
        public State() { }
        public State(Enum e)
        {
            name = e.ToString();
        }
        public string name;
        public delegate void OnEnter();
        public delegate void OnExit();
        public OnEnter onEnter;
        public OnExit onExit;

        public void AddEnterFunction(Delegate d)
        {
            onEnter += d as OnEnter;

        }
    }
    class FSM<T>
    {
        // Init -> Red : Automatic
        // Red -> Green : timer == 135 seconds
        // Green -> Yellow == 120 seconds
        // Yellow -> Red == 15 seconds
        // Any -> Exit == turned off
        public FSM()
        {
            States = new Dictionary<string, State>();
            var v = Enum.GetValues(typeof(T));
            foreach (var e in v)
            {
                State s = new State(e as Enum);
                States.Add(s.name, s);
            }
        }

        Dictionary<string, State> States;
        State cState;
        State nextState;
        public void ChangeState(State state)
        {
            if (isValidTransition(state))
            {
                cState.onExit();
                cState = state;
                cState.onEnter();
            }
        }

        public bool isValidTransition(State s)
        {
            return States.ContainsKey(s.name);
        }
        public void AddTransition(State a, State b)
        {
            cState = a;
            ChangeState(b);
        }
        public State GetState(T e)
        {
            string key = (e as State).name;
            return States[key];
        }
        private Dictionary<string, List<State>> transitions = new Dictionary<string, List<State>>();
        private bool isValidTransition(State to)
        {
            var validStates = transitions[cState.name];
            if (validStates == null)
            {
                return false;
            }
            foreach (var state in validStates)
            { 
                if (state == to)
                {
                    return true;
                }
                return false;
            }
        }
        public bool Start()
        {
            return true;
        }
        public bool Update()
        {
            return true;
        }
    }
}
