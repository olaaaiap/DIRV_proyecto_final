using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AceleradorVR : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public Transform controlador;

    public float velocidadMax = 20f;
    public Transform moto;

    public float rotacionMin = 0f;
    public float rotacionMax = -60f; // girar hacia atrás

    public Animator animator;

    public bool agarrado = false;

    void Start()
    {
        grabInteractable.selectEntered.AddListener(_ => agarrado = true);
        grabInteractable.selectExited.AddListener(_ => agarrado = false);
    }

    void Update()
    {
        if (agarrado)
        {
            animator.speed = 1f;
        }
        else
        {
            animator.speed = 0f;

        }

        //if (!agarrado) return;

        //float rotZ = ObtenerRotacion();
        //float aceleracion = ObtenerAceleracion(rotZ);

        //float velocidad = aceleracion * velocidadMax;

        //if(velocidad > 0)
        //{
        //    animator.SetBool("Going", true);
        //}

        
    }

    float ObtenerRotacion()
    {
        float rotZ = controlador.localEulerAngles.z;

        if (rotZ > 180) rotZ -= 360;

        return rotZ;
    }

    public float ObtenerAceleracion(float rotZ)
    {
        float t = Mathf.InverseLerp(rotacionMin, rotacionMax, rotZ);
        return Mathf.Clamp01(t);
    }


}