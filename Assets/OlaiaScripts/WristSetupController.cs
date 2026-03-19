using UnityEngine;
using UnityEngine.SceneManagement;

public class WristSetupController : MonoBehaviour
{
    [Header("Prefabs / Objetos")]
    public GameObject wristSocketController;   // El socket en la muñeca
    public GameObject wristWatchPrefab; // Prefab del reloj

    private GameObject instantiatedWatch;

    public Transform leftControllerAttach;
    public Transform leftHandAttach;
    private bool wasHandActive;

    void Start()
    {
        wasHandActive = IsHandActive();
        HandleWatch();
    }

    void Update()
    {
        bool isHandActive = IsHandActive();

        // Detectar cambio de estado (mano <-> mando)
        if (isHandActive != wasHandActive)
        {
            wasHandActive = isHandActive;
            HandleWatch();
        }
    }

    bool IsHandActive()
    {
        return leftHandAttach != null && leftHandAttach.gameObject.activeInHierarchy;
    }

    void HandleWatch()
    {

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Tuto_ExteriorInstituto")
            return;

        // Eliminar reloj anterior si existe
        if (instantiatedWatch != null)
        {
            Destroy(instantiatedWatch);
        }

        if (!IsHandActive())
        {
            if (leftControllerAttach != null)
            {
                if (wristWatchPrefab != null && wristSocketController != null)
                {
                    instantiatedWatch = Instantiate(wristWatchPrefab, wristSocketController.transform);
                }
            }
        }

        if (instantiatedWatch != null)
        {
            instantiatedWatch.transform.localPosition = Vector3.zero;
            instantiatedWatch.transform.localRotation = Quaternion.identity;

        }
    
}
}