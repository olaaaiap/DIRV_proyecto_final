using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameTimer", menuName = "VR/Game Timer")]
public class GameTimerSO : ScriptableObject
{
    public Action OnTimerUpdated;
    public float elapsedTime = 0f;
    public bool isRunning = false;

    public void ResetTimer()
    {
        Debug.Log("resetTimer");
        elapsedTime = 0f;
        isRunning = false;
    }

    public void UpdateTimer(float deltaTime)
    {
        if (isRunning)
        {
            elapsedTime += deltaTime;
            OnTimerUpdated?.Invoke();
        }
    }
}