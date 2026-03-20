using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoLuces : MonoBehaviour
{
    public Light[] lights;
    public float lightDuration = 4.0f;

    public AudioSource errorAudio;
    public AudioSource dingAudio;

    public GameObject portal;

    private List<int> secuenciaLuces = new List<int>();
    private int currentStep = 0;
    private int correctStreak = 0;
    private bool empezado = false;

    void Start()
    {
        portal.SetActive(false);
        GenerateSequence(50);
        
    }

    void GenerateSequence(int length)
    {
        secuenciaLuces.Clear();
        for (int i = 0; i < length; i++)
        {
            secuenciaLuces.Add(Random.Range(0, lights.Length));
        }
    }

    IEnumerator PlaySecuencia()
    {
        for (int i = 0; i < secuenciaLuces.Count; i++)
        {
            int index = secuenciaLuces[i];
            ApagarLuces();
            lights[index].enabled = true;

            yield return new WaitForSeconds(lightDuration);
            lights[index].enabled = false;

            yield return new WaitForSeconds(0.3f);
        }

        currentStep = 0;
    }

    void ApagarLuces()
    {
        foreach (Light l in lights)
        {
            l.enabled = false;
        }
    }

   
    public void PressButton(string color)
    {
        if (!empezado)
        {
            empezado = true;
            StartCoroutine(PlaySecuencia());
        }
        else
        {
            Light currentLight = lights[secuenciaLuces[currentStep]];
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

                if (currentStep >= secuenciaLuces.Count)
                {
                    StartCoroutine(PlaySecuencia());
                }
            }
            else
            {
                Error();
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

    void Error()
    {
        errorAudio.Play();

        correctStreak = 0;
        currentStep = 0;

        StartCoroutine(PlaySecuencia());
    }
}