using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static ButtonManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //_audioSource.Play();
        Screen.SetResolution(1920, 1080, true);
        Time.timeScale = 1.0f;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Shop");
        PlayerPrefs.SetInt("nbBomb", 10);
        PlayerPrefs.SetInt("nbGold", 5);
        PlayerPrefs.SetInt("nbHp", 3);
        PlayerPrefs.SetInt("Speed", 0);
        PlayerPrefs.SetInt("SpeedEnnemi", 0);
        PlayerPrefs.SetInt("Wave", 1);
        Time.timeScale = 1.0f;
    }
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
    public void PauseMenu()
    {
        if (!_pauseActive) 
        {
            GameObject.Find("MenuManager").transform.Find("PauseMenu").gameObject.SetActive(true);
            _pauseActive = true;
            Time.timeScale = 0.0f;
        }
        else
        {
            GameObject.Find("MenuManager").transform.Find("PauseMenu").gameObject.SetActive(false);
            _pauseActive = false;
            Time.timeScale = 1.0f;
        }
    }
    public void EndMenu()
    {
        if (!_endActive)
        {
            GameObject.Find("MenuManager").transform.Find("EndMenu").gameObject.SetActive(true);
            _endActive = true;
            Time.timeScale = 0.0f;
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
            Time.timeScale = 0.0f;
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
    public void OptionMenuGamePause()
    {
        if (!_optionActive)
        {
            GameObject.Find("MenuManager").transform.Find("Option").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("PauseMenu").gameObject.SetActive(false);
            _optionActive = true;
        }
        else
        {
            GameObject.Find("MenuManager").transform.Find("Option").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("PauseMenu").gameObject.SetActive(true);
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
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
