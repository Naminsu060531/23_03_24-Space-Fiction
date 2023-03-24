using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject RankingPanel;
    public GameObject HelpPanel, HelpPanel_Item, HelpPanel_HowtoPlay;
    public GameObject PausePanel;
    public GameObject GameEndPanel;
    public void GameStartAction()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void UnGameEnd()
    {
        GameEndPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void TitleAction()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }



    public void OnRankingPanel()
    {
        RankingPanel.SetActive(true);
        Time.timeScale = 1;
    }
    public void UnRankingPanel()
    {
        RankingPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnHelpPanel()
    {
        HelpPanel.SetActive(true);
        Time.timeScale = 1;
    }
    public void UnHelpPanel()
    {
        HelpPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnHelpPanel_Item()
    {
        HelpPanel_Item.SetActive(true);
        Time.timeScale = 1;
    }
    public void UnHelpPanel_Item()
    {
        HelpPanel_Item.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnHelpPanel_HowtoPlay()
    {
        HelpPanel_HowtoPlay.SetActive(true);
        Time.timeScale = 1;
    }
    public void UnHelpPanel_HowtoPlay()
    {
        HelpPanel_HowtoPlay.SetActive(false);
        Time.timeScale = 1;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

}
