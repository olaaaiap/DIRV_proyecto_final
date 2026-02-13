using UnityEngine;

public class MoveBackOnApproach : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;           // La cámara VR o XR Rig

    [Header("Ajustes")]
    public float triggerDistance = 2f; // Distancia a la que reacciona
    public Vector3 moveOffset = new Vector3(5f, 0f, 0f); // Aumento en ejes (por defecto +5 en X)
    public float moveSpeed = 3f;

    private Vector3 targetPosition;
    private bool hasMoved = false;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position; // Se actualizará más tarde
    }

    void Update()
    {
        if (hasMoved) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < triggerDistance && !isMoving)
        {
            isMoving = true;
            targetPosition = transform.position + moveOffset; // Aumenta en el eje indicado
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                hasMoved = true; // solo ocurre una vez
            }
        }
    }
}
