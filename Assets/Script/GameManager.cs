using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int stageValue = 1;

    public Text StageStartText;
    public GameObject StageStartPanel;

    public Text StageClearText;
    public GameObject StageClearPanel;

    public Text BossSpawnText;
    public GameObject BossSpawnPanel;

    public GameObject enemyManagerObj;

    public Text ScoreText;
    public Text TimeText;
    public Text StageText;

    public GameObject[] Boss;

    public int BossSpanwCount;

    public static bool BossSpawn = true;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        ShowStageStartText();
        InvokeRepeating("AddTime", 0f, 1f);
    }


    void Update()
    {
        if(stageValue >= 3)
        {
            stageValue = 3;
        }

        ScoreText.text = DataManager.CurScore.ToString();
        TimeText.text = DataManager.CurTime.ToString();
        StageText.text = stageValue.ToString();


        if (BossSpanwCount >= 60 && stageValue == 1)
        {
            BossSpanwCount = 0;
            Boss[stageValue - 1].SetActive(true);
            enemyManagerObj.SetActive(false);
        }

        if (BossSpanwCount >= 60 && stageValue == 2)
        {
            BossSpanwCount = 0;
            Boss[stageValue - 1].SetActive(true);
            enemyManagerObj.SetActive(false);
        }

        if (BossSpanwCount >= 60 && stageValue == 3)
        {
            BossSpanwCount = 0;
            Boss[stageValue - 1].SetActive(true);
            enemyManagerObj.SetActive(false);
        }

    }

    void UnShowSpawn()
    {
        BossSpawnPanel.SetActive(false);
        CancelInvoke("UnShowSpawn");
    }

    void AddTime()
    {
        DataManager.CurTime += 1;
        BossSpanwCount += 1;
    }

    public void ShowStageStartText()
    {
        StageStartPanel.SetActive(true);
        StageStartText.gameObject.SetActive(true);
        StageStartText.text = "Stage  " + stageValue + "  Start";
        Invoke("UnShow", 1.5f);
    }

    public void ShowStageClearText()
    {
        StageClearPanel.SetActive(true);
        StageClearText.gameObject.SetActive(true);
        StageClearText.text = "Stage  " + stageValue + "  Clear";

        Invoke("UnShowStageClear", 1.5f);
        Invoke("ShowStageStartText", 2f);
    }

    void UnShow()
    {
        StageStartPanel.SetActive(false);
        StageStartText.gameObject.SetActive(false);
    }

    void UnShowStageClear()
    {
        StageStartPanel.SetActive(false);
        StageStartText.gameObject.SetActive(false);
    }


}
