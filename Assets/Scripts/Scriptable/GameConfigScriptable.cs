
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfigScriptable : ScriptableObject
{
    [Header("Match Config Data")]
    public int toWin;
    public int totalOvers;
    public int totalBallsPerOver;
    public int totalBatsmen;


    [Header("Balling Config Data")]
    public List<BallType> ballTypes;
    public Vector2 dividePitchInto;


    [Header("Batting Config Data")]
    public List<RunsMetric> runsData;

    [Tooltip("Weightage of OUT(X) and MISS(Y)")]
    public Vector2 outMissWeightage;

    public string GetBallType(int id) => ballTypes.Find(x => x.id == id).ballType;
    public int TotalBalls => totalOvers * totalBallsPerOver;


    public float GetWeightage(int score) => runsData.Find(x => x.runs == score).weightage;
    public Vector2 GetOutAndMissWeightage() => outMissWeightage;
}

[System.Serializable]
public class BallType
{
    public int id;
    public string ballType;
}

[System.Serializable]
public class RunsMetric
{
    public int runs;
    public float weightage;
}
