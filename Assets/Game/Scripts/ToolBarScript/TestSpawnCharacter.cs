using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestSpawnCharacter : View
{
    [Inject] public ShowRevivePopupSignal showRevivePopupSignal { get; set; }
    [Inject] public ShowRewardGamePlayPopupSignal showRewardGamePlayPopupSignal { get; set; }
    public bool SetPlayer;
    public float TimeDelay;
    protected override void Awake()
    {
        base.Awake();
        
    }

    protected override void Start()
    {
        base.Start();
    }
    
    

    public void Spawn(GameObject prefab)
    {
        StateMachineController temp = null;
        int index = 0;
        while (index<count)
        {
//            //temp = ObjectPool.Spawn(prefab).GetComponent<StateMachineController>();
//            temp = PoolManager.Spawn(prefab.GetComponent<PoolItem>()).GetComponent<StateMachineController>();
//            //temp.GetComponent<ComponentManager>().SetupEntity();
//            
//            
//            //ActionBufferManager.Instance.ActionDelayTime(() => { temp.GetComponent<ComponentManager>().SetupEntity();}, 2f);
//            
//            //temp.gameObject.SetActive(true);
//            Action tempAction = delegate {  temp.GetComponent<ComponentManager>().SetupEntity(); };
//            setActionDelay(tempAction, TimeDelay);
            
            
            temp = PoolManager.Spawn(prefab).GetComponent<StateMachineController>();
            temp.GetComponent<ComponentManager>().SetupEntity();
            
//            Action tempAction = delegate {  temp.GetComponent<ComponentManager>().SetupEntity(); };
//            setActionDelay(tempAction, TimeDelay);
            
            index += 1;
        } 
        
        
        if (SetPlayer)
        {
            Action tempAction = delegate {  
                GameUIController.instance.stateMachine = temp;
                GameUIController.instance.MODIFY(); 
            };
            
            setActionDelay(tempAction, TimeDelay);
//            ActionBufferManager.Instance.ActionDelayTime(() =>
//            {
//                GameUIController.instance.stateMachine = temp;
//                GameUIController.instance.MODIFY();
//                
//            }, 2.1f);
            
        }
    }

    public void ReviveState()
    {
//        if (Contexts.sharedInstance.game.playerFlagEntity != null)
//            Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.value.OnInputRevive();
        showRevivePopupSignal.Dispatch();
    }

    public void EndGame()
    {
        showRewardGamePlayPopupSignal.Dispatch();   
    }
    public void SpawnMap(GameObject prefab)
    {
       // ObjectPool.Spawn(prefab);
        
    }

    public int count;
    public void TestSpawnTextMesh(Transform transform)
    {
        int index = 0;
        while (index<count)
        {
            DamageTextManager.AddReactiveComponent(DamageTextType.Normal,10.ToString(),transform.position + new Vector3(Random.Range(-.5f,.5f),Random.Range(1.5f,2f),0f));
            index += 1;
        } 
        
    }
    
    public void SpawnEnemy(GameObject prefab)
    {
        int countSpawn = 1;
        float timeDelay = 1f;
        while (countSpawn < count+1 )
        {
//            Vector3 randomPos = new Vector3(Random.Range(-13f,5f),0f,0f);
//            Action action = delegate 
//            {
//                StateMachineController temp = ObjectPool.Spawn(prefab,null,randomPos).GetComponent<StateMachineController>();
//                temp.gameObject.SetActive(true);  
//            };
//            ActionBufferManager.Instance.ActionDelayTime(action,timeDelay*countSpawn);
//            
            Vector3 randomPos = new Vector3(Random.Range(-13f,5f),0f,0f);
            StateMachineController temp = PoolManager.Spawn(prefab,null,randomPos).GetComponent<StateMachineController>();
            Action tempAction = delegate
            {
                temp.GetComponent<ComponentManager>().SetupEntity();
            };
            setActionDelay(tempAction, timeDelay*countSpawn);
            //timeDelay *= countSpawn;
            countSpawn += 1;
        }
    }

    private int fps = -1;
    public void SetFps()
    {
        if (fps == -1)
        {
            Application.targetFrameRate = 70;
            fps = 70;
        }
        else
        {
            Application.targetFrameRate = -1;
            fps = -1;
        }
    }

    private void Update()
    {
        if (actions.Count > 0)
        {
            foreach (var action in actions)
            {
                if (action.CanInvoke)
                {
                    action.Invoke();
                }
                else
                {
                    action.DownTime(Time.deltaTime);
                }
            }

            foreach (var action in actions)
            {
                if (action.CanRemove)
                {
                    actions.Remove(action);
                    break;
                }
            }
        }
    }

    public List<DataAction> actions= new List<DataAction>();
    public void setActionDelay(Action actionNew, float time)
    {
        if (actions == null)
        {
            actions= new List<DataAction>();
        }
        actions.Add(new DataAction(){action = actionNew,timeDelay = time,isInvoke = false});
    }
    [System.Serializable]
    public class DataAction
    {
        public Action action;
        public float timeDelay;
        public bool isInvoke;

        public bool CanInvoke
        {
            get { return isInvoke == false && timeDelay < 0; }
        }
        public bool CanRemove
        {
            get { return isInvoke == true && timeDelay < 0; }
        }
        public void DownTime(float Time)
        {
            timeDelay -= Time;
        }

        public void Invoke()
        {
            action.Invoke();
            isInvoke = true;
        }
    }
}
