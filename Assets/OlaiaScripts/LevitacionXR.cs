using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
public class LevitacionXR : MonoBehaviour
{
    [Header("Acciones XR")]
    public InputActionReference gripPosition;  // XRI Left/Grip Position
    public InputActionReference joystickAction; // XRI Left Locomotion/Move

    [Header("Parámetros de levitación")]
    public float velocidad = 2f;
    public float alturaMax = 3f;
    public float alturaMin = 0.5f;
    public float duracionMax = 5f;
    public float gravedad = 9.81f;

    private CharacterController cc;
    private bool levitando = false;
    private float tiempoLevitando = 0f;
    private float verticalVel = 0f;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        Debug.Log("Grip Value: " + (gripPosition != null ? gripPosition.action.ReadValue<float>() : "No Grip Action"));
        Debug.Log("Joystick Value: " + (joystickAction != null ? joystickAction.action.ReadValue<Vector2>() : "No Joystick Action"));
        if (gripPosition == null || joystickAction == null) return;

        // Leer Grip Position y convertirlo a "presionado" si > 0.5
        float gripValue = gripPosition.action.ReadValue<float>();
        bool gripPressed = gripValue > 0.5f;

        // Leer joystick (Vector2)
        Vector2 joystick = joystickAction.action.ReadValue<Vector2>();
        float moveY = joystick.y;

        if (gripPressed && tiempoLevitando < duracionMax)
        {
            if (!levitando)
            {
                levitando = true;
                verticalVel = 0f; // reset vertical velocity
            }

            tiempoLevitando += Time.deltaTime;

            // Movimiento horizontal + vertical
            Vector3 move = new Vector3(joystick.x, moveY, joystick.y) * velocidad;

            // Limitar altura
            float nuevaAltura = transform.position.y + move.y * Time.deltaTime;
            if (nuevaAltura > alturaMax) move.y = (alturaMax - transform.position.y) / Time.deltaTime;
            if (nuevaAltura < alturaMin) move.y = (alturaMin - transform.position.y) / Time.deltaTime;

            // Aplicar movimiento con CharacterController
            cc.Move(move * Time.deltaTime);

            verticalVel = 0f; // no gravedad mientras levita
        }
        else
        {
            if (levitando)
            {
                levitando = false;
                tiempoLevitando = 0f;
            }

            // Aplicar gravedad manual
            if (!cc.isGrounded)
                verticalVel -= gravedad * Time.deltaTime;
            else
                verticalVel = 0f;

            cc.Move(new Vector3(0f, verticalVel, 0f) * Time.deltaTime);
        }
    }
}