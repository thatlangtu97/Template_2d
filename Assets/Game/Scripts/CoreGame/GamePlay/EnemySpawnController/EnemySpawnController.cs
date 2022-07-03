using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.GamePlay;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnController : MonoBehaviour
{
    private static EnemySpawnController instance;
    public static EnemySpawnController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject dataManager = new GameObject();
                dataManager.name = "EnemySpawnController";
                instance = dataManager.AddComponent<EnemySpawnController>();
            }
            return instance;
        }
    }

    public Dictionary<int, List<WaveData>> dictionaryWave = new Dictionary<int, List<WaveData>>();
    public int level;
    public LayerMask layerWall;
    #region RANDOM DATA

    public List<float> randomRatioIime = new List<float>();
    public List<Vector3> randomPositionGround = new List<Vector3>();
    private int randomIndex;
    public void RandomListTimeRatio()
    {
        randomRatioIime = new List<float>();
        int count = 0;
        while (count<100)
        {
            count += 1;
            randomRatioIime.Add(Random.Range(0f,1f));
        }
    }

    public float GetRandomRatioTime()
    {
        randomIndex += 1;
        return randomRatioIime[randomIndex % randomRatioIime.Count];
        
    }

    public void RandomListPosition()
    {
        randomPositionGround = new List<Vector3>();
        RaycastHit2D[] hitsMin = Physics2D.RaycastAll(Vector2.zero, Vector2.left, 1000f, layerWall);
        RaycastHit2D[] hitsMax = Physics2D.RaycastAll(Vector2.zero, Vector2.right, 1000f, layerWall);
        float minX = 0;
        float maxX = 0;
        foreach (var hit in hitsMin)
        {
            if (hit.point.x < minX)
            {
                minX = hit.point.x;
            }
        }
        foreach (var hit in hitsMax)
        {
            if (hit.point.x > maxX)
            {
                minX = hit.point.x;
            }
        }

        int count = 0;
        while (count<100)
        {
            count += 1;
            randomPositionGround.Add(new Vector3(Random.Range(minX,maxX),0f));
        }
        
    }
    public Vector3 GetRandomPosition()
    {
        randomIndex += 1;
        return randomPositionGround[randomIndex % randomRatioIime.Count];
        
    }
    #endregion

    public void Setup()
    {
        //base.CopyStart();
        SetupWaveData();
        RandomListTimeRatio();
        RandomListPosition();
        _disposable= new CompositeDisposable();
        
    }
    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetupWaveData()
    {
        ZoneData zoneData = LevelMapConfigManager.Instance.zoneData;
        
        foreach (var tempData in zoneData.waveList)
        {
            if (dictionaryWave.Keys.Contains(tempData.WaveID))
            {
                dictionaryWave[tempData.WaveID].Add(tempData);
            }
            else
            {
                dictionaryWave.Add(tempData.WaveID,new List<WaveData>(){tempData});
            }
        }
        
    }

    List<string> NameEnemyPrefabList(int level)
    {
        List<string> temp = new List<string>();

        List<WaveData> dataWave = dictionaryWave[level];

        foreach (var wave in dataWave)
        {
//            if (!temp.Contains(wave.EnemyID))
//            {
                temp.Add(wave.EnemyID);
//            }
        }
        return temp;
    }

    GameObject GetPrefabInZoneData(string EnemyID,List< GameObject >prefabs)
    {
        foreach (GameObject prefab in prefabs)
        {
            if (EnemyID == prefab.name)
            {
                return prefab;
            }
        }

        return null;
    }
    int GetCountEnemy(int level)
    {
        int count = 0;
        
        List<WaveData> dataWave = dictionaryWave[level];
        foreach (var wave in dataWave)
        {
            count += wave.Amount;
        }
        Debug.Log("count enemy "+ count);
        return count;
    }
    [Button("SPAWN ENEMY", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void TestSpawn()
    {
        SpawnEnemy(level);
    }
    public void SpawnEnemy(int currentLevel)
    {
        int enemyCountinLevel = GetCountEnemy(currentLevel);
        List<GameObject> prefabs = LevelMapConfigManager.Instance.zoneData.enemyList;
        float time = 0;
        int countRandomRatio = 0;
        foreach (WaveData wave in dictionaryWave[currentLevel])
        {
            GameObject tempEnemyPrefab = GetPrefabInZoneData(wave.EnemyID, prefabs);
            for (int i = 0; i < wave.Amount; i++)
            {
                time += (wave.TimeSpawnMin + wave.TimeSpawnMax) * GetRandomRatioTime();
                countRandomRatio += 1;
                Vector3 positionRandom = GetRandomPosition();
                Action action = delegate
                {
                    Spawn(tempEnemyPrefab,positionRandom);
                };
                ActionDelayTime(action,time);
            }
        }
    }
    public void Spawn(GameObject prefab,Vector3 positionSpawn)
    {
        StateMachineController temp = PoolManager.Spawn<StateMachineController>(prefab,positionSpawn);
        temp.GetComponent<ComponentManager>().SetupEntity();
    }
    CompositeDisposable _disposable;
    public void ActionDelayTime(Action action ,float timedelay)
    {
        Observable.Timer(TimeSpan.FromSeconds(timedelay)).Subscribe(l => { action.Invoke(); }).AddTo(_disposable);
    }

    public void ActionDelayFrame(Action action, int frameDelay)
    {
        Observable.TimerFrame(frameDelay,FrameCountType.Update).Subscribe(l => { action.Invoke(); }).AddTo(_disposable);
    }
}
