using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public float speed = 1f;
    public float mouseSpeed = 60f;
    private float h;

    private float v;
    private float mouse;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(-v, -mouse * mouseSpeed, h) * speed * Time.deltaTime, Space.World);
    }
}