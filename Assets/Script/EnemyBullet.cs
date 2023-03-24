using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Player playerScript;

    public int BulletType;

    public float Speed;

    public Vector3 Target;

    public AudioClip ShootSFX;

    public AudioClip TriggerBoom;

    private void Start()
    {
        SoundManager.instance.SFXPlay("SFX", ShootSFX);

    }

    private void Update()
    {
        switch(BulletType)
        {
            case 0:
                transform.position += Vector3.down * Speed * Time.deltaTime;

                break;
            case 1:
                Target = GameObject.FindWithTag("Player").transform.position;

                //transform.position += Vector3.down * Speed * Time.deltaTime;

                Vector3 Pos = transform.position;

                Vector3 Dir = Target - Pos;

                float angle = Mathf.Atan2(Target.y, Target.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.position += Dir * Speed * Time.deltaTime;

                Destroy(gameObject, 1.75f);
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();

        if (collision.tag == "Player")
        {

            playerScript.HP.value -= 3;

            Destroy(gameObject);
        }

        if(collision.tag == "Boom")
        {
            Destroy(gameObject);
        }
    }

}
