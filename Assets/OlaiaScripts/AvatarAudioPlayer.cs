using System.Collections;
using UnityEngine;

public class AvatarAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform player;
    public float delayBeforeTalking = 1.5f;
    public float rotationSpeed = 3f;

    private bool hasPlayed = false;
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby)
        {
            LookAtPlayer();
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // opcional: mantener la cabeza nivelada

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Ajuste de 90 grados en Y
        Quaternion offset = Quaternion.Euler(0, 180f, 0); // cambia a -90f si va al otro lado
        targetRotation *= offset;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (!hasPlayed && other.CompareTag("Player"))
        {
            playerNearby = true;
            player = other.transform;
            StartCoroutine(StartTalking());
        }
    }

    IEnumerator StartTalking()
    {
        hasPlayed = true;

        yield return new WaitForSeconds(delayBeforeTalking);

        audioSource.Play();
    }
}
