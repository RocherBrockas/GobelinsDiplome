using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    public GameObject controleUI;
    public static PauseMenu instance;

    public GameObject[] morceauMasque;

    public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton;

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
            }
            else
            {
                Pause();
                AudioManager.instance.Play("Menu OPen");
            }
        }
    }

    public void switchMenu( bool toControl)
    {
        AudioManager.instance.Play("Menu choose");
        PauseMenuUI.SetActive(!toControl);
        controleUI.SetActive(toControl);
        EventSystem.current.SetSelectedGameObject(null);
        if (toControl)
        {
            EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        } else
        {
            EventSystem.current.SetSelectedGameObject(optionsClosedButton);
        }
    }

    public void Resume()
    {
        AudioManager.instance.Play("Menu close");
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void LoadMenu()
    {
        AudioManager.instance.Play("Menu choose");
        //add confirmation
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        PauseMenuUI.SetActive(false);
        controleUI.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.instance.Play("Menu choose");
        //add confirmation 
        Application.Quit();
    }
}
