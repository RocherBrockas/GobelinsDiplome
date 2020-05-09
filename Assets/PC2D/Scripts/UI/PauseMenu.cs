﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    public GameObject controleUI;
    public static PauseMenu instance;

    public GameObject[] morceauMasque;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            foreach( GameObject g in morceauMasque)
            {
                g.SetActive(false);
            }
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
        // Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void switchMenu( bool toControl)
    {
        PauseMenuUI.SetActive(!toControl);
        controleUI.SetActive(toControl);
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        controleUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        //add confirmation
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        PauseMenuUI.SetActive(false);
        controleUI.SetActive(false);
    }

    public void QuitGame()
    {
        //add confirmation 
        Application.Quit();
    }
}
