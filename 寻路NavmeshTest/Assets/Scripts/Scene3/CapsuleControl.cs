using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CapsuleControl : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 p = hit.point;
                _agent.SetDestination(p);
            }
        }
        
        //从link上瞬间移动
        if (_agent.isOnOffMeshLink)
        {
            _agent.CompleteOffMeshLink();
        }
    }
}