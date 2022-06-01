using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string starScene, continueScene;

    public GameObject continueButton;

    private void Start() {
        if (PlayerPrefs.HasKey(starScene + "_unlocked"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void StartGame(){
        SceneManager.LoadScene(starScene);

        PlayerPrefs.DeleteAll();
    }

    public void ContinueGame(){
        SceneManager.LoadScene(continueScene);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
