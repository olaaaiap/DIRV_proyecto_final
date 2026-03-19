using TMPro;
using UnityEngine;

public class ActualizarTiempoReloj : MonoBehaviour
{
    public TextMeshPro clockText;


    void Update()
    {
        clockText.text = ContarTiempoReloj.instance.UpdateClockText();
    }
}
