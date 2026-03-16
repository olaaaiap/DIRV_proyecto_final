using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    public float speed = 2f;
    public float patrolDistance = 5f;

    private Vector3 startPosition;
    private bool goingForward = true;
    private Animator animator;

    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetBool("Walking", true);
        }
    }

    void Update()
    {
        if (goingForward)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(startPosition, transform.position) >= patrolDistance)
            {
                goingForward = false;
                transform.Rotate(0, 180, 0);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(startPosition, transform.position) <= 0.1f)
            {
                goingForward = true;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}

