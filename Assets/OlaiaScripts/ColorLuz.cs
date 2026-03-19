using UnityEngine;

public class ColorLuz : MonoBehaviour
{
    public string color;
    public JuegoLuces game;

    public void OnPress()
    {
        game.PressButton(color);
    }
}
