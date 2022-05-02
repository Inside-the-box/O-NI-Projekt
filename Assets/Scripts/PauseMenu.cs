using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public StarterAssetsInputs starterAssetsInputs;

    public GameObject pauseMenuUI;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (starterAssetsInputs.PauseMenu)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            starterAssetsInputs.PauseMenu = false;
        }
    }

    public void Resume()
    {
        Debug.Log("resume");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        starterAssetsInputs.PauseMenu = false;
        GameIsPaused = false;
    }
    void Pause()
    {
        Debug.Log("pause");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPaused = true;
    }

    public void LoadMenu()

    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("LoadMenu");
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}