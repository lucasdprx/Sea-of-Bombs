using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Move_Ennemi : MonoBehaviour
{
    public GameObject _ennemi;
    public List<NavMeshAgent> _agent = new();
    [HideInInspector] public Vector3 _initPos;
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
    }
    void Update()
    {
        MoveEnnemi();

        if (IsAllDead())
        {
            _wave += 1;
            PlayerPrefs.SetInt("Wave", _wave);
            PlayerPrefs.SetInt("nbGold", PlayerPrefs.GetInt("nbGold") + 5);
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

    private void MoveEnnemi()
    {
        for (int i = 0; i < _agent.Count; i++)
        {
            if (_agent[i] != null && _ennemi != null)
            {
                _agent[i].SetDestination(_ennemi.transform.position);
                if (Vector3.Distance(_ennemi.transform.position, _agent[i].transform.position) <= 1)
                {
                    Destroy(_ennemi);
                    _textHp.text = PlayerPrefs.GetInt("nbHp").ToString();
                    if (PlayerPrefs.GetInt("nbHp") <= 0)
                    {
                        PlayerPrefs.DeleteAll();
                        _uiDefeat.SetActive(true);
                        _textDefeat.text = "Vous n'avez plus de point de vie";
                        BOMB.Instance._textWave.text = "Vous etes arrivez jusqu'a la vague " + PlayerPrefs.GetInt("Wave").ToString();
                        Time.timeScale = 0.0f;
                    }
                    else
                    {
                        PlayerPrefs.SetInt("nbHp", PlayerPrefs.GetInt("nbHp") - 1);
                        SceneManager.LoadScene("Shop");
                    } 
                }
                if (BOMB.Instance._explosion)
                {
                    if (Vector3.Distance(_posBomb, _agent[i].transform.position) <= 2.5)
                    {
                        Destroy(_agent[i].gameObject);
                        Destroy(MovePlayer.Instance._ennemi[i]);
                        continue;
                    }
                }
            }
        }
        BOMB.Instance._explosion = false;
    }
}
