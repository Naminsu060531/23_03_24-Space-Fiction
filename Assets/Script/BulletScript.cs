using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;
    public int Dmg;
    public AudioClip Trigger;
    public AudioClip Shoot;

    public Vector2 direction = new Vector2(0, 1);
    public Vector2 velocity;

    private void Start()
    {
        SoundManager.instance.SFXPlay("Shoot", Shoot);
    }

    private void Update()
    {
        velocity = direction * Speed;

        /*
        print("Direction : " + direction);

        print("Velocity : " + velocity);
        */
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
 
        //float distance = Vector3.Distance(a, b);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            SoundManager.instance.SFXPlay("Trigger", Trigger);
            Destroy(gameObject);
        }

        if (collision.tag == "Boss")
        {
            DataManager.CurScore += 10;
            SoundManager.instance.SFXPlay("Trigger", Trigger);
            Destroy(gameObject);
        }
    }
}
