using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private List<Case> cases = new List<Case>();

    private int _reduction;

    public List<Case> _borderCases;
    public List<Case> _centerCases;

    public static GridManager Instance;

    public float _speedZone;
    [SerializeField] private int _crateCount;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetBorderList();
        InvestmentElement();
        StartCoroutine(MapRoutine());
        SetPos();
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
        for (int i = 0; i < total; i++)
        {
            int _rand = Random.Range(0, _centerCases.Count);
            if (!_centerCases[_rand]._isInvincible && !_centerCases[_rand]._isCrate)
            {
                _centerCases[_rand].SetCrate();
            }
            else
                i--;
        }
    }

    public void SetPos()
    {
        for (int j = 0; j < MovePlayer.Instance._ennemi.Count; j++)
        {
            int rand2 = Random.Range(0, cases.Count);
            if (!cases[rand2]._isCrate && !cases[rand2]._isInvincible)
            {
                MovePlayer.Instance._ennemi[j].GetComponent<NavMeshAgent>().nextPosition = new Vector3(cases[rand2].transform.position.x, MovePlayer.Instance._ennemi[j].transform.position.y, cases[rand2].transform.position.z);
                print(MovePlayer.Instance._ennemi[j].transform.position);
            }
            else
                j--;
        }
    }

    public List<Case> GetGrid()
    {
        return cases;
    }
}


