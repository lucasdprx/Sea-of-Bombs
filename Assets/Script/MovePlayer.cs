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

    public int _hpPlayer;
    public int _nbGold;
    public int _speed;

    public GameObject AudioSlider;

    public bool _canKill = true;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AudioSlider.GetComponent<SettingsMenu>().SFXSound.value = PlayerPrefs.GetFloat("SFX");
        AudioSlider.GetComponent<SettingsMenu>().MusicSound.value = PlayerPrefs.GetFloat("Music");

        AudioManager.instance.PlaySong("Pirate");
        if (!_wait)
            StartCoroutine(wait(1.0f));
        
        _initPos = _agent.transform.position;
        StartCoroutine(FollowEnnemi(0.3f));

        _hpPlayer = PlayerPrefs.GetInt("nbHp");
        _nbGold = PlayerPrefs.GetInt("nbGold");
        _speed = PlayerPrefs.GetInt("Speed");
        _agent.speed += _speed;
    }
    private void Update()
    {
        if (!AudioManager.instance.sound[1].Value.Source.isPlaying)
        {
            AudioManager.instance.PlaySong("Pirate");
        }

        if (_ennemi != null)
            ExplosionDamage(_agent.transform.position, .55f);

        Kill();
    }

    IEnumerator wait(float second)
    {
        yield return new WaitForSeconds(second);
        _wait = true;
    }
    IEnumerator FollowEnnemi(float second)
    {
        yield return new WaitForSeconds(second);
        if (!Move_Ennemi.Instance.IsAllDead())
            Move();
        StartCoroutine(FollowEnnemi(0.3f));
    }
    private void Flee()
    {
        _agent.SetDestination(_initPos);
        _agent.transform.LookAt(_initPos);
        BOMB.Instance._isFlee = true;
    }

    private void Move()
    {
        float dist = 9999;
        int indice = 0;

        for (int i = 0;  i < _ennemi.Count; i++)
        {
            if (_ennemi[i] != null)
            {
                if (dist > Vector3.Distance(_ennemi[i].transform.position, _agent.transform.position))
                {
                    dist = Vector3.Distance(_ennemi[i].transform.position, _agent.transform.position);
                    indice = i;
                }
            }
        }
        if (!BOMB.Instance._isFlee)
            _agent.SetDestination(_ennemi[indice].transform.position);        
    }

    public void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<Case>() != null)
                if (hitCollider.GetComponent<Case>()._isCrate && !BOMB.Instance._isFlee && _agent.transform.position.x != _initPos.x && _agent.transform.position.z != _initPos.z)
                {
                    if (PlayerPrefs.GetInt("nbBomb") > 0)
                        BOMB.Instance.SpawnBomb();
                    Flee();
                }
        }
    }

    public void Kill()
    {
        if (_canKill)
        {
            for (int i = 0; i < _ennemi.Count; ++i)
            {
                if (_ennemi[i] != null)
                    if (Vector3.Distance(_ennemi[i].transform.position, _agent.transform.position) <= 4 && _wait && !BOMB.Instance._isFlee)
                    {
                        if (PlayerPrefs.GetInt("nbBomb") > 0)
                            BOMB.Instance.SpawnBomb();
                        Flee();
                    }
            }
        }
    }
}