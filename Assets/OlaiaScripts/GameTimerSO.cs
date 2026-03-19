using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameTimer", menuName = "VR/Game Timer")]
public class GameTimerSO : ScriptableObject
{
    public Action OnTimerUpdated;
    public float elapsedTime = 0f; // Tiempo en segundos
    public bool isRunning = false; // ¿Está contando?

    // Reinicia el timer
    public void ResetTimer()
    {
        Debug.Log("resetTimer");
        elapsedTime = 0f;
        isRunning = false;
    }

    // Actualiza el tiempo (llámalo desde un MonoBehaviour)
    public void UpdateTimer(float deltaTime)
    {
        if (isRunning)
        {
            elapsedTime += deltaTime;
            OnTimerUpdated?.Invoke();
        }
    }
}