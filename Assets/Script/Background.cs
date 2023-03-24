using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [Range(0, 100)] public float DownSpeed;

    public Vector3 Target;

    public Vector3 StartPos;

    private void Start()
    {
        StartPos = transform.position;
    }

    private void Update()
    {
        Target = GameObject.Find("Player").transform.position;

        transform.position += Vector3.down * DownSpeed * Time.deltaTime;

    }

    
}
