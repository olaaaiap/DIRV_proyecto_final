using UnityEngine;
using UnityEngine.SceneManagement;

public class WristSetupController : MonoBehaviour
{
    public GameObject wristSocketController;//El socket en la muñeca
    public GameObject wristWatchPrefab; //Prefab del reloj

    private GameObject instanciaReloj;

    public Transform leftControllerAttach;
    public Transform leftHandAttach;
    private bool estabaManoActiva;

    void Start()
    {
        estabaManoActiva = ManosActivas();
        HandleWatch();
    }

    void Update()
    {
        bool manosActivas = ManosActivas();

        if (manosActivas != estabaManoActiva)
        {
            estabaManoActiva = manosActivas;
            HandleWatch();
        }
    }

    bool ManosActivas()
    {
        return leftHandAttach != null && leftHandAttach.gameObject.activeInHierarchy;
    }

    void HandleWatch()
    {

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Tuto_ExteriorInstituto")
            return;

        if (instanciaReloj != null)
        {
            Destroy(instanciaReloj);
        }

        if (!ManosActivas())
        {
            if (leftControllerAttach != null)
            {
                if (wristWatchPrefab != null && wristSocketController != null)
                {
                    instanciaReloj = Instantiate(wristWatchPrefab, wristSocketController.transform);
                }
            }
        }

        if (instanciaReloj != null)
        {
            instanciaReloj.transform.localPosition = Vector3.zero;
            instanciaReloj.transform.localRotation = Quaternion.identity;

        }
    
}
}