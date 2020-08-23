using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mainMenuScript : MonoBehaviour
{
    [SerializeField] Button playButton = null;
    [SerializeField] Button creditsButton = null;
    [SerializeField] Button quitButton = null;

    GameObject [] mainMenuObjects;
    [SerializeField] mainMenuCreditsScript credits = null;
    void Awake()
    {
        playButton.onClick.AddListener(OnClickPlay);
        creditsButton.onClick.AddListener(OnClickCredits);
        quitButton.onClick.AddListener(OnClickQuit);
        mainMenuObjects = GameObject.FindGameObjectsWithTag("MainMenuOnly");
    }

    void OnClickPlay()
    {
        SceneManager.LoadScene("introCutscene");
    }

    void OnClickCredits()
    {
        SetMenu(false);
        credits.SetMenu(true);
    }

    void OnClickQuit()
    {
        Application.Quit();
    }

    public void SetMenu(bool set)
    {
        foreach(GameObject g in mainMenuObjects)
            g.SetActive(set);
    }
}
