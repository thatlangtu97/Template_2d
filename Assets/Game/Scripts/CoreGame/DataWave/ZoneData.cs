using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DataZone", menuName = "CoreGame/ZoneData")]
public class ZoneData : SerializedScriptableObject
{
//    public bool tutorialMap;
    public List<WaveData> waveList;
    public List<LevelData> levelList;
//    public List<GameObject> mapList;
//    public List<GameObject> mapListTutorial;
//
//    public float bonusLevelAppearPercent;
//    public float incrementalBonusStage;
//    public int maxBonusStage;
//    public float healDropRate;
//    public float incrementalHealDropRate;
//
//    //public BonusData bonus;
//    public List<int> openWallLevels;
//    //public List<int> passiveGetLevels;
//    public List<int> changeBackGroundLevels;
//    public List<int> introLevels;
//    //public List<ArtifactConfig> artifactConfigs;
//    public List<Intro> introBoss;
//    public List<Sprite> bossIcon;
//    public int numberOfStageToInit;
//    public List<int> initEnemyPerStage;
//
//    public int numberOfStageToInitBoss;
//    public List<int> initEnemyWithBossStage;
//
//    //public List<Tape> dialogueSequenceList;
    public List<GameObject> enemyList;
    public List<PoolSizeData> poolSizeData;
    
    [Button("FindEnemyPrefab", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void FindEnemyPrefab()
    {
        // FindPrefab
        enemyList = new List<GameObject>();
        foreach (var tempWave in waveList)
        {
            GameObject prefab = Resources.Load<GameObject>("PrefabCharacter/" + tempWave.EnemyID);
            if (!enemyList.Contains(prefab))
            {
                enemyList.Add(prefab);
            }
        }
        // Find Size
        poolSizeData = new List<PoolSizeData>();
        foreach (var tempEnemy in enemyList)
        {
            PoolSizeData currentPool= new PoolSizeData();
            string idEnemy = tempEnemy.name;
            int size = 0;
            
            foreach (var tempWave in waveList)
            {
                if (tempWave.EnemyID == idEnemy && tempWave.Amount > size)
                {
                    size = tempWave.Amount;
                }
            }
            
            currentPool.name = idEnemy;
            currentPool.size = size;
            poolSizeData.Add(currentPool);
        }
    }

    public PoolManager poolManager;
    [Button("SetupPool", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void SetupPool()
    {
//        List<PoolManager.PoolList> poolSizeZone = poolManager.poolSizeZone;
//        PoolManager.PoolList[] poolLists = poolManager.poolLists;
//
//        foreach (var poolSizeItem in poolSizeData)
//        {
//            foreach (var poolItem in poolLists)
//            {
//                if (poolItem.name == poolSizeItem.name)
//                {
//                    PoolManager.PoolList temp = new PoolManager.PoolList(poolItem);
//                    
//                    //temp.name = poolSizeItem.name;
//                    //temp.ListPrefab = poolItem.ListPrefab;
//                    foreach (var tempPrefab in temp.ListPrefab)
//                    {
//                        string name = tempPrefab.prefab.name;
//                        tempPrefab.prefab = Resources.Load<GameObject>("PrefabCharacter" + tempPrefab.prefab.name).GetComponent<PoolItem>();
//                        tempPrefab.size = poolSizeItem.size * tempPrefab.size;
//                    }
//                    poolSizeZone.Add(temp);
//                    
//                }
//            }
//        }

    }
}
[Serializable]
public class PoolSizeData
{
    public string name;
    public int size;
}

[Serializable]
public class WaveData
{
    public int WaveID;
    public string EnemyID;
    public int Amount;
    public float Time;
    public float TimeSpawnMin;
    public float TimeSpawnMax;
}

[Serializable]
public class LevelData
{
    public string LevelID;
    public float atkScale;
    public float hpScale;
    public float goldScale;
    public float hpReduceDropChance;
    public float EXP;
    public float crystalScale;
    public string[] WaveIDs;
}


[System.Serializable]
public struct Intro
{
    public float delay;
    public GameObject prefab;
    public float duration;
}

