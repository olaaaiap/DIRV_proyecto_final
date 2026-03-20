using UnityEngine;
using UnityEngine.AI;

public class PerseguirPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distanciaMinima = 2f;
    [SerializeField] private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    

    public void Seguir()
    {
        //Calcular distancia entre el avatar y el jugador
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia > distanciaMinima)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }
    }
}