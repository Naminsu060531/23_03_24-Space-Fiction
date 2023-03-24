using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] int Num;
    [SerializeField] GameObject Bullet;
    [SerializeField] float Radius = 5f;
    [Range(1, 10)] public float speed;
    [Range(0, 10)] public float ShotMax;
    public float ShotDelay;
    public AudioClip ShootSFX;

    Vector2 startPos;

    private void Update()
    {
        startPos = transform.position;
        ShotDelay += Time.deltaTime;

        if (ShotDelay > ShotMax)
        {
            ShotDelay = 0;
            waveAction(Num);
        }
    }

    public void waveAction(int Num)
    {
        float angleStep = 360 / Num;
        float angle = 0;

        for (int i = 0; i < Num; i++)
        {
            SoundManager.instance.SFXPlay("Shoot", ShootSFX);

            float waveX = startPos.x + Mathf.Sin((angle * Mathf.PI) / 180) * Radius;
            float waveY = startPos.y + Mathf.Cos((angle * Mathf.PI) / 180) * Radius;

            Vector2 waveVector = new Vector2(waveX, waveY);
            Vector2 waveDir = (waveVector - startPos).normalized * speed;

            var wave = Instantiate(Bullet, startPos, Quaternion.identity);
            wave.GetComponent<Rigidbody2D>().velocity = new Vector2(waveDir.x, waveDir.y);

            angle += angleStep;
        }
    }


}