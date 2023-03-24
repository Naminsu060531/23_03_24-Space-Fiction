using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    Color OrigColor;

    public int EnemyHP;
    public int EnemySpeed;
    public float EnemyShotDelay;
    public float EnemyShotDelayMax;
    public int EnemyDmg;

    public int EnemyType;
    public int EnemyMoveType;
    public int EnemyAttackType;
    public int EnemyDestroyType;

    public GameObject Trigger;

    Player playerScript;

    public GameObject EnemyBullet;

    public Vector3 Target;

    public GameObject[] Item;
    public int ItemValue;
    public int ItemDrop;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        OrigColor = spriteRenderer.material.color;

        switch(EnemyType)
        {
            case 0: //Asteroid
                if (GameManager.instance.stageValue == 1)
                {
                    EnemyHP = 1;
                    EnemyDmg = 10;
                    EnemySpeed = 10;

                    EnemyMoveType = 0;
                }
                if (GameManager.instance.stageValue == 2)
                {
                    EnemyHP = 2;
                    EnemyDmg = 10;
                    EnemySpeed = 10;

                    EnemyMoveType = 0;
                }
                if (GameManager.instance.stageValue == 3)
                {
                    EnemyHP = 3;
                    EnemyDmg = 15;
                    EnemySpeed = 10;

                    EnemyMoveType = 0;
                }

                break;
            case 1: //enemy1_Attack

                if (GameManager.instance.stageValue == 1)
                {
                    EnemyHP = 2;
                    EnemyDmg = 2;
                    EnemySpeed = 5;
                    EnemyShotDelayMax = 1;

                    EnemyAttackType = 1;
                    EnemyMoveType = 1;
                }
                if (GameManager.instance.stageValue == 2)
                {
                    EnemyHP = 6;
                    EnemyDmg = 2;
                    EnemySpeed = 5;
                    EnemyShotDelayMax = 2;

                    EnemyAttackType = 1;
                    EnemyMoveType = 1;
                }
                if (GameManager.instance.stageValue == 3)
                {
                    EnemyHP = 10;
                    EnemyDmg = 2;
                    EnemySpeed = 5;
                    EnemyShotDelayMax = 2;

                    EnemyAttackType = 1;
                    EnemyMoveType = 1;
                }

                break;
            case 2: //enemy2_Follow

                if (GameManager.instance.stageValue == 1)
                {
                    EnemyHP = 2;
                    EnemyDmg = 2;
                    EnemySpeed = 2;

                    EnemyAttackType = 2;
                    EnemyMoveType = 2;
                }
                if (GameManager.instance.stageValue == 2)
                {
                    EnemyHP = 10;
                    EnemyDmg = 2;
                    EnemySpeed = 3;

                    EnemyAttackType = 2;
                    EnemyMoveType = 2;
                }
                if (GameManager.instance.stageValue == 3)
                {
                    EnemyHP = 15;
                    EnemyDmg = 2;
                    EnemySpeed = 5;

                    EnemyAttackType = 2;
                    EnemyMoveType = 2;
                }

                break;
            case 3: //enemy3_Spawn
              
                if (GameManager.instance.stageValue == 1)
                {
                    EnemyHP = 3;
                    EnemyDmg = 2;
                    EnemySpeed = 2;

                    EnemyAttackType = 3;
                    EnemyMoveType = 3;
                }
                if (GameManager.instance.stageValue == 2)
                {
                    EnemyHP = 10;
                    EnemyDmg = 2;
                    EnemySpeed = 2;

                    EnemyAttackType = 3;
                    EnemyMoveType = 3;

                }
                if (GameManager.instance.stageValue == 3)
                {
                    EnemyHP = 15;
                    EnemyDmg = 2;
                    EnemySpeed = 2;

                    EnemyAttackType = 3;
                    EnemyMoveType = 3;

                }

                break;
        }
    }

    private void Update()
    {
        EnemyShotDelay += Time.deltaTime;

        Target = GameObject.FindWithTag("Player").transform.position;

        if(EnemyShotDelay > EnemyShotDelayMax)
        {

            switch (EnemyAttackType)
            {
                case 0: //Asteroid



                    break;
                case 1: //enemy1_Attack

                    Instantiate(EnemyBullet, transform.position, Quaternion.identity);

                    break;
                case 2: //enemy2_Follow


                    break;
                case 3: //enemy3_Spawn

                    Instantiate(EnemyBullet, transform.position, Quaternion.identity);

                    break;
            }

            EnemyShotDelay = 0;
        }

        switch (EnemyMoveType)
        {
            case 0: //Asteroid
                transform.position += Vector3.down * EnemySpeed * Time.deltaTime;
                transform.Rotate(0, 0, 10);

                break;
            case 1: //enemy1_Attack
                transform.position += Vector3.down * EnemySpeed * Time.deltaTime;

                break;
            case 2: //enemy2_Follow
                Vector3 Pos = transform.position;

                Vector3 Dir = Target - Pos;

                transform.position += Dir * EnemySpeed * Time.deltaTime;

                float angle = Mathf.Atan2(Target.y, Target.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

                float distance = Vector3.Distance(gameObject.transform.position, Target);

                if(distance < 2.5)
                {
                    print("ÁøÀÔ");
                    transform.position -= Dir * EnemySpeed * Time.deltaTime;
                }

                break;
            case 3: //enemy3_Spawn
                transform.position += Vector3.down * EnemySpeed * Time.deltaTime;

                break;
        }

        switch (EnemyDestroyType)
        {
            case 0: //Asteroid


                break;
            case 1: //enemy1_Attack


                break;
            case 2: //enemy2_Follow


                break;
            case 3: //enemy3_Spawn
                

                break;
        }

        if(EnemyHP <= 0)
        {
            ItemDrop = Random.Range(0, 2);
            
            switch(ItemDrop)
            {
                case 0:

                    DataManager.CurScore += 10;

                    break;
                case 1:

                    ItemValue = Random.Range(1, 11);

                    DataManager.CurScore += 15;

                    if (1 <= ItemValue && ItemValue < 3)
                    {
                        Instantiate(Item[0], transform.position, Quaternion.identity); 
                    }
                    else if (3 <= ItemValue && ItemValue < 5)
                    {
                        Instantiate(Item[1], transform.position, Quaternion.identity);
                    }
                    else if (5 <= ItemValue && ItemValue < 7)
                    {
                        Instantiate(Item[2], transform.position, Quaternion.identity);
                    }
                    else if (7 <= ItemValue && ItemValue <= 10)
                    {
                        Instantiate(Item[3], transform.position, Quaternion.identity);
                    }

                    break;
            }
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerShot")
        {
            StartCoroutine(Flash());
            EnemyHP -= 1;
            DataManager.CurScore += 10;
            Instantiate(Trigger, transform.position, Quaternion.identity);
        }

        if (collision.tag == "Player" && EnemyType == 0)
        {
            playerScript = collision.GetComponent<Player>();
            playerScript.HP.value -= EnemyDmg;
        }

        if(collision.tag == "Boom")
        {
            EnemyHP -= 20;
            DataManager.CurScore += 30;
        }

    }

    IEnumerator Flash()
    {
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(1);
        spriteRenderer.material.color = OrigColor;
    }
}
