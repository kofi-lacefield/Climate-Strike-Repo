using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroScreenScript : MonoBehaviour
{
    public GameObject IntroMainMenu;
    public GameObject PlayMenu;
    public GameObject SettingsMenu;
    private float escButton;
    private OmnisceneScript dontDestroy;

    void Start()
    {
        dontDestroy = GameObject.FindObjectOfType<OmnisceneScript>();
        IntroMainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }

    void Update()
    {
        escButton = Input.GetAxisRaw("Cancel");
        if ((escButton <= 1) && (escButton > 0))
        {
            onClickBackButton();
        }
    }

    public void onClickBackButton()
    {
        IntroMainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }

    public void onClickPlayButton()
    {
        IntroMainMenu.SetActive(false);
        PlayMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void onClickSettingsButton()
    {
        IntroMainMenu.SetActive(false);
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void onClickQuitButton()
    {
        Application.Quit();
    }

    public void onClickNewGameButton()
    {
        dontDestroy.newGame();
    }

    public void onClickContinueButton()
    {
        dontDestroy.loadGame();
    }
}
