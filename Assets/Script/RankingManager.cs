using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingManager : MonoBehaviour
{
    public Text[] NameText, ScoreText, TimeText;
    public InputField InputName;
    public GameObject RankingPanel;

    public void Update()
    {
        if (InputName.text == null)
        {
            print("이름을 입력하십시오");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            string name = InputName.text;

            DataManager.Instance.ScoreLoad();

            DataManager.Instance.ScoreInput(name);

            SetRanking();

            //RankingPanel.SetActive(false);

            RankingPanel.SetActive(true);
        }
    }

    public void RankingUpdate()
    {
        if(InputName.text == null)
        {
            print("이름을 입력하시오");
            return;
        }

        DataManager.Instance.ScoreLoad();

        DataManager.Instance.ScoreInput(InputName.text);

        SetRanking();

        //RankingPanel.SetActive(false);

        RankingPanel.SetActive(true);
    }

    public void SetRanking()
    {
        for (int i = 0; i < NameText.Length; i++)
        {
            NameText[i].text = DataManager.ScoreList[i].Name;
            ScoreText[i].text = DataManager.ScoreList[i].Score.ToString();
            TimeText[i].text = DataManager.ScoreList[i].Time.ToString();
        }
    }
}
