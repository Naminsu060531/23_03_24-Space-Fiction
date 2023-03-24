using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public BulletScript bullet;
    Vector2 direction;

    private void Start()
    {
        direction = (transform.localRotation * Vector3.up).normalized;
    }

    private void Update()
    {
        
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        BulletScript goBullet = go.GetComponent<BulletScript>();
        goBullet.direction = direction;
    }
}
