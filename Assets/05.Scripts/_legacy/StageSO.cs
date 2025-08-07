using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyInfo
{
    public int fly;
    public int bee;
    public int butterfly;
}

[Serializable]
public struct StageInfo
{
    public float difficulty;
    public EnemyInfo enemyinfo;
}

[CreateAssetMenu(fileName = "StageSO", menuName = "ScriptableObjects/StageSO", order = 1)]
public class StageSO : ScriptableObject
{
    public List<StageInfo> stageInfoList;
}