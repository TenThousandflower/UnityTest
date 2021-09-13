using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QiaoController : MonoBehaviour
{
    private NavMeshObstacle navMeshObstacle;

    // Start is called before the first frame update
    void Start()
    {
        navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (navMeshObstacle)
            {
                navMeshObstacle.enabled = false;
                GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            if (navMeshObstacle)
            {
                navMeshObstacle.enabled = true;
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}