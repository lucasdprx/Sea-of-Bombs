using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    public GameObject _ennemi;
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

            ExplosionDamage(_agent.transform.position, 10f);
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
        if (!BOMB.Instance._isFlee)
            _agent.SetDestination(_ennemi.transform.position);

        if (Vector3.Distance(_ennemi.transform.position, _agent.transform.position) <= 4 && _wait && !BOMB.Instance._isFlee)
        {
            if (BOMB.Instance._nbBomb > 0)
                BOMB.Instance.SpawnBomb();
            Flee();
        }
    }

    private void SpawnBombWall()
    {
        for (int i = 0; i < GridManager.Instance._centerCases.Count; i++)
        {
            if (GridManager.Instance._centerCases[i]._isCrate && Vector3.Distance(GridManager.Instance._centerCases[i].transform.position, _agent.transform.position) <= 1.0f)
            {
                Flee();
                BOMB.Instance.SpawnBomb();
                return;
            }
        }
    }

    public void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            print(hitCollider);
        }
    }
}