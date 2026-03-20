using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;

public class Levitacion : MonoBehaviour
{
    public Transform xrRig;
    public Transform leftHand;
    public Transform rightHand;
    public CharacterController characterController;
    public GravityProvider gravityProvider;
    public AudioSource audioSource;
    public float moveSpeed = 2f;
    public float speed = 2f;
    public float tiempoMantenerX = 2f;
    public float maxHeight = 10f;
    public float tiempoMaxLevitacion = 15f;

    public InputActionReference buttonX;

    private bool levitando = false;
    private float temporizadorBoton = 0f;
    private float temporizadorLevitacion = 0f;

    private float velocidad = 0f;

    void OnEnable()
    {
        buttonX.action.Enable();
    }

    void OnDisable()
    {
        buttonX.action.Disable();
    }

    void Update()
    {
        bool manosDetectadas = leftHand != null && rightHand != null && leftHand.gameObject.activeInHierarchy && rightHand.gameObject.activeInHierarchy;

        if (!levitando)
        {
            ComprobarLevitacion();
        }
        else
        {
            temporizadorLevitacion += Time.deltaTime;

            if (temporizadorLevitacion >= tiempoMaxLevitacion)
            {
                levitando = false;
                temporizadorLevitacion = 0f;
                temporizadorBoton = 0f;

                if (gravityProvider != null)
                    gravityProvider.useGravity = true;
            }
        }

        if (manosDetectadas)
        {
            Levitar();
        }
    }

    void ComprobarLevitacion()
    {
        if (buttonX.action.IsPressed())
        {
            temporizadorBoton += Time.deltaTime;

            if (temporizadorBoton >= tiempoMantenerX && !levitando)
            {
                levitando = true;
                temporizadorLevitacion = 0f;

                if (audioSource != null)
                    audioSource.Play();

            }
        }
        else
        {
            temporizadorBoton = 0f;
        }
    }

    void Levitar()
    {
        float rightPalmRotationZ = rightHand.rotation.eulerAngles.z;
        float leftPalmRotationZ = leftHand.rotation.eulerAngles.z;

        float targetVertical = 0f;

        if ((rightPalmRotationZ < 20f || rightPalmRotationZ > 335f))
        {
            targetVertical = -speed;
        }
        else if ((rightPalmRotationZ > 180f && rightPalmRotationZ < 220f))
        {
            targetVertical = speed;
        }

        Vector3 horizontalMove = Vector3.zero;

        if (levitando)
        {
            if ((leftPalmRotationZ > 155f && leftPalmRotationZ < 220f))
            {
                horizontalMove += Camera.main.transform.forward * moveSpeed * Time.deltaTime;
            }
            else if ((leftPalmRotationZ < 20f || leftPalmRotationZ > 335f))
            {
                horizontalMove -= Camera.main.transform.forward * moveSpeed * Time.deltaTime;
            }
        }

        if (levitando)
        {
            if (gravityProvider != null)
                gravityProvider.useGravity = false;

            velocidad = Mathf.Lerp(velocidad, targetVertical, Time.deltaTime * 3f);

            Vector3 move = horizontalMove + Vector3.up * velocidad * Time.deltaTime;

            if (characterController.transform.position.y + move.y > maxHeight)
            {
                move.y = maxHeight - characterController.transform.position.y;
                velocidad = 0f;
            }

            characterController.Move(move);
        }
        else
        {
            if (characterController.isGrounded)
            {
                if (gravityProvider != null)
                    gravityProvider.useGravity = true;

                velocidad = -0.5f;
            }
            else
            {
                velocidad += Physics.gravity.y * Time.deltaTime;
            }

            Vector3 move = horizontalMove + Vector3.up * velocidad * Time.deltaTime;
            characterController.Move(move);
        }
    }
}