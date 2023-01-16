using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemiesManager enemiesManager;
    YouWinManager playerWin;

    StageTime stageTime;
    int eventIndexer;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    private void Start()
    {
        playerWin = FindObjectOfType<YouWinManager>();
    }
    private void Update()
    {
        if (eventIndexer >= stageData.stageEvents.Count) { return; }

        if(stageTime.time > stageData.stageEvents[eventIndexer].time)
        {
            switch (stageData.stageEvents[eventIndexer].eventType)
            {
                case StageEventType.SpawnEnemy:
                    for (int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
                    {
                        SpawnEnemy();
                    }
                    break;
                case StageEventType.SpawnObject:
                    for (int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
                    {
                        SpawnObject();
                    }
                    break;
                case StageEventType.WinStage:
                    WinStage();
                    break;
            }
            Debug.Log(stageData.stageEvents[eventIndexer].message);
            
             

            eventIndexer += 1;

        }
    }

    private void WinStage()
    {
        playerWin.Win();
    }

    private void SpawnEnemy()
    {
        enemiesManager.SpawnEnemy(stageData.stageEvents[eventIndexer].enemyToSpawn);
    }

    private void SpawnObject()
    {
        Vector3 positionToSpawn = GameManager.instance.playerTransform.position;
        positionToSpawn += UtilityTools.GenerateRandomPositionSquarePattern(new Vector2(5f, 5f));
        SpawnManager.instance.SpawnObject(
                                    positionToSpawn,
                                    stageData.stageEvents[eventIndexer].objectToSpawn
                                    );
    }
}
