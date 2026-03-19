using UnityEngine;
using TMPro;
using System.IO;

public class ContarTiempoReloj : Singleton<ContarTiempoReloj>
{
    private string filePath;
    public GameTimerSO gameTimer;

    private TimeManager timeManager;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        filePath = Application.persistentDataPath + "/times.txt";
        //timeManager = FindFirstObjectByType<TimeManager>();
        DontDestroyOnLoad(this);
        gameTimer.isRunning = true;
        //StartClock();
    }

    // Llama a esta función para empezar a contar
    public void StartClock()
    {
        elapsedTime = 0f;
        isRunning = true;   
        if (timeManager != null)
            timeManager.StartTimer();
    }

    // Llama a esta función si quieres pausar el reloj
    public void StopClock()
    {
        isRunning = false;
        if (timeManager != null)
            timeManager.StopTimer();
    }

    // Llama a esta función para reiniciar y detener
    public void ResetClock()
    {
        elapsedTime = 0f;
        isRunning = false;
        UpdateClockText();
    }

    private void Update()
    {
        if (isRunning)
        {
            // Suma el tiempo transcurrido desde el último frame
            elapsedTime += Time.deltaTime;
            UpdateClockText();
        }

        //// Guardar tiempo al pulsar SPACE
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SaveTime();
        //}
    }

    public string UpdateClockText()
    {
        
        int hours = Mathf.FloorToInt(gameTimer.elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((gameTimer.elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(gameTimer.elapsedTime % 60f);

        // Formato HH:MM:SS
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void SaveTime()
    {

        int hours = Mathf.FloorToInt(gameTimer.elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((gameTimer.elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(gameTimer.elapsedTime % 60f);

        string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        File.AppendAllText(filePath, timeString + "\n");

        Debug.Log("Tiempo guardado en: " + filePath);
    }
}