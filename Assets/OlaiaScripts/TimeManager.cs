using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameTimerSO gameTimer; // Asignas aquí tu Scriptable Object
    private bool inicializado = false;
    void Update()
    {
        // Actualiza el tiempo cada frame
        if (gameTimer.isRunning)
            gameTimer.UpdateTimer(Time.deltaTime);
    }


    public void StartTimer()
    {
        if(!inicializado)
        {
            gameTimer.ResetTimer();
            inicializado = true;
        }
        gameTimer.isRunning = true;
    }

    public void StopTimer()
    {
        gameTimer.isRunning = false;
    }

    public void ResetTimer()
    {
        gameTimer.ResetTimer();
    }
}