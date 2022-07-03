using System;
using Entitas;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using EntrySystem;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;

public class GameController : View
{
    public static GameController instance;

    public bool isPlaying = true;
    //Systems CharacterSystems;
    private Systems GameSystem;
    private StateMachineUpdateSystem stateMachineUpdateSystem;
    private TakeDamageSystem takeDamageSystem;
    private ProjectileMoveBezierSystem projectileMoveBezierSystem;
    private DamageTextSystem damageTextSystem;
    private HealthBarUpdateSystem healthBarUpdateSystem;
    private Contexts contexts;


    public bool testloadFlashScene = false;
    private void Awake()
    {
        //Debug.Log("entry context "+EntryContextView.Instance);
#if UNITY_EDITOR
        EntryContextView.Instance.loadFlashScene = testloadFlashScene;
        GamePlayContextView.Instance.Load();
#endif
        if (instance == null)
        {
            instance = this;
        }
    }
    public void InitUI()
    {
        GameObject UI1 = Resources.Load<GameObject>(GameResourcePath.UI1);
        GameObject UI2 = Resources.Load<GameObject>(GameResourcePath.UI2);
        GameObject UI3 = Resources.Load<GameObject>(GameResourcePath.UI3);
        GameObject UI4 = Resources.Load<GameObject>(GameResourcePath.UI4);
        Instantiate(UI1);
        Instantiate(UI2);
        Instantiate(UI3);
        Instantiate(UI4);
    }
    public void CreateSystem()
    {
        contexts = Contexts.sharedInstance;
        stateMachineUpdateSystem = new StateMachineUpdateSystem(contexts);
        takeDamageSystem = new TakeDamageSystem(contexts);
        projectileMoveBezierSystem = new ProjectileMoveBezierSystem(contexts);
        damageTextSystem = new DamageTextSystem(contexts);
        healthBarUpdateSystem = new HealthBarUpdateSystem(contexts);
    }
    
    [Button("SetupSystem", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void SetupSystem()
    {
        GameSystem = new Feature("GameSystem")
                .Add(stateMachineUpdateSystem)
                .Add(takeDamageSystem)
                .Add(projectileMoveBezierSystem)
                .Add(damageTextSystem)
                .Add(healthBarUpdateSystem)
            ;
        DealDmgManager.context = contexts;
        DamageTextManager.context = contexts;
        GameSystem.Initialize();
    }
    void Start()
    {
        if (PlayFlashScene.instance != null)
        {
            PlayFlashScene.instance.HideLoading();
        }
        CreateSystem();
        SetupSystem();
        InitUI();
    }
    void Update()
    {
        if(isPlaying)
            if(GameSystem!=null)
                GameSystem.Execute();
        //GameSystem.Cleanup();
    }

//    private void FixedUpdate()
//    {
//        if(GameSystem!=null)
//            GameSystem.Execute();  
//    }

    private void LateUpdate()
    {
        if(isPlaying)
            if(GameSystem!=null)
                GameSystem.Cleanup();  
    }



}

