using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class StartContador : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor wristSocket;
    [SerializeField] private XRGrabInteractable watch;
    [SerializeField] private AudioSource errorAudio;
    [SerializeField] private AudioSource okAudio;
    [SerializeField] private GameObject tablas;

    private ContarTiempoReloj contadorReloj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Busca la primera instancia de ContarTiempoReloj en la escena
        contadorReloj = FindFirstObjectByType<ContarTiempoReloj>();

    }

    public void PushButton()
    {
        if (contadorReloj == null || wristSocket == null) return;

        if (wristSocket.interactablesSelected.Contains(watch))
        {
            contadorReloj.StartClock();
            if (tablas != null)
                tablas.SetActive(false);
            if (okAudio != null)
                okAudio.Play();
        }
        else
        {
            if (errorAudio != null)
                errorAudio.Play();
        }

        
    }
}
