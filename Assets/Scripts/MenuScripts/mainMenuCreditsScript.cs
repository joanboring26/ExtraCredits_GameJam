using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class mainMenuCreditsScript : MonoBehaviour
{
    [SerializeField] Button mainMenuButton;
    GameObject [] CreditsObjects;
    [SerializeField] mainMenuScript mainMenu;
    void Awake()
    {
        mainMenuButton.onClick.AddListener(OnClickMainMenu);
        CreditsObjects = GameObject.FindGameObjectsWithTag("MainMenuCreditsOnly");
    }
    // Start is called before the first frame update
    void Start()
    {
        SetMenu(false);
    }


    void OnClickMainMenu()
    {
        SetMenu(false);
        mainMenu.SetMenu(true);
    }

    public void SetMenu(bool set)
    {
        foreach (GameObject g in CreditsObjects)
            g.SetActive(set);
    }
}
