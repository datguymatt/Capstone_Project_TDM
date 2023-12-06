using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //UI
    public TextMeshProUGUI statusMessage;
    public GameObject settingsMenu;

    void Start()
    {
        
        DisplayLoad();
    }

    public void DisplayLoad()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("Escape"))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        //SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
