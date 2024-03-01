using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] string _coordinatesLength;
    [SerializeField] string _coordinatesWidth;

    public Material _material;
    public static Case Instance;
    public bool _isInvincible;
    public bool _isCrate;

    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {

    }

    public string GetCoordinatesLength()
    {
        return _coordinatesLength;
    }

    public string GetCoordinatesWidth()
    {
        return _coordinatesWidth;
    }

    public void SetCrate()
    {
        Debug.Log("Crate");
        gameObject.GetComponent<MeshRenderer>().material = _material;
        _isCrate = true;
    }

    public bool GetCrate()
    {
        return _isCrate;
    }

    public bool IsEmpty()
    {
        if (!_isCrate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


