using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Move_Ennemi : MonoBehaviour
{
    public GameObject _ennemi;
    public List<NavMeshAgent> _agent = new();
    [HideInInspector] public Vector3 _posBomb;
    public static Move_Ennemi Instance;
    public int _speedEnnemi;

    public int _wave = 1;

    public TextMeshProUGUI _textHp;

    public GameObject _uiDefeat;
    public TextMeshProUGUI _textDefeat;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }
    private void Start()
    {
        _speedEnnemi = PlayerPrefs.GetInt("SpeedEnnemi");
        for (int i = 0; i < _agent.Count; i++)
        {
            _agent[i].speed += _speedEnnemi;
        }
        _textHp.text = PlayerPrefs.GetInt("nbHp").ToString();

        StartCoroutine(MoveContinue());
    }
    void Update()
    {
        KillAgent();

        if (IsAllDead())
        {
            PlayerPrefs.SetInt("Wave", PlayerPrefs.GetInt("Wave") + 1);
            PlayerPrefs.SetInt("nbGold", PlayerPrefs.GetInt("nbGold") + 5);
            PlayerPrefs.SetInt("nbBomb", PlayerPrefs.GetInt("nbBomb") + 2);
            PlayerPrefs.SetFloat("SFX", MovePlayer.Instance.AudioSlider.GetComponent<SettingsMenu>().SFXSound.value);
            PlayerPrefs.SetFloat("Music", MovePlayer.Instance.AudioSlider.GetComponent<SettingsMenu>().MusicSound.value);
            SceneManager.LoadScene("Shop");
        }
    }

    public bool IsAllDead()
    {
        for (int i = 0; i < _agent.Count;i++)
        {
            if (_agent[i] != null)
                return false;
        }
        return true;
    }

    IEnumerator MoveContinue()
    {
        yield return new WaitForSeconds(0.1f);
        MoveEnnemi();
        StartCoroutine(MoveContinue());
    }
    private void MoveEnnemi()
    {
        for (int i = 0; i < _agent.Count; i++)
        {
            if (_agent[i] != null && _ennemi != null)
            {
                _agent[i].SetDestination(_ennemi.transform.position);
            }
        }
    }
    private void KillAgent()
    {
        for (int i = 0; i < _agent.Count; i++)
        {
            if (_agent[i] != null && _ennemi != null)
            {
                if (Vector3.Distance(_ennemi.transform.position, _agent[i].transform.position) <= 1)
                {
                    Destroy(_ennemi);
                    PlayerPrefs.SetInt("nbHp", PlayerPrefs.GetInt("nbHp") - 1);
                    _textHp.text = PlayerPrefs.GetInt("nbHp").ToString();
                    if (PlayerPrefs.GetInt("nbHp") <= 0)
                    {
                        _uiDefeat.SetActive(true);
                        _textDefeat.text = "You don't have anymore health point";
                        BOMB.Instance._textWave.text = "You arrived at the wave " + PlayerPrefs.GetInt("Wave").ToString();
                        Time.timeScale = 0.0f;
                    }
                    else
                    {
                        SceneManager.LoadScene("Shop");
                    }
                    PlayerPrefs.SetFloat("SFX", MovePlayer.Instance.AudioSlider.GetComponent<SettingsMenu>().SFXSound.value);
                    PlayerPrefs.SetFloat("Music", MovePlayer.Instance.AudioSlider.GetComponent<SettingsMenu>().MusicSound.value);
                }
                if (BOMB.Instance._explosion)
                {
                    if (Vector3.Distance(_posBomb, _agent[i].transform.position) <= 2.5)
                    {
                        Destroy(_agent[i].gameObject);
                        Destroy(MovePlayer.Instance._ennemi[i]);
                    }
                }
            }
        }
        BOMB.Instance._explosion = false;
    }
}
