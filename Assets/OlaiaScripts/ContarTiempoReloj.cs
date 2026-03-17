using UnityEngine;
using TMPro;
using System.IO;

public class ContarTiempoReloj : MonoBehaviour
{
    private string filePath;

    public TextMeshPro clockText;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        filePath = Application.persistentDataPath + "/times.txt";
        //StartClock();
    }

    // Llama a esta función para empezar a contar
    public void StartClock()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    // Llama a esta función si quieres pausar el reloj
    public void StopClock()
    {
        isRunning = false;
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

    private void UpdateClockText()
    {
        
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // Formato HH:MM:SS
        clockText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    void SaveTime()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        File.AppendAllText(filePath, timeString + "\n");

        Debug.Log("Tiempo guardado en: " + filePath);
    }
}