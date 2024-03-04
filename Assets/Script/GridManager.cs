using System.Collections;
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
    private int _reduction;

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
        StartCoroutine(MapRoutine());
    }

    IEnumerator MapRoutine()
    {
        yield return new WaitForSeconds(5f);
        ReductionMap();
        StartCoroutine(MapRoutine());
    }
    public void ReductionMap()
    {
        //foreach (Case _case in cases)
        //{
        //    if (_case.GetCoordinatesLength() == "A" || _case.GetCoordinatesLength() == "M" && _reduction == 1)
        //    {
        //        _case.SetInvincible();
        //        _reduction += 1;
        //    }
        //    if (_case.GetCoordinatesLength() == "B" || _case.GetCoordinatesLength() == "L" && _reduction == 2)
        //    {
        //        _case.SetInvincible();
        //        _reduction += 1;
        //    }
        //    if (_case.GetCoordinatesLength() == "C" || _case.GetCoordinatesLength() == "K" && _reduction == 3)
        //    {
        //        _case.SetInvincible();
        //        _reduction += 1;
        //    }
        //    if (_case.GetCoordinatesLength() == "D" || _case.GetCoordinatesLength() == "J" && _reduction == 4)
        //    {
        //        _case.SetInvincible();
        //        _reduction += 1;
        //    }
        //    if (_case.GetCoordinatesLength() == "E" || _case.GetCoordinatesLength() == "I" && _reduction == 5)
        //    {
        //        _case.SetInvincible();
        //        _reduction += 1;
        //    }
        //    if (_case.GetCoordinatesLength() == "F" || _case.GetCoordinatesLength() == "H" && _reduction == 6)
        //    {
        //        _case.SetInvincible();
        //        _reduction += 1;
        //    }
        //    if (_case.GetCoordinatesLength() == "G" && _reduction == 7)
        //    {
        //        _case.SetInvincible();
        //    }
        //}

        for (int i = 0; i < 9; i++)
        {
            cases[i + 9 * _reduction].SetInvincible();
            cases[cases.Count - 1 - ( i + 9 * _reduction)].SetInvincible();
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
    }

    public List<Case> GetGrid()
    {
        return cases;
    }

}


