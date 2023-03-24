using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurret : MonoBehaviour
{
    public GameObject Bullet;
    public Vector3 StartPos;
    public float XRange = 5f;
    public float ShotMax;
    public float ShotDelay;
    public float Speed;

    void Start()
    {
    }


    void Update()
    {
        ShotDelay += Time.deltaTime;

        /*

        StartPos = GameObject.Find("Player").transform.position;

        Vector3 Vec = StartPos;

        Vec.x = XRange * Mathf.Sin(Time.time * Speed);

        transform.position = Vec;

        */

        if (ShotDelay > ShotMax)
        {
            ShotDelay = 0;
            Instantiate(Bullet, transform.position, Quaternion.identity);
        }

    }
}
