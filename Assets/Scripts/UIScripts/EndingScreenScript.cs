using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class EndingScreenScript : MonoBehaviour
{
    private GameObject [] menuObjects; 
    [SerializeField] TextMeshProUGUI peasantText;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button quitButton;

    void Awake()
    {
        mainMenuButton.onClick.AddListener(OnClickMainMenu);
        quitButton.onClick.AddListener(OnClickQuit);
        menuObjects = GameObject.FindGameObjectsWithTag("EndingScreenOnly");
    }
    void Start()
    {
        foreach(GameObject g in menuObjects)
            g.SetActive(false);
    }

    void OnClickMainMenu()
    {
        SceneManager.LoadScene("mainMenuScene");
    }

    void OnClickQuit()
    {
        Application.Quit();
    }

    public void EnableMenu()
    {
        foreach(GameObject g in menuObjects)
            g.SetActive(true);
        peasantText.SetText("And we only lost " + Peasant.peasantDeaths + " along the way!");
    }
}
