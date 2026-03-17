using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cucaracha : MonoBehaviour
{
    public NavMeshAgent agent;

    public List<Transform> points;

    private Transform current;

    private void Start()
    {
        current = points[0];
    }

    private void Update()
    {
        agent.SetDestination(current.position);

        if(Vector3.Distance(current.position, agent.transform.position) <= .5f)
        {
            SetNewLocation();
        }
    }

    public void SetNewLocation()
    {
        Transform aux = current;
        points.Remove(current);

        current = points[Random.Range(0, points.Count)]; 

        points.Add(aux);
    }

}
