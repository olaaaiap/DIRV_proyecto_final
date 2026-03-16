using UnityEngine;
using TMPro;

public class ContarTiempoReloj : MonoBehaviour
{
    public TextMeshPro clockText;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    public void Start()
    {
        StartClock();
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
    }

    private void UpdateClockText()
    {
        
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // Formato HH:MM:SS
        clockText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}