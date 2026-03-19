using UnityEngine;
using TMPro;
using System.IO;

public class ContarTiempoReloj : MonoBehaviour
{
    private string filePath;
    public GameTimerSO gameTimer;
    public TextMeshPro clockText;

    private TimeManager timeManager;

    private bool haEmpezado = false;

    void Start()
    {
        filePath = Application.persistentDataPath + "/times.txt";
        timeManager = FindFirstObjectByType<TimeManager>();
        //StartClock();
    }

    void OnEnable()
    {
        if (gameTimer != null)
            gameTimer.OnTimerUpdated += UpdateClockText;
    }

    void OnDisable()
    {
        if (gameTimer != null)
            gameTimer.OnTimerUpdated -= UpdateClockText;
    }

    // Llama a esta función para empezar a contar
    public void StartClock()
    {
        haEmpezado = true; 
        if (timeManager != null)
            timeManager.StartTimer();
    }

    // Llama a esta función si quieres pausar el reloj
    public void StopClock()
    {
        if (timeManager != null)
            timeManager.StopTimer();
    }

    // Llama a esta función para reiniciar y detener
    public void ResetClock()
    {
        UpdateClockText();
    }


    public void UpdateClockText()
    {
            int hours = Mathf.FloorToInt(gameTimer.elapsedTime / 3600f);
            int minutes = Mathf.FloorToInt((gameTimer.elapsedTime % 3600f) / 60f);
            int seconds = Mathf.FloorToInt(gameTimer.elapsedTime % 60f);
            // Formato HH:MM:SS
            clockText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        
        
    }

    void SaveTime()
    {

        int hours = Mathf.FloorToInt(gameTimer.elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((gameTimer.elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(gameTimer.elapsedTime % 60f);

        string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        File.AppendAllText(filePath, timeString + "\n");

        Debug.Log("Tiempo guardado en: " + filePath);
    }
}