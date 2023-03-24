using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Audio
    public AudioClip ShootSFX;
    public AudioClip LowHPSFX;
    public AudioClip BoomSFX;
    public AudioClip HpAddSFX;
    public AudioClip NoColSFX;
    public AudioClip NoUseSFX;

    //

    public float speed;
    public Slider HP;
    public Slider Engine;

    public Slider Skill1Slider;
    public int Skill1Use;
    public int Skill1UseMax;
    public Text Skill1UseText;
    public GameObject Skill1Icon;

    public Slider Skill2Slider;
    public int Skill2Use;
    public int Skill2UseMax;
    public Text Skill2UseText;
    public GameObject Skill2Icon;

    public float Skill1UseDelay;
    public float Skill2UseDelay;
    public float Skill1UseDelayMax;
    public float Skill2UseDelayMax;

    public GameObject Boom;

    public float ShotMax;
    public float ShotDelay;
    public GameObject Bullet;

    public GameObject HpLow;
    public GameObject HpFull;

    public SpriteRenderer spriteRenderer;
    Color OrigColor;

    Guns[] gun;

    public int PlayerLevel;

    public GameObject Trigger;

    public GameObject PlayerTurretLevel2;
    public GameObject PlayerTurretLevel3;
    public GameObject PlayerTurretLevel4;

    public GameManager gameManager;

    public GameObject PausePanel;

    public int PauseCount;

    public GameObject NoUseObj;
    public GameObject NoColObj;

    public static GameObject NoHitUI;

    private void Awake()
    {
        DataManager.CurTime = 0;
    }

    private void Start()
    {
        HP.value = HP.maxValue;
        Engine.value = Engine.maxValue;

        Skill1UseMax = 10;
        Skill2UseMax = 5;

        Skill1Use = Skill1UseMax;
        Skill2Use = Skill2UseMax;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        OrigColor = spriteRenderer.material.color;

        gun = transform.GetComponentsInChildren<Guns>();

        InvokeRepeating("MinusEngine", 0f, 3f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.tag == "EnemyShot")
        {

            StartCoroutine(Flash());
            Instantiate(Trigger, transform.position, Quaternion.identity);
        }

        if(collision.tag == "Laser")
        {
            StartCoroutine(Flash());
            Instantiate(Trigger, transform.position, Quaternion.identity);
            HP.value -= 10;
        }
    }

    private void Update()
    {

        if (GameManager.instance.stageValue == 1)
        {
            Skill1Slider.maxValue = 10;
            Skill1Slider.maxValue = 7;
        }
        if (GameManager.instance.stageValue == 2)
        {
            Skill1Slider.maxValue = 7;
            Skill1Slider.maxValue = 5;
        }
        if (GameManager.instance.stageValue == 3)
        {
            Skill1UseMax = 4;
            Skill2UseMax = 3;
        }


        if (HP.value <= 15)
        {
            HpLow.SetActive(true);
            HpFull.SetActive(false);
        }
        else
        {
            HpLow.SetActive(false);
            HpFull.SetActive(true);
        }

        //움직이기

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 Pos = transform.position;
        Vector3 NextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = Pos + NextPos;

        //제한
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -17, 17),
            Mathf.Clamp(transform.position.y, -9, 9)
            );

        //공격
        ShotDelay += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.K))
        {
            PlayerLevel++;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if(ShotDelay > ShotMax)
            {
                print("Z");

                foreach (Guns Gun in gun)
                {
                    print("a");
                    Gun.Shoot();
                    print("b");
                }

                ShotDelay = 0;
            }

        }

        //스킬
        Skill1UseDelay += Time.deltaTime;
        Skill2UseDelay += Time.deltaTime;

        Skill1UseText.text = Skill1Use.ToString();
        Skill2UseText.text = Skill2Use.ToString();

        Skill1Slider.value = Skill1UseDelay;
        Skill2Slider.value = Skill2UseDelay;

        Skill1UseDelayMax = Skill1Slider.maxValue;
        Skill2UseDelayMax = Skill2Slider.maxValue;


        if (Skill1UseDelay > Skill1UseDelayMax)
        {
            Skill1Icon.SetActive(true);        
        }
        else
        {
            Skill1Icon.SetActive(false);
        }

        if (Skill2UseDelay > Skill2UseDelayMax)
        {
            Skill2Icon.SetActive(true);
        }
        else
        {
            Skill2Icon.SetActive(false);
        }

        if(Skill1Use <= 0)
        {
            Skill1Use = 0;
            Skill1UseDelay = 0;
        }
        if (Skill2Use <= 0)
        {
            Skill2Use = 0;
            Skill2UseDelay = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(Skill1Use <= 0)
            {
                SoundManager.instance.SFXPlay("NoUseSFX", NoUseSFX);
                print("사용 횟수 초과");
                StartCoroutine(NoShowUse());
                return;
            }

            if (Skill1UseDelay > Skill1UseDelayMax)
            {
                SoundManager.instance.SFXPlay("Hpadd", HpAddSFX);
                Skill1UseDelay = 0;
                HP.value += 20;
                Skill1Use -= 1;
            } 
           
            else if(Skill1UseDelay < Skill1UseDelayMax)
            {
                SoundManager.instance.SFXPlay("NoCol", NoColSFX);
                print("쿨타임이 부족합니다.");
                StartCoroutine(NoShowCol());
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Skill2Use <= 0)
            {
                SoundManager.instance.SFXPlay("NoUseSFX", NoUseSFX);
                print("사용 횟수 초과");
                StartCoroutine(NoShowUse());
                return;
            }

            if (Skill2UseDelay > Skill2UseDelayMax)
            {
                SoundManager.instance.SFXPlay("Boom", BoomSFX);
                StartCoroutine(BoomAction());
                Skill2UseDelay = 0;
                Skill2Use -= 1;
            }

            else if (Skill2UseDelay < Skill2UseDelayMax)
            {
                SoundManager.instance.SFXPlay("NoCol", NoColSFX);
                print("쿨타임이 부족합니다.");
                StartCoroutine(NoShowCol());
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(PauseCount >= 2)
            {
                PausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
                PauseCount = 0;
            }

            PausePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
            PauseCount += 1;
        }

        if (PlayerLevel == 2)
        {
            PlayerTurretLevel2.SetActive(true);
            ShotMax = 0.08f;
        }

        if (PlayerLevel == 3)
        {
            PlayerTurretLevel2.SetActive(true);
            PlayerTurretLevel3.SetActive(true);
            ShotMax = 0.065f;
        }

        if (PlayerLevel == 4)
        {
            PlayerTurretLevel2.SetActive(true);
            PlayerTurretLevel3.SetActive(true);
            PlayerTurretLevel4.SetActive(true);
            ShotMax = 0.05f;
        }

        //Cheat
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject target in obj)
            {
                GameObject.Destroy(target);
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerLevel += 3;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {

            Skill1Use = Skill1UseMax;
            Skill2Use = Skill2UseMax;
            Skill1UseDelay = Skill1UseDelayMax;
            Skill2UseDelay = Skill2UseDelayMax;
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {

            HP.value = HP.maxValue;

        }

        if (Input.GetKeyDown(KeyCode.F5))
        {

            Engine.value = Engine.maxValue;

        }

        if (Input.GetKeyDown(KeyCode.F6))
        {

            GameManager.instance.stageValue += 1;
            
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            gameManager.BossSpanwCount += 60;
        }


        //GameOver

        if(HP.value <= 0 || Engine.value <= 0)
        {
            print("GameOver");
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator BoomAction()
    {
        Boom.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Boom.SetActive(false);
    }

    IEnumerator Flash()
    {
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(1);
        spriteRenderer.material.color = OrigColor;
    }

    void MinusEngine()
    {
        Engine.value -= 5;
    }

    IEnumerator NoShowUse()
    {
        NoUseObj.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        NoUseObj.gameObject.SetActive(false);
    }

    IEnumerator NoShowCol()
    {
        NoColObj.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        NoColObj.gameObject.SetActive(false);
    }

}
