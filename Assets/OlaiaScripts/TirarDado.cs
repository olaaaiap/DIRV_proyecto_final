using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TirarDado : MonoBehaviour
{
    public Transform player;         // Referencia al jugador o c�mara
    public float maxDistance = 5f;   // Distancia m�xima antes de reposicionar
    public float torqueForce = 5f;   // Magnitud del giro aleatorio
    public Vector3 respawnOffset = new Vector3(0, 0, 2f); // Donde aparece frente al jugador

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Llamar cuando el jugador suelta el dado
    //public void Lanzar(Vector3 manoVel, Vector3 manoAngularVel)
    //{
    //    // Aplica la velocidad de la mano
    //    rb.linearVelocity = manoVel;
    //    rb.angularVelocity = manoAngularVel;

    //    // Aplica un torque aleatorio extra para que gire
    //    Vector3 randomTorque = new Vector3(
    //        Random.Range(-1f, 1f),
    //        Random.Range(-1f, 1f),
    //        Random.Range(-1f, 1f)
    //    ) * torqueForce;
    //    rb.AddTorque(randomTorque, ForceMode.Impulse);
    //}

    public void LanzarWrapper()
    {
        // Llamamos al método interno usando args nulos o valores por defecto
        // Nota: si quieres la velocidad de la mano, es mejor manejarlo en código
        Rigidbody manoRb = null; // Aquí podrías poner la referencia si quieres
        Vector3 manoVel = manoRb ? manoRb.linearVelocity : Vector3.zero;
        Vector3 manoAngVel = manoRb ? manoRb.angularVelocity : Vector3.zero;

        rb.linearVelocity = manoVel;
        rb.angularVelocity = manoAngVel;

        Vector3 randomTorque = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ) * torqueForce;
        rb.AddTorque(randomTorque, ForceMode.Impulse);
    }

    void Update()
    {
        // Si el dado se aleja demasiado, reposici�n frente al jugador
        float distancia = Vector3.Distance(transform.position, player.position);
        if (distancia > maxDistance)
        {
            Vector3 newPos = player.position + player.forward * respawnOffset.z + Vector3.up * respawnOffset.y;
            transform.position = newPos;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Opcional: aplicar torque peque�o para realismo
            Vector3 smallTorque = new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f)
            );
            rb.AddTorque(smallTorque, ForceMode.Impulse);
        }
    }
}