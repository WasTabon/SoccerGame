using UnityEngine;
using System;

public class GoalZone : MonoBehaviour
{
    public bool isPlayerGoal;

    public static event Action<bool> OnGoalScored;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball == null) return;

        OnGoalScored?.Invoke(isPlayerGoal);
    }
}
