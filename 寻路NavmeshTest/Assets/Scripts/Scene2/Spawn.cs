using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject point;
    public GameObject end;

    public float bornTime;
    private Vector3 bornPoint;

    // Start is called before the first frame update
    void Start()
    {
        bornPoint = transform.GetChild(0).position;
        StartCoroutine(BornPoint());
    }

    IEnumerator BornPoint()
    {
        while (true)
        {
            GameObject go = Instantiate(point, bornPoint, Quaternion.identity);
            go.GetComponent<PointController>().SetTarget(end.transform.position);
            yield return new WaitForSeconds(bornTime);
        }
    }
}