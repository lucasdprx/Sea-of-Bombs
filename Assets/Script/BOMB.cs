using System.Collections;
using UnityEngine;

public class BOMB : MonoBehaviour
{
    public GameObject bombObject;
    public static BOMB Instance;
    [HideInInspector] public bool _isFlee = false;
    [HideInInspector] public GameObject bal;
    [HideInInspector] public bool _explosion;
    public ParticleSystem _particle;

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
        _nbBomb--;
        StartCoroutine(TimeBomb(2));
    }

    IEnumerator TimeBomb(int second)
    {
        yield return new WaitForSeconds(second);
        Move_Ennemi.Instance._posBomb = bal.transform.position;
        Destroy(bal);
        _isFlee = false;
        _explosion = true;
        _particle.Play();

        
    }
}
