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

    public float holdTimeToActivate = 2f;
    public float maxHeight = 10f;
    public float maxLevitationTime = 5f;

    public InputActionReference buttonX;

    private bool levitating = false;
    private float holdTimer = 0f;
    private float levitationTimer = 0f;

    private float verticalVelocity = 0f;

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
        bool handsDetected = leftHand != null && rightHand != null &&
                             leftHand.gameObject.activeInHierarchy &&
                             rightHand.gameObject.activeInHierarchy;

        if (!levitating)
        {
            CheckLevitationActivation();
        }
        else
        {
            levitationTimer += Time.deltaTime;

            if (levitationTimer >= maxLevitationTime)
            {
                levitating = false;
                levitationTimer = 0f;
                holdTimer = 0f;

                if (gravityProvider != null)
                    gravityProvider.useGravity = true;
            }
        }

        if (handsDetected)
        {
            HandleLevitationMovement();
        }
    }

    void CheckLevitationActivation()
    {
        if (buttonX.action.IsPressed())
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTimeToActivate && !levitating)
            {
                levitating = true;
                levitationTimer = 0f;

                if (audioSource != null)
                    audioSource.Play();

            }
        }
        else
        {
            holdTimer = 0f;
        }
    }

    void HandleLevitationMovement()
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

        if (levitating)
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

        if (levitating)
        {
            if (gravityProvider != null)
                gravityProvider.useGravity = false;

            verticalVelocity = Mathf.Lerp(verticalVelocity, targetVertical, Time.deltaTime * 3f);

            Vector3 move = horizontalMove + Vector3.up * verticalVelocity * Time.deltaTime;

            if (characterController.transform.position.y + move.y > maxHeight)
            {
                move.y = maxHeight - characterController.transform.position.y;
                verticalVelocity = 0f;
            }

            characterController.Move(move);
        }
        else
        {
            if (characterController.isGrounded)
            {
                if (gravityProvider != null)
                    gravityProvider.useGravity = true;

                verticalVelocity = -0.5f;
            }
            else
            {
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
            }

            Vector3 move = horizontalMove + Vector3.up * verticalVelocity * Time.deltaTime;
            characterController.Move(move);
        }
    }
}