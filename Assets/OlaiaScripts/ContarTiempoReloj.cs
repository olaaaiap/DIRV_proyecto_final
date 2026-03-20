using UnityEngine;
using TMPro;
using System.IO;

public class ContarTiempoReloj : Singleton<ContarTiempoReloj>
{
    public GameTimerSO gameTimer;
    private string filePath;
    private TimeManager timeManager;
    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        filePath = Application.persistentDataPath + "/times.txt";
        timeManager = FindFirstObjectByType<TimeManager>();
        DontDestroyOnLoad(this);
        gameTimer.isRunning = true;
    }

    //Empezar a contar
    public void StartClock()
    {
        elapsedTime = 0f;
        isRunning = true;
        if (timeManager != null)
            timeManager.StartTimer();
    }


    private void Update()
    {
        if (isRunning)
        {
            //Sumar el tiempo transcurrido desde el último frame
            elapsedTime += Time.deltaTime;
            UpdateClockText();
        }

    }

    public string UpdateClockText()
    {
        //Actualziar el texto del reloj
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