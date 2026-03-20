using System.Collections;
using UnityEngine;

public class AvatarAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform player;
    [SerializeField] private float esperaAntesDeHablar = 1.5f;
    [SerializeField] private float velocidadRotacion = 3f;
    [SerializeField] private PerseguirPlayer seguirPlayer;

    private bool reproducido = false;
    private bool playerCerca = false;
    private bool perseguir = false;


    void Update()
    {
        if (playerCerca)
        {
            MirarPlayer();
        }
        if (perseguir) 
        {
            if (seguirPlayer != null)
            {
                seguirPlayer.Seguir();

            }
           
        }
    }

    void MirarPlayer()
    {
        //Calcular la dirección
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        //Calcular la rotación hacia el player
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        //Hacer ul lerp para suavizar la rotacion
        transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, Time.deltaTime * velocidadRotacion);
    }


    private void OnTriggerEnter(Collider other)
    {
        //Si colisiona con player y si el audio no se ha reproducido..
        if (!reproducido && other.CompareTag("Player"))
        {
            //...perseguir player
            perseguir = true;
            playerCerca = true;
            player = other.transform;
            StartCoroutine(EmpezarHablar());
        }
    }

    IEnumerator EmpezarHablar()
    {
        //Reproducir audio
        reproducido = true;
        yield return new WaitForSeconds(esperaAntesDeHablar);
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);
        perseguir = false;
    }
}
