using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMainMenu;
    public GameObject pauseSettingsMenu;
    private float escButton;
    private OmnisceneScript dontDestroy;

    void Start()
    {
        dontDestroy = GameObject.FindObjectOfType<OmnisceneScript>();
        pauseMainMenu.SetActive(true);
        pauseSettingsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onPauseClickBackButton();
        }
    }

    public void onPauseClickBackButton()
    {
        if (pauseSettingsMenu.activeInHierarchy)
        {
            pauseMainMenu.SetActive(true);
            pauseSettingsMenu.SetActive(false);
        }
        else
        {
            onPauseClickResumeButton();
        }
    }

    public void onPauseClickResumeButton()
    {
        dontDestroy.resumeGame();
    }

    public void onPauseClickSettingsButton()
    {
        pauseMainMenu.SetActive(false);
        pauseSettingsMenu.SetActive(true);
    }

    public void onPauseClickQuitButton()
    {
        dontDestroy.saveGame();
        Application.Quit();
    }
}
