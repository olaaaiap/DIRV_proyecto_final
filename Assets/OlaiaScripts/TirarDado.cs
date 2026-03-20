using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TirarDado : MonoBehaviour
{
    public Transform player;      
    public float maxDistance = 5f; 
    public float torqueForce = 5f; 
    public Vector3 respawnOffset = new Vector3(0, 0, 2f); 
    private int intentosSin3 = 0;
    public int intentosParaForzar = 4;
    public AudioSource audioCorrect;
    private bool yaEvaluado = false;
    private Rigidbody rb;
    public GameObject[] tablas;
    public Light pointLight;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void LanzarWrapper()
    {
        yaEvaluado = false;

        bool forzar3 = intentosSin3 >= intentosParaForzar;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (forzar3)
        {
            StartCoroutine(ForzarResultado3());
            intentosSin3 = 0;
        }
        else
        {
            Vector3 randomTorque = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ) * torqueForce;

            rb.AddTorque(randomTorque, ForceMode.Impulse);
        }
    }


    void FixedUpdate()
    {
        if (yaEvaluado) return;

        if (rb.linearVelocity.magnitude < 0.1f && rb.angularVelocity.magnitude < 0.1f)
        {
            yaEvaluado = true;

            if (EsCara3())
            {
                intentosSin3 = 0;
                if (audioCorrect != null)
                    audioCorrect.Play();

                DesactivarTablas();
            }
            else
            {
                intentosSin3++;
            }
        }
    }

    IEnumerator ForzarResultado3()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        //Forzar la rotación del dado
        transform.rotation = Quaternion.Euler(0, 0, 0);

        yield return new WaitForSeconds(0.5f);

        rb.isKinematic = false;
    }

    bool EsCara3()
    {
        //Se que la cara 3 es donde y apunta hacia arriba
        float dot = Vector3.Dot(transform.up, Vector3.up);
        return dot > 0.9f;
    }


    void Update()
    {
        float distancia = Vector3.Distance(transform.position, player.position);
        if (distancia > maxDistance)
        { 
            //Acercar dado para que no se vaya muy lejos
            Vector3 newPos = player.position + player.forward * respawnOffset.z + Vector3.up * respawnOffset.y;
            transform.position = newPos;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            
            Vector3 smallTorque = new Vector3( Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f),  Random.Range(-0.5f, 0.5f) );
            rb.AddTorque(smallTorque, ForceMode.Impulse);
        }
    }

    void DesactivarTablas()
    {
        foreach (GameObject tabla in tablas)
        {
            if (tabla != null)
                tabla.SetActive(false);
        }

        if (pointLight != null)
        {
            pointLight.enabled = true;
        }
    }
}