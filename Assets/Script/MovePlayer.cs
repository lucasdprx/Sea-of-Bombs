using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    public GameObject ennemi;
    public NavMeshAgent agent;
    private bool _wait;

    private void Start()
    {
        if (!_wait)
        {
            StartCoroutine(wait());
        }
    }
    private void Update()
    {
        agent.SetDestination(ennemi.transform.position);
        
        if (agent.velocity == Vector3.zero && _wait)
            print("Bommbeeeeeeeeeeeeee");
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        _wait = true;
    }
}