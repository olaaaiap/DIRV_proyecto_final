using UnityEngine;


public class StartContador : MonoBehaviour
{
    private ContarTiempoReloj contadorReloj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Busca la primera instancia de ContarTiempoReloj en la escena
        contadorReloj = FindFirstObjectByType<ContarTiempoReloj>();

    }

    public void PushButton()
    {
        if (contadorReloj != null)
        {
            contadorReloj.StartClock();
        }
    }
}
