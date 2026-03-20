using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameTimerSO gameTimer; //Scriptable object
    private bool inicializado = false;
    void Update()
    {
        //Actualizar tiempo del scriptable object
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