using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Visagra : MonoBehaviour
{
    public XRGrabInteractable grabInteractable; // referencia al XRGrabInteractable de la tapa
    public float maxOpenAngle = 90f; // ángulo máximo de apertura
    public float minOpenAngle = 0f; // ángulo cerrado

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        if (grabInteractable.isSelected)
        {
            // Calcular la rotación relativa al pivot
            Vector3 localEuler = transform.localEulerAngles;
            float angle = localEuler.x;

            // Ajustar rango de 0 a 360
            if (angle > 180f) angle -= 360f;

            // Limitar ángulo
            angle = Mathf.Clamp(angle, minOpenAngle, maxOpenAngle);

            // Aplicar rotación
            transform.localEulerAngles = new Vector3(angle, 0, 0);
        }
    }
}