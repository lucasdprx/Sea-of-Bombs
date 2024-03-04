using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Case : MonoBehaviour
{
    [SerializeField] string _coordinatesLength;
    [SerializeField] string _coordinatesWidth;

    public Material _material;
    public Material _materialInvincible;
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
        gameObject.GetComponent<MeshRenderer>().material = _material;
        gameObject.transform.position += new Vector3(0,1,0);
        gameObject.GetComponent<NavMeshObstacle>().carving = true;
        _isCrate = true;
    }

    public void SetInvincible()
    {
        if (_isInvincible || _isCrate) 
        {
            gameObject.GetComponent<MeshRenderer>().material = _materialInvincible;
            _isInvincible = true;
            _isCrate = false;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = _materialInvincible;
            gameObject.transform.position += new Vector3(0, 1, 0);
            _isInvincible = true;
        }
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


