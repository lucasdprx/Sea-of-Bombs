using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private List<Case> cases = new List<Case>();


    [SerializeField] private int _crateCount;

    private int _total;
    private int _totalBase;

    public List<Case> _borderCases;
    public List<Case> _centerCases;

    public static GridManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetBorderList();

        _total = _crateCount;
        InvestmentElement();
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
    }

    public List<Case> GetGrid()
    {
        return cases;
    }

}


