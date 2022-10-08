using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    public class StateMachineController : MonoBehaviour
    {
        public Dictionary<NameState, State> dictionaryStateMachine = new Dictionary<NameState, State>();
        [FoldoutGroup("Current State")] public State currentState;
        [FoldoutGroup("Current State")] public NameState currentNameState;
        [FoldoutGroup("Previous State")] public NameState previousNameState;
        [FoldoutGroup("State To Clone")] public List<StateClone> States;
        [FoldoutGroup("Referen")] public ComponentManager componentManager;
        private void Awake()
        {
            SpawnState();
            InitState();
        }

        public void Recycle()
        {
            currentState = null;
            currentNameState = NameState.UnknowState;
            previousNameState = NameState.UnknowState;

        }
        private void SpawnState()
        {
            dictionaryStateMachine = new Dictionary<NameState, State>();
            foreach (StateClone tempState in States) {
                State state = Instantiate(tempState.StateToClone);
                if (!dictionaryStateMachine.ContainsKey(tempState.NameState))
                {
                    dictionaryStateMachine.Add(tempState.NameState, state);
                }
                else
                {
                    dictionaryStateMachine[tempState.NameState] = state;
                }
            }
        }

        private void InitState()
        {
            foreach (State state in dictionaryStateMachine.Values)
            {
                state.InitState(this);
            }
        }
        private void SetupState()
        {
            currentState = null;
            currentNameState = NameState.UnknowState;
            InitState();
        }
        public virtual void InitStateMachine()
        {
            SetupState();
            if (dictionaryStateMachine.ContainsKey(NameState.SpawnState))
            {
                ChangeState(NameState.SpawnState, 0, true);
            }
            else
            {
                ChangeState(NameState.IdleState, 0, true);
            }
        }
        public virtual void UpdateState()
        {
            if (currentState != null)
            {
                currentState.UpdateState();
            }
        }
        public virtual void OnSpawn()
        {
        }
        public virtual void OnRevival()
        {
        }
        protected void CreateStateFactory(StateClone stateClone)
        {
            State state = Instantiate(stateClone.StateToClone);
            state.InitState(this);
            if (!dictionaryStateMachine.ContainsKey(stateClone.NameState))
            {
                dictionaryStateMachine.Add(stateClone.NameState, state);
            }
            else
            {
                dictionaryStateMachine[stateClone.NameState] = state;
            }
        }
        public virtual void ChangeState(NameState nameState, bool forceChange = false)
        {
            if (!dictionaryStateMachine.ContainsKey(nameState)) return;
            State newState = dictionaryStateMachine[nameState];
            if (!forceChange)
            {
                if (currentNameState != NameState.DieState && currentNameState != NameState.ReviveState)
                {
                    
                    if (nameState != currentNameState)
                    {
                        if (currentState)
                        {
                            currentState.ExitState();
                        }

                        previousNameState = currentNameState;
                        currentState = newState;
                        currentNameState = nameState;
                        currentState.EnterState();
                    }
                }
            }
            else
            {
                if (currentState)
                {
                    currentState.ExitState();
                }
                previousNameState = currentNameState;
                currentState = newState;
                currentNameState = nameState;
                currentState.EnterState();
            }
        }
        public virtual void ChangeState(NameState nameState, int idState, bool forceChange = false)
        {
            if (!dictionaryStateMachine.ContainsKey(nameState)) return;
            State newState = dictionaryStateMachine[nameState];
            newState.idState = idState;
            if (!forceChange)
            {
                if (currentNameState != NameState.DieState && currentNameState != NameState.ReviveState)
                {
                    if (nameState != currentNameState)
                    {
                        if (currentState)
                        {
                            currentState.ExitState();
                        }
                        previousNameState = currentNameState;
                        currentState = newState;
                        currentNameState = nameState;
                        currentState.EnterState();
                    }
                }
            }
            else
            {
                if (currentState)
                {
                    currentState.ExitState();
                }
                previousNameState = currentNameState;
                currentState = newState;
                currentNameState = nameState;
                currentState.EnterState();
            }
        }
        public void SetIdState(NameState nameState, int idState)
        {
            if (!dictionaryStateMachine.ContainsKey(nameState)) return;
            State newState = dictionaryStateMachine[nameState];
            newState.idState = idState;
        }
        public virtual void OnInputState(NameState nameState,int idState)
        {
        }
        public virtual void OnInputAttack()
        {
            if (currentState)
            {
                currentState.OnInputAttack();
            }
        }
        public virtual void OnInputAttack(int idState)
        {

        }
        public virtual void OnInputJump()
        {
            if (currentState)
            {
                currentState.OnInputJump();
            }
        }
        public virtual void OnInputMove()
        {
            if (currentState)
            {
                currentState.OnInputMove();
            }
        }
        public virtual void OnInputDash()
        {
            if (currentState)
            {
                currentState.OnInputDash();
            }
        }
        public virtual void OnInputRevive()
        {
        }
        public virtual void OnInputSkill(int idSkill)
        {
            if (currentState)
            {
                currentState.OnInputSkill(idSkill);
            }
        }
        
        
        public virtual void OnInputCounter()
        {
            if (currentState)
            {
                currentState.OnInputCounter();
            }
        }
        public virtual void OnHit(Action action)
        {
            if(componentManager.HasImmune(Immune.HIT)) 
                return;
            if (action != null)
            {
                action.Invoke();
            }
            ChangeState(NameState.HitState,true);
        }
        public virtual void OnKnockDown(Action action)
        {
            
            if(componentManager.HasImmune(Immune.KNOCK))
            {
                return;
            }
            ChangeState(NameState.KnockDownState, true);
            if (action != null)
            {
                action.Invoke();
            }
        }
        public virtual void InvokeAction(Action action)
        {
            if (action != null)
            {
                action.Invoke();
            }
        }
        [Button("TEST CHANGE STATE", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
        void TESTCHANGESTATE(NameState nameState)
        { 
            ChangeState(nameState);
        }
        [Button("ATTACK STATE", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
        void TESTATTACK()
        { 
            if(currentState)
            currentState.OnInputAttack();
        }
    }
    
    [System.Serializable]
    public struct StateClone
    {
        [HideLabel]
        public NameState NameState;
        [HideLabel]
        public State StateToClone;
    }
    public enum NameState
    {
        UnknowState,
        SpawnState,
        IdleState,
        MoveState,
        JumpState,
        DashState,
        DieState,
        ReviveState,
        AttackState,
        AirAttackState,
        DashAttackState,
        SkillState,
        KnockDownState,
        HitState,
        GetUpState,
        FreezeState,
        StuntState,
        AirSkillState,
        FallingState,
        KnockUpState,
        RollOutState,
        LandingState,
        CounterState,
    }
}


