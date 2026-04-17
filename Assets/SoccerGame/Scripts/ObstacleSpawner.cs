using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance { get; private set; }

    public PhysicsMaterial2D bounceMaterial;
    public Sprite squareSprite;

    private Transform obstaclesParent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ClearObstacles()
    {
        obstaclesParent = GameObject.Find("Obstacles")?.transform;
        if (obstaclesParent != null)
        {
            for (int i = obstaclesParent.childCount - 1; i >= 0; i--)
                Destroy(obstaclesParent.GetChild(i).gameObject);
        }
        else
        {
            GameObject obj = new GameObject("Obstacles");
            obstaclesParent = obj.transform;
        }
    }

    public void SpawnForMatchEndless()
    {
        ClearObstacles();

        SpawnStatic("StaticBlock_Left", new Vector2(-1.8f, 2f), new Vector2(0.8f, 0.8f));
        SpawnStatic("StaticBlock_Right", new Vector2(1.8f, 2f), new Vector2(0.8f, 0.8f));
        SpawnStatic("StaticBlock_Center", new Vector2(0f, 5f), new Vector2(1f, 0.5f));

        SpawnMoving("MovingBlock_1", new Vector2(-1.5f, -2f), new Vector2(1.5f, -2f), 1.5f, new Vector2(1.2f, 0.4f));
        SpawnMoving("MovingBlock_2", new Vector2(-1.8f, 6.5f), new Vector2(1.8f, 6.5f), 2f, new Vector2(0.8f, 0.4f));

        SpawnRotating("RotatingBlock", new Vector2(0f, 0f), new Vector2(2f, 0.3f), 60f);
    }

    public void SpawnForLevel(LevelConfig config)
    {
        ClearObstacles();

        for (int i = 0; i < config.obstacles.Count; i++)
        {
            ObstacleData data = config.obstacles[i];
            string name = "LvlObs_" + i;

            switch (data.type)
            {
                case ObstacleData.ObstacleType.Static:
                    SpawnStatic(name, data.position, data.size);
                    break;
                case ObstacleData.ObstacleType.Moving:
                    SpawnMoving(name, data.movePointA, data.movePointB, data.moveSpeed, data.size);
                    break;
                case ObstacleData.ObstacleType.Rotating:
                    SpawnRotating(name, data.position, data.size, data.rotationSpeed);
                    break;
            }
        }
    }

    private GameObject CreateBase(string name, Vector2 pos, Vector2 size, Color color)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(obstaclesParent);
        obj.transform.position = new Vector3(pos.x, pos.y, 0);
        obj.transform.localScale = new Vector3(size.x, size.y, 1f);

        SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = squareSprite;
        sr.color = color;
        sr.sortingOrder = 2;

        BoxCollider2D col = obj.AddComponent<BoxCollider2D>();
        col.size = Vector2.one;
        if (bounceMaterial != null) col.sharedMaterial = bounceMaterial;

        return obj;
    }

    private void SpawnStatic(string name, Vector2 pos, Vector2 size)
    {
        CreateBase(name, pos, size, new Color(0.5f, 0.4f, 0.6f));
    }

    private void SpawnMoving(string name, Vector2 pointA, Vector2 pointB, float speed, Vector2 size)
    {
        GameObject obj = CreateBase(name, pointA, size, new Color(0.9f, 0.6f, 0.2f));

        Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        MovingObstacle mo = obj.AddComponent<MovingObstacle>();
        mo.pointA = pointA;
        mo.pointB = pointB;
        mo.speed = speed;
    }

    private void SpawnRotating(string name, Vector2 pos, Vector2 size, float rotSpeed)
    {
        GameObject obj = CreateBase(name, pos, size, new Color(0.2f, 0.7f, 0.8f));

        Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        RotatingObstacle ro = obj.AddComponent<RotatingObstacle>();
        ro.rotationSpeed = rotSpeed;
    }
}
