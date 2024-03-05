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
    private int _nbDeath;

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
        //_initPos = _agent.transform.position;
    }
    void Update()
    {
        MoveEnnemi();

        if (IsAllDead())
        {
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
                    print("Lose");
                    SceneManager.LoadScene("Shop");
                }
                if (BOMB.Instance._explosion)
                {
                    if (Vector3.Distance(_posBomb, _agent[i].transform.position) <= 2.5)
                    {
                        Destroy(_agent[i].gameObject);
                        _agent.RemoveAt(i);
                        MovePlayer.Instance._ennemi.RemoveAt(i);
                        continue;
                    }
                    BOMB.Instance._explosion = false;
                }
            }
        }
    }
}
