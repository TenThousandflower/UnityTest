using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointController : MonoBehaviour
{
    public NavMeshAgent _agent;
    private Vector3 _waypoint = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (_waypoint != Vector3.zero)
        {
            if (_agent.isOnNavMesh)
            {
                _agent.SetDestination(_waypoint);
            }
        }
    }

    public void SetTarget(Vector3 waypoint)
    {
        _waypoint = waypoint;
        _agent.SetDestination(waypoint);
    }
}