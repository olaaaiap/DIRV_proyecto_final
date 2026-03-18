using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cucaracha : MonoBehaviour
{
    private bool sleeping;

    public NavMeshAgent agent;

    public List<Transform> points;

    private Transform current;

    private void Start()
    {
        current = points[0];
    }

    private void Update()
    {
        if (sleeping) return;

        agent.SetDestination(current.position);

        if(Vector3.Distance(current.position, agent.transform.position) <= .5f)
        {
            SetNewLocation();
        }
    }

    public void SetNewLocation()
    {
        int sleep = Random.Range(0, 3);

        if(sleep == 0) { sleeping = true; StartCoroutine(SleepCoroutine()); }

        SelectNewLocation();
    }

    private IEnumerator SleepCoroutine()
    {
        yield return new WaitForSeconds(3f);

        sleeping = false;

        SelectNewLocation();
    }

    private void SelectNewLocation()
    {
        Transform aux = current;
        points.Remove(current);

        current = points[Random.Range(0, points.Count)];

        points.Add(aux);
    }

}
