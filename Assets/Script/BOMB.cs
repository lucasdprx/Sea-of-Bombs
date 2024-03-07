using System.Collections;
using TMPro;
using UnityEngine;

public class BOMB : MonoBehaviour
{
    public GameObject bombObject;
    public static BOMB Instance;
    [HideInInspector] public bool _isFlee = false;
    [HideInInspector] public GameObject bal;
    [HideInInspector] public bool _explosion;
    public ParticleSystem _particle;
    public float _rangeBomb = 4.0f;

    public Material _material;

    public int _nbBomb;
    public TextMeshProUGUI _textBomb;
    public TextMeshProUGUI _textWave;

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
        _nbBomb = PlayerPrefs.GetInt("nbBomb");
        _textBomb.text = PlayerPrefs.GetInt("nbBomb").ToString();
    }

    private void Update()
    {
        if (bal != null)    
            _particle.transform.position = bal.transform.position;

        if (_textBomb.text == "0" && !Move_Ennemi.Instance.IsAllDead())
        {
            Move_Ennemi.Instance._uiDefeat.SetActive(true);
            Move_Ennemi.Instance._textDefeat.text = "Vous n'avez plus de bombe";
            _textWave.text = "Vous etes arrivez jusqu'a la vague " + PlayerPrefs.GetInt("Wave").ToString();
            Time.timeScale = 0.0f;
            PlayerPrefs.DeleteAll();
            _textBomb.text = "1";
        }
    }

    public void SpawnBomb()
    {
        bal = Instantiate(bombObject, gameObject.transform.position, Quaternion.identity);
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
        _nbBomb -= 1;
        MovePlayer.Instance._agent.transform.LookAt(new Vector3(0, 0, 4));
        PlayerPrefs.SetInt("nbBomb", _nbBomb);
        _textBomb.text = PlayerPrefs.GetInt("nbBomb").ToString();
        _particle.Play();
    }

    private void BreakWall()
    {
        for (int i = 0; i < GridManager.Instance._centerCases.Count; i++)
        {
            if (GridManager.Instance._centerCases[i]._isCrate && Vector3.Distance(GridManager.Instance._centerCases[i].transform.position, bal.transform.position) <= _rangeBomb)
            {
                GridManager.Instance._centerCases[i]._isCrate = false;
                GridManager.Instance._centerCases[i].transform.position -= new Vector3(0, 1, 0);
                GridManager.Instance._centerCases[i].GetComponent<MeshRenderer>().material = _material;
            }
        }
    }
}
