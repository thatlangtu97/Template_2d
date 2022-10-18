using System;
using Entitas;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Core.GamePlay;
using Sirenix.OdinInspector;

namespace Core.GamePlay
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        public bool isPlaying = true;
        //Systems CharacterSystems;
        private Systems GameSystem;
        private StateMachineUpdateSystem stateMachineUpdateSystem;
        private TakeDamageSystem takeDamageSystem;
        private ProjectileMoveBezierSystem projectileMoveBezierSystem;
        private DamageTextSystem damageTextSystem;
//    private HealthBarUpdateSystem healthBarUpdateSystem;
        private Contexts contexts;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        public void CreateSystem()
        {
            contexts = Contexts.sharedInstance;
            stateMachineUpdateSystem = new StateMachineUpdateSystem(contexts);
            takeDamageSystem = new TakeDamageSystem(contexts);
            projectileMoveBezierSystem = new ProjectileMoveBezierSystem(contexts);
            damageTextSystem = new DamageTextSystem(contexts);
//        healthBarUpdateSystem = new HealthBarUpdateSystem(contexts);
        }
    
        [Button("SetupSystem", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
        public void SetupSystem()
        {
            GameSystem = new Feature("GameSystem")
                    .Add(stateMachineUpdateSystem)
                    .Add(takeDamageSystem)
                    .Add(projectileMoveBezierSystem)
                    .Add(damageTextSystem)
//                .Add(healthBarUpdateSystem)
                ;
            DealDmgManager.context = contexts;
            DamageTextManager.context = contexts;
            GameSystem.Initialize();
        }
        void Start()
        {
            CreateSystem();
            SetupSystem();
        }
        void Update()
        {
            if(isPlaying)
                if(GameSystem!=null)
                    GameSystem.Execute();
        }
        private void LateUpdate()
        {
            if(isPlaying)
                if(GameSystem!=null)
                    GameSystem.Cleanup();  
        }
    }

}


