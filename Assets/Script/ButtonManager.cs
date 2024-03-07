using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu, _endMenu;
    public AudioSource _audioSource;
    private bool _pauseActive;
    private bool _endActive;
    private bool _optionActive;
    private bool _isVictory;
    private bool _isDefeat;

    [SerializeField] private Animator _animator;
    private void Start()
    {
        //_audioSource.Play();
        Screen.SetResolution(1920, 1080, true);
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("nbGold", 10);
        PlayerPrefs.SetInt("nbHp", 3);
        PlayerPrefs.SetInt("nbBomb", 20);
        SceneManager.LoadScene("Shop");
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PauseMenu()
    {
        if (!_pauseActive) 
        {
            GameObject.Find("MenuManager").transform.Find("PauseMenu").gameObject.SetActive(true);
            _pauseActive = true;
        }
        else
        {
            GameObject.Find("MenuManager").transform.Find("PauseMenu").gameObject.SetActive(false);
            _pauseActive = false;
        }
    }
    public void EndMenu()
    {
        if (!_endActive)
        {
            GameObject.Find("MenuManager").transform.Find("EndMenu").gameObject.SetActive(true);
            _endActive = true;
            if (_isVictory)
            {
                GameObject.Find("MenuManager").transform.Find("VictoryTextTop").gameObject.SetActive(true);
                GameObject.Find("MenuManager").transform.Find("VictoryTextDown").gameObject.SetActive(true);
            }
            if (_isDefeat)
            {
                GameObject.Find("MenuManager").transform.Find("DefeatTextTop").gameObject.SetActive(true);
                GameObject.Find("MenuManager").transform.Find("DefeatTextDown").gameObject.SetActive(true);
            }
        }
        else
        {
            GameObject.Find("MenuManager").transform.Find("EndMenu").gameObject.SetActive(false);
            _endActive = false;
            if (!_isVictory)
            {
                GameObject.Find("MenuManager").transform.Find("VictoryTextTop").gameObject.SetActive(false);
                GameObject.Find("MenuManager").transform.Find("VictoryTextDown").gameObject.SetActive(false);
            }
            if (!_isDefeat)
            {
                GameObject.Find("MenuManager").transform.Find("DefeatTextTop").gameObject.SetActive(false);
                GameObject.Find("MenuManager").transform.Find("DefeatTextDown").gameObject.SetActive(false);
            }
        }
    }

    public void OptionMenu()
    {
        if (!_optionActive)
        {
            GameObject.Find("MenuManager").transform.Find("Option").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("CreditButton").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("PlayButton").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("QuitButton").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("OptionButton").gameObject.SetActive(false);
            _optionActive = true;
        }
        else
        {
            GameObject.Find("MenuManager").transform.Find("Option").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("CreditButton").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("PlayButton").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("QuitButton").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("OptionButton").gameObject.SetActive(true);
            _optionActive = false;
        }
    }


    public void Credit()
    {
        GameObject.Find("MenuManager").transform.Find("Credit").gameObject.SetActive(true);
        GameObject.Find("MenuManager").transform.Find("CreditButton").gameObject.SetActive(false);
        GameObject.Find("MenuManager").transform.Find("PlayButton").gameObject.SetActive(false);
        GameObject.Find("MenuManager").transform.Find("QuitButton").gameObject.SetActive(false);
        GameObject.Find("MenuManager").transform.Find("OptionButton").gameObject.SetActive(false);
    }

    public void CreditClose()
    {
        GameObject.Find("MenuManager").transform.Find("Credit").gameObject.SetActive(false);
        GameObject.Find("MenuManager").transform.Find("CreditButton").gameObject.SetActive(true);
        GameObject.Find("MenuManager").transform.Find("PlayButton").gameObject.SetActive(true);
        GameObject.Find("MenuManager").transform.Find("QuitButton").gameObject.SetActive(true);
        GameObject.Find("MenuManager").transform.Find("OptionButton").gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
