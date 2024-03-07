using System.Collections.Generic;
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
    }
    void Update()
    {
        MoveEnnemi();

        if (IsAllDead())
        {
            PlayerPrefs.DeleteAll();
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
                    PlayerPrefs.DeleteAll();
                    SceneManager.LoadScene("Shop");
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
