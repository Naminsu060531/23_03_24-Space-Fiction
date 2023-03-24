using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public GameObject BossSpawnPanel;

    public int BossHP;
    public Slider BossHpSlider;

    public int BossType;
    public int BossAttackType;
    public int BossMoveType;
    public int BossDestroyType;

    public float ShotMax;
    public float ShotDelay;

    public SpriteRenderer spriteRenderer;
    Color OrigColor;

    public AudioClip BossSpawnSFX;
    public AudioClip BossDestroySFX;

    public GameObject Trigger;

    public GameObject Laser;

    public Vector3 StartPos;

    void Start()
    {
        StartPos = transform.position;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        OrigColor = spriteRenderer.material.color;

        BossSpawnPanel.SetActive(true);
        SoundManager.instance.SFXPlay("BossSpawn", BossSpawnSFX);
        Invoke("NoShow", 1.5f);
        BossHpSlider.gameObject.SetActive(true);

        switch (BossType)
        {
            case 0: // Boss 1
                BossHP = 150;
                BossMoveType = 0;

                break;
            case 1: // Boss 2
                BossHP = 350;
                BossMoveType = 1;
                BossAttackType = 1;
                break;
            case 2: // Boss 3
                BossHP = 400;
                BossMoveType = 1;
                BossAttackType = 1;


                break;

        }

        BossHpSlider.maxValue = BossHP;
        BossHpSlider.value = BossHpSlider.maxValue;
    }


    void Update()
    {
        BossHpSlider.value = BossHP;

        switch(BossMoveType)
        {
            case 0:



                break;

            case 1:

                Vector3 Pos = StartPos;

                float Xrange = 5f;

                Pos.x = Xrange * Mathf.Sin(Time.time * 1);

                transform.position = Pos;

                break;
            case 2:

                Vector3 Pos2 = StartPos;

                float Xrange2 = 5f;

                Pos.x = Xrange2 * Mathf.Sin(Time.time * 2f);

                transform.position = Pos2;

                break;
        }

        ShotDelay += Time.deltaTime;

        switch (BossAttackType)
        {

            case 0:



                break;

            case 1:

                if(ShotDelay > ShotMax)
                {
                    Laser.SetActive(true);
                    Invoke("UnLaser", 2f);
                }


                break;
            case 2:



                break;
        }

        if (BossHP <= 0)
        {
            if (GameManager.instance.stageValue == 3 && BossType == 2)
            {
                GameManager.instance.ShowStageClearText();
                BossHpSlider.gameObject.SetActive(false);
                SoundManager.instance.SFXPlay("BossDestory", BossDestroySFX);
                GameManager.BossSpawn = false;
                Destroy(gameObject);
                print("ÀÛµ¿");
                SceneManager.LoadScene("GameEnd");

                return;
            }


            GameManager.instance.ShowStageClearText();
            BossHpSlider.gameObject.SetActive(false);
            SoundManager.instance.SFXPlay("BossDestory", BossDestroySFX);
            GameManager.BossSpawn = false;
            Destroy(gameObject);

            GameManager.instance.stageValue += 1;

            GameManager.instance.ShowStageStartText();
            GameManager.instance.enemyManagerObj.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerShot")
        {
            Instantiate(Trigger, transform.position, Quaternion.identity);
            StartCoroutine(Flash());
            BossHP -= 1;
        }
    }

    void NoShow()
    {
        BossSpawnPanel.SetActive(false);
    }

    void UnLaser()
    {
        Laser.SetActive(false);
        ShotDelay = 0;
    }

    IEnumerator Flash()
    {
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(1);
        spriteRenderer.material.color = OrigColor;
    }
}
