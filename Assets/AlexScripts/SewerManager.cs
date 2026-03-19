using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Content.Interaction;

public class SewerManager : MonoBehaviour
{
    private int count;
    [SerializeField] private TextMeshPro display;

    [SerializeField]  List<LeverActivator> buttons;

    private void Start()
    {
        foreach (var button in buttons) { button.OnActivated += () => Push(button); }
        UpdateText();
    }

    public void Push(LeverActivator button)
    {
        print("HOLAA");
        count++;
        UpdateText();
    }

    public void EndPush()
    {
        if(count == 3)
        {
            SceneLoadingManagement.instance.LoadNextScene();
        }
    }

    private void UpdateText()
    {
        display.text = "BUSCAR TODOS LOS BOTONES \n\n BOTONES PULSADOS \n"+count+"/3";
    }
    
}
