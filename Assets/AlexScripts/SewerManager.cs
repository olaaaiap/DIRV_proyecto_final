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

    [SerializeField]  List<XRLever> buttons;

    private void Start()
    {
        foreach (var button in buttons) { button.onLeverActivate.AddListener(() => Push(button)); }
        UpdateText();
    }

    public void Push(XRLever button)
    {
        count++;
        UpdateText();
        button.onLeverActivate.RemoveAllListeners();
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
