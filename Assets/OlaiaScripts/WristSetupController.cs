using UnityEngine;
using UnityEngine.SceneManagement;

public class WristSetupController : MonoBehaviour
{
    [Header("Prefabs / Objetos")]
    public GameObject wristSocket;   // El socket en la muñeca
    public GameObject wristWatchPrefab; // Prefab del reloj

    private GameObject instantiatedWatch;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != "Tuto_ExteriorInstituto")
        {
            // Instanciamos el reloj y lo hacemos hijo del socket
            if (wristWatchPrefab != null && wristSocket != null)
            {
                instantiatedWatch = Instantiate(wristWatchPrefab, wristSocket.transform);
                // Ajustar posición y rotación relativa al socket
                instantiatedWatch.transform.localPosition = Vector3.zero;
                instantiatedWatch.transform.localRotation = Quaternion.identity;
            }
        }
        else
        {
            // En tutorial no hacemos nada; el socket queda vacío
        }
    }
}