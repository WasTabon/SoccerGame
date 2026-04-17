using System.Collections.Generic;
using UnityEngine;

public static class LevelDatabase
{
    private static List<LevelConfig> allLevels;
    private static int[] levelMapping;

    public static int TotalLevels => 30;

    public static LevelConfig GetLevel(int levelNumber)
    {
        if (allLevels == null) GenerateLevels();
        int index = Mathf.Clamp(levelNumber - 1, 0, allLevels.Count - 1);
        return allLevels[index];
    }

    public static int GetBaseConfigIndex(int levelNumber)
    {
        if (levelMapping == null) GenerateLevels();
        return levelMapping[Mathf.Clamp(levelNumber - 1, 0, 29)];
    }

    private static void GenerateLevels()
    {
        allLevels = new List<LevelConfig>();
        levelMapping = new int[30];

        List<LevelConfig> baseConfigs = new List<LevelConfig>
        {
            CreateLevel1(),
            CreateLevel2(),
            CreateLevel3(),
            CreateLevel4(),
            CreateLevel5()
        };

        for (int i = 0; i < 5; i++)
        {
            allLevels.Add(baseConfigs[i]);
            levelMapping[i] = i;
        }

        Random.State oldState = Random.state;
        Random.InitState(42);
        for (int i = 5; i < 30; i++)
        {
            int baseIndex = Random.Range(0, 5);
            levelMapping[i] = baseIndex;
            LevelConfig baseConfig = baseConfigs[baseIndex];
            LevelConfig copy = new LevelConfig(i + 1, baseConfig.goalsToWin + (i / 10), baseConfig.obstacles);
            allLevels.Add(copy);
        }
        Random.state = oldState;
    }

    private static LevelConfig CreateLevel1()
    {
        var obs = new List<ObstacleData>();

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(0f, 3f),
            size = new Vector2(1.5f, 0.5f)
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(-1.5f, -1f),
            size = new Vector2(0.6f, 0.6f)
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(1.5f, -1f),
            size = new Vector2(0.6f, 0.6f)
        });

        return new LevelConfig(1, 2, obs);
    }

    private static LevelConfig CreateLevel2()
    {
        var obs = new List<ObstacleData>();

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Moving,
            position = new Vector2(-1.5f, 1f),
            size = new Vector2(1f, 0.4f),
            movePointA = new Vector2(-1.5f, 1f),
            movePointB = new Vector2(1.5f, 1f),
            moveSpeed = 1.5f
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(0f, 5f),
            size = new Vector2(2f, 0.4f)
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(-2f, -3f),
            size = new Vector2(0.5f, 1.5f)
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(2f, -3f),
            size = new Vector2(0.5f, 1.5f)
        });

        return new LevelConfig(2, 2, obs);
    }

    private static LevelConfig CreateLevel3()
    {
        var obs = new List<ObstacleData>();

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Rotating,
            position = new Vector2(0f, 2f),
            size = new Vector2(2.5f, 0.3f),
            rotationSpeed = 50f
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Moving,
            position = new Vector2(-1.5f, 6f),
            size = new Vector2(0.8f, 0.4f),
            movePointA = new Vector2(-1.5f, 6f),
            movePointB = new Vector2(1.5f, 6f),
            moveSpeed = 2f
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(-1f, -2f),
            size = new Vector2(0.7f, 0.7f)
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(1f, -2f),
            size = new Vector2(0.7f, 0.7f)
        });

        return new LevelConfig(3, 3, obs);
    }

    private static LevelConfig CreateLevel4()
    {
        var obs = new List<ObstacleData>();

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Moving,
            position = new Vector2(-2f, -1f),
            size = new Vector2(1.2f, 0.4f),
            movePointA = new Vector2(-2f, -1f),
            movePointB = new Vector2(2f, -1f),
            moveSpeed = 2.5f
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Moving,
            position = new Vector2(2f, 4f),
            size = new Vector2(1f, 0.4f),
            movePointA = new Vector2(2f, 4f),
            movePointB = new Vector2(-2f, 4f),
            moveSpeed = 1.8f
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Rotating,
            position = new Vector2(0f, 7f),
            size = new Vector2(1.8f, 0.3f),
            rotationSpeed = -70f
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(0f, 0f),
            size = new Vector2(0.8f, 0.8f)
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(-2f, 7f),
            size = new Vector2(0.4f, 1.2f)
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(2f, 7f),
            size = new Vector2(0.4f, 1.2f)
        });

        return new LevelConfig(4, 3, obs);
    }

    private static LevelConfig CreateLevel5()
    {
        var obs = new List<ObstacleData>();

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Rotating,
            position = new Vector2(-1.2f, 1f),
            size = new Vector2(1.8f, 0.25f),
            rotationSpeed = 60f
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Rotating,
            position = new Vector2(1.2f, 5f),
            size = new Vector2(1.8f, 0.25f),
            rotationSpeed = -60f
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Moving,
            position = new Vector2(-1.5f, -2f),
            size = new Vector2(1f, 0.4f),
            movePointA = new Vector2(-1.5f, -2f),
            movePointB = new Vector2(1.5f, -2f),
            moveSpeed = 3f
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Moving,
            position = new Vector2(0f, 3f),
            size = new Vector2(0.6f, 0.6f),
            movePointA = new Vector2(-2f, 3f),
            movePointB = new Vector2(2f, 3f),
            moveSpeed = 2f
        });

        obs.Add(new ObstacleData
        {
            type = ObstacleData.ObstacleType.Static,
            position = new Vector2(0f, 8f),
            size = new Vector2(1.5f, 0.3f)
        });

        return new LevelConfig(5, 4, obs);
    }
}
