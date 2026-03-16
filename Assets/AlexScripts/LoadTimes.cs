using System.IO;
using TMPro;
using UnityEngine;

public class LoadTimes : MonoBehaviour
{
    public TextMeshPro textOutput;

    void Start()
    {
        LoadFile();
    }

    void LoadFile()
    {
        string filePath = Application.persistentDataPath + "/times.txt";

        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            textOutput.text = content;
        }
        else
        {
            textOutput.text = "No hay tiempos guardados.";
        }
    }
}
