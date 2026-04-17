using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class ObstacleData
{
    public enum ObstacleType { Static, Moving, Rotating }

    public ObstacleType type;
    public Vector2 position;
    public Vector2 size;
    public Vector2 movePointA;
    public Vector2 movePointB;
    public float moveSpeed;
    public float rotationSpeed;
}

[Serializable]
public class LevelConfig
{
    public int levelNumber;
    public int goalsToWin;
    public List<ObstacleData> obstacles;

    public LevelConfig(int number, int goals, List<ObstacleData> obs)
    {
        levelNumber = number;
        goalsToWin = goals;
        obstacles = obs;
    }
}
