using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move_Ennemi : MonoBehaviour
{
    public GameObject _ennemi;
    public NavMeshAgent _agent;
    public Vector3 _initPos;
    public Vector3 _posBomb;
    public static Move_Ennemi Instance;

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
        _initPos = _agent.transform.position;
    }
    void Update()
    {
        _agent.SetDestination(_ennemi.transform.position);
        if (BOMB.Instance._explosion)
        {
            if (Vector3.Distance(_posBomb, _agent.transform.position) <= 2)
                _agent.transform.position = _initPos;
            BOMB.Instance._explosion = false;
        }
    }
}
