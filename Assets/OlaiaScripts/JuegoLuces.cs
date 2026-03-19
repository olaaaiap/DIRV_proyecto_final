using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoLuces : MonoBehaviour
{
    public Light[] lights; // Luces en la escena
    public float lightDuration = 4.0f;

    public AudioSource errorAudio;
    public AudioSource dingAudio;

    public GameObject portal;

    private List<int> sequence = new List<int>();
    private int currentStep = 0;
    private int correctStreak = 0;
    private bool empezado = false;
    //private bool waitingForInput = false;

    void Start()
    {
        portal.SetActive(false);
        GenerateSequence(50); // puedes cambiar tamaño
        
    }

    void GenerateSequence(int length)
    {
        sequence.Clear();
        for (int i = 0; i < length; i++)
        {
            sequence.Add(Random.Range(0, lights.Length));
        }
    }

    IEnumerator PlaySequence()
    {
        //waitingForInput = false;

        for (int i = 0; i < sequence.Count; i++)
        {
            int index = sequence[i];

            TurnOffAllLights();
            lights[index].enabled = true;

            yield return new WaitForSeconds(lightDuration);
            lights[index].enabled = false;

            yield return new WaitForSeconds(0.3f);
        }

        //waitingForInput = true;
        currentStep = 0;
    }

    void TurnOffAllLights()
    {
        foreach (Light l in lights)
        {
            l.enabled = false;
        }
    }

    // Este método lo llamarán los botones
    public void PressButton(string color)
    {
        //if (!waitingForInput) return;

        if (!empezado)
        {
            empezado = true;
            StartCoroutine(PlaySequence());
        }
        else
        {
            Light currentLight = lights[sequence[currentStep]];
            ColorLuz colorScript = currentLight.GetComponentInParent<ColorLuz>();

            string lightColor = colorScript.color;

            if (lightColor == color)
            {
                currentStep++;
                correctStreak++;

                if (correctStreak >= 10)
                {
                    portal.SetActive(true);
                    dingAudio.Play();
                    return;
                }

                if (currentStep >= sequence.Count)
                {
                    StartCoroutine(PlaySequence());
                }
            }
            else
            {
                Fail();
            }

        }
    }

    string GetNombreColor(Color color)
    {
        if (color == Color.red) return "rojo";
        if (color == Color.blue) return "azul";
        if (color == Color.yellow) return "amarillo";

        return "error";
    }

    void Fail()
    {
        errorAudio.Play();

        correctStreak = 0;
        currentStep = 0;

        StartCoroutine(PlaySequence());
    }
}