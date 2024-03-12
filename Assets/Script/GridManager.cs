using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private List<Case> cases = new List<Case>();


    [SerializeField] private int _crateCount;
    [SerializeField] private int _playerSpawnCount;
    [SerializeField] private int _mobSpawnCount;

    private int _total;
    private int _totalBase;
    private int _reduction;

    public List<Case> _borderCases;
    public List<Case> _centerCases;

    public static GridManager Instance;

    public float _speedZone;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetBorderList();

        _total = _crateCount + _playerSpawnCount + _mobSpawnCount;
        InvestmentElement();
        StartCoroutine(MapRoutine());
    }

    IEnumerator MapRoutine()
    {
        yield return new WaitForSeconds(_speedZone);
        ReductionMap();
        StartCoroutine(MapRoutine());
    }
    public void ReductionMap()
    {
        for (int i = 0; i < 9; i++)
        {
            cases[i + 9 * _reduction].SetInvincible();
            cases[cases.Count - 1 - (i + 9 * _reduction)].SetInvincible();
            cases[i + 9 * _reduction].GetComponent<NavMeshObstacle>().carving = true;
            cases[cases.Count - 1 - (i + 9 * _reduction)].GetComponent<NavMeshObstacle>().carving = true;
        }
        _reduction++;
    }

    public void SetBorderList()
    {
        foreach (Case _case in cases)
        {
            if (_case.GetCoordinatesLength() == "A" || _case.GetCoordinatesLength() == "M")
            {
                _borderCases.Add(_case);
            }
            else
            {
                _centerCases.Add(_case);
            }
        }
    }


    public void InvestmentElement()
    {
        int total = _crateCount;
        bool _isBlock = false;
        for (int i = 0; i < total; i++)
        {
            if (_isBlock)
            {
                i--;
                _isBlock = false;
            }
            int _rand = Random.Range(0, _centerCases.Count);
            if (!_centerCases[_rand]._isInvincible && !_centerCases[_rand]._isCrate)
            {
                _centerCases[_rand].SetCrate();
            }
            else 
                _isBlock = true;
        }
        SetPos();
    }

    private void SetPos()
    {
        for (int j = 0; j < MovePlayer.Instance._ennemi.Count; j++)
        {
            for (int i = 0; cases.Count > i; i++)
            {
                int rand2 = Random.Range(0, cases.Count);
                if (!cases[rand2]._isCrate && !cases[rand2]._isInvincible)
                {
                    MovePlayer.Instance._ennemi[j].transform.position = cases[rand2].transform.position;
                }
            }
        }    
    }

    public List<Case> GetGrid()
    {
        return cases;
    }

}


