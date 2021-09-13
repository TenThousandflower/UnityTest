using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapController : MonoBehaviour
{
    private NavMeshModifier[,] _modifiers;

    private NavMeshModifier[] modifierList;

    public NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        _modifiers = new NavMeshModifier[10, 10];
        modifierList = transform.GetComponentsInChildren<NavMeshModifier>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                _modifiers[i, j] = modifierList[i * 10 + j];
            }
        }

        PathInit();
        if (surface)
        {
            surface.BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PathInit();
            PathGeneration();
        }
    }

    private void PathInit()
    {
        for (int i = 0; i < modifierList.Length; i++)
        {
            modifierList[i].area = 5;
        }
    }

    private void PathGeneration()
    {
        int r = Random.Range(0, 10);
        for (int i = 0; i < 10; i++)
        {
            DirectionChoice(i, ref r);
        }

        if (surface)
        {
            surface.BuildNavMesh();
        }
    }

    private void DirectionChoice(int rows, ref int colums)
    {
        int r = Random.Range(0, 3);
        _modifiers[rows, colums].area = 0;
        switch (r)
        {
            case 0:
                break;
            case 1:
                int r1 = -1;
                while (r1 != 0)
                {
                    r1 = Random.Range(0, 2);
                    colums = Mathf.Max(colums - 1, 0);
                    _modifiers[rows, colums].area = 0;
                }

                break;
            case 2:
                int r2 = -1;
                while (r2 != 0)
                {
                    r2 = Random.Range(0, 2);
                    colums = Mathf.Min(colums + 1, 9);
                    _modifiers[rows, colums].area = 0;
                }

                break;
        }
    }
}