using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FailureScreenScript : MonoBehaviour
{
    private GameObject [] menuObjects;
    [SerializeField] Button restartButton = null;
    [SerializeField] Button quitButton = null;
    [SerializeField] Image background = null;
    void Awake()
    {
        menuObjects = GameObject.FindGameObjectsWithTag("FailureScreenOnly");
        restartButton.onClick.AddListener(OnClickRestart);
        quitButton.onClick.AddListener(OnClickQuit);
    }

    void Start()
    {
        foreach(GameObject g in menuObjects)
            g.SetActive(false);
    }

    void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnClickQuit()
    {
        Application.Quit();
    }

    public void EnableScreen()
    {
        foreach(GameObject g in menuObjects)
            g.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
