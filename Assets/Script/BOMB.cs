using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BOMB : MonoBehaviour
{
    public GameObject bombObject;
    public static BOMB Instance;
    [HideInInspector] public bool _isFlee = false;
    [HideInInspector] public GameObject bal;
    [HideInInspector] public bool _explosion;
    public ParticleSystem _particle;

    public Material _material;

    public int _nbBomb = 3;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    private void Update()
    {
        if (bal != null)    
            _particle.transform.position = bal.transform.position;
    }

    public void SpawnBomb()
    {
        bal = Instantiate(bombObject, gameObject.transform.position, Quaternion.identity);
        _nbBomb -= 1;
        StartCoroutine(TimeBomb(2));
    }

    IEnumerator TimeBomb(int second)
    {
        yield return new WaitForSeconds(second);
        Move_Ennemi.Instance._posBomb = bal.transform.position;
        BreakWall();
        Destroy(bal);
        _isFlee = false;
        _explosion = true;
        _particle.Play();    
    }

    private void BreakWall()
    {
        for (int i = 0; i < GridManager.Instance._centerCases.Count; i++)
        {
            print(Vector3.Distance(GridManager.Instance._centerCases[i].transform.position, bal.transform.position));
            if (GridManager.Instance._centerCases[i]._isCrate && Vector3.Distance(GridManager.Instance._centerCases[i].transform.position, bal.transform.position) <= 4)
            {
                GridManager.Instance._centerCases[i]._isCrate = false;
                GridManager.Instance._centerCases[i].transform.position -= new Vector3(0, 1, 0);
                GridManager.Instance._centerCases[i].GetComponent<MeshRenderer>().material = _material;
            }
        }
    }
}
