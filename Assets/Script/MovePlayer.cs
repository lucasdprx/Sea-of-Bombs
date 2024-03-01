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
            StartCoroutine(wait(.2f));
        }
        _initPos = _agent.transform.position;
    }
    private void Update()
    {
        if (_ennemi != null)
        {
            Move();
        }      
    }

    IEnumerator wait(float second)
    {
        yield return new WaitForSeconds(second);
        _wait = true;
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
}