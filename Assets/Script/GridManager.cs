using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private List<Case> cases = new List<Case>();


    [SerializeField] private int _crateCount;

    private int _total;
    private int _totalBase;

    public List<Case> _borderCases;
    public List<Case> _centerCases;

    private void Start()
    {
        SetBorderList();

        _total = _crateCount;
        for (int i = 0; i <= _total; i++)
        {
            InvestmentElement();
        }
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
        int _rand = Random.Range(0, _centerCases.Count);
        if (_centerCases[_rand].IsEmpty())
        {
            if (_crateCount > 0 && !_centerCases[_rand]._isInvincible)
            {
                _centerCases[_rand].SetCrate();
                _crateCount--;
            }
            else
            {
                Debug.Log("Invincible");
            }
        }
        else
        {
            InvestmentElement();
            return;
        }
    }

    public List<Case> GetGrid()
    {
        return cases;
    }

}


