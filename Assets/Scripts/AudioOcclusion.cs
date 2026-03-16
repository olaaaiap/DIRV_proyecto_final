using System.Security.Cryptography;
using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]
[RequireComponent(typeof(LineRenderer))]
public class AudioOcclusion : MonoBehaviour
{
    public Transform player;
    public LayerMask wallLayer;

    public float occludedVolume = 0.4f;
    public float normalVolume = 1f;

    public float occludedCutoff = 2000f;
    public float normalCutoff = 22000f;

    private AudioSource audioSource;
    private AudioLowPassFilter lowPass;
    private LineRenderer line;

    public float sphereRadius = 0.05f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lowPass = GetComponent<AudioLowPassFilter>();
        line = GetComponent<LineRenderer>();

        line.positionCount = 2;
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;
    }

    //void Update()
    //{
    //    if (player == null) return;

    //    //Vector3 direction = player.position - transform.position;
    //    //float distance = direction.magnitude;

    //    // Obtenemos todos los hits
    //    //RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, wallLayer);
    //    Vector3 origin = transform.position;
    //    Vector3 target = player.position;

    //    Vector3 direction = (target - origin).normalized;
    //    float distance = Vector3.Distance(origin, target);

    //    //RaycastHit[] hits = Physics.RaycastAll(origin, direction, distance);
    //    RaycastHit[] hits = Physics.SphereCastAll(origin, sphereRadius, direction, distance);

    //    if (hits.Length > 0)
    //    {
    //        // Hay al menos una pared
    //        audioSource.volume = occludedVolume;
    //        lowPass.cutoffFrequency = occludedCutoff;

    //        line.enabled = false;

    //        foreach (RaycastHit hit in hits)
    //        {
    //            Debug.Log("Rayo chocando con: " + hit.collider.name);
    //        }
    //    }
    //    else
    //    {
    //        // No hay pared
    //        Debug.Log("No hay pared entre fuente y jugador");

    //        audioSource.volume = normalVolume;
    //        lowPass.cutoffFrequency = normalCutoff;

    //        line.enabled = true;
    //        line.SetPosition(0, transform.position);
    //        line.SetPosition(1, player.position);
    //        Debug.DrawLine(transform.position, player.position, Color.green);
    //    }
    //}


    void Update()
    {
        if (player == null) return;

        Vector3 origin = transform.position;
        Vector3 target = player.position;

        Vector3 direction = (target - origin).normalized;
        float distance = Vector3.Distance(origin, target);

        // mover un poco el origen para evitar empezar dentro de colliders
        origin += direction * 0.02f;

        RaycastHit[] hits = Physics.SphereCastAll(origin, sphereRadius, direction, distance);

        bool wallDetected = false;

        foreach (RaycastHit hit in hits)
        {
            // ignorar el propio objeto
            if (hit.collider.gameObject == gameObject) continue;

            wallDetected = true;
        }

        if (wallDetected)
        {
            audioSource.volume = occludedVolume;
            lowPass.cutoffFrequency = occludedCutoff;

            line.enabled = false;
        }
        else
        {
            Debug.Log("No hay pared entre fuente y jugador");

            audioSource.volume = normalVolume;
            lowPass.cutoffFrequency = normalCutoff;

            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, player.position);

            Debug.DrawLine(transform.position, player.position, Color.green);
        }
    }


}