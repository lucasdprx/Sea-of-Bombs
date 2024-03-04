using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    public List<GameObject> _ennemi = new();
    public NavMeshAgent _agent;
    private bool _wait;
    public Vector3 _initPos;
    public static MovePlayer Instance;
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
        if (!_wait)
        {
            StartCoroutine(wait(1.0f));
        }
        _initPos = _agent.transform.position;
        StartCoroutine(FollowEnnemi(0.3f));
    }
    private void Update()
    {
        if (_ennemi != null)
        {
            ExplosionDamage(_agent.transform.position, .55f);
        }      
    }

    IEnumerator wait(float second)
    {
        yield return new WaitForSeconds(second);
        _wait = true;
    }
    IEnumerator FollowEnnemi(float second)
    {
        yield return new WaitForSeconds(second);
        Move();
        StartCoroutine(FollowEnnemi(0.3f));
    }
    private void Flee()
    {
        _agent.SetDestination(_initPos);
        BOMB.Instance._isFlee = true;
    }

    private void Move()
    {
        float dist = Vector3.Distance(_ennemi[0].transform.position, _agent.transform.position);
        int indice = 0;
        for (int i = 0;  i < _ennemi.Count; i++)
        {    
            if (_ennemi[i] != null)
            {
                float dist2 = Vector3.Distance(_ennemi[i].transform.position, _agent.transform.position);
                if (dist2 < dist)
                {
                    dist = dist2;
                    indice = i;   
                }
                if (Vector3.Distance(_ennemi[i].transform.position, _agent.transform.position) <= 4 && _wait && !BOMB.Instance._isFlee)
                {
                    if (BOMB.Instance._nbBomb > 0)
                        BOMB.Instance.SpawnBomb();
                    Flee();
                }
                if (!BOMB.Instance._isFlee)
                    _agent.SetDestination(_ennemi[indice].transform.position);
            }
        }          
    }

    public void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<Case>() != null)
                if (hitCollider.GetComponent<Case>()._isCrate && !BOMB.Instance._isFlee)
                {
                    BOMB.Instance.SpawnBomb();
                    Flee();
                }
        }
    }
}