using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CardEffect : MonoBehaviour
{

    public int _nbGold;
    public int _hp;
    public int _nbBomb;
    public int _speed;
    public int _speedEnnemi;
    public List<bool> _button = new();
    public static CardEffect instance;

    public TextMeshProUGUI _textGold;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _nbGold = PlayerPrefs.GetInt("nbGold");
        _textGold.text = PlayerPrefs.GetInt("nbGold").ToString();
    }
    public void Card1()
    {
        if (gameObject.GetComponent<Card>()._cardShow[0]._price <= PlayerPrefs.GetInt("nbGold") && _button[0])
        {
            AddStats(0);
            _button[0] = false;
        } 
    }
    public void Card2()
    {
        if (gameObject.GetComponent<Card>()._cardShow[1]._price <= PlayerPrefs.GetInt("nbGold") && _button[1])
        {
            AddStats(1);
            _button[1] = false;
        }
    }
    public void Card3()
    {
        if (gameObject.GetComponent<Card>()._cardShow[2]._price <= PlayerPrefs.GetInt("nbGold") && _button[2])
        {
            AddStats(2);
            _button[2] = false;
        }
    }

    private void AddStats(int nbCard)
    {
        if (gameObject.GetComponent<Card>()._cardShow[nbCard]._speed < 0)
        {
            _speedEnnemi += gameObject.GetComponent<Card>()._cardShow[nbCard]._speed;
            PlayerPrefs.SetInt("SpeedEnnemi", _speedEnnemi);
        }
        else
        {
            _nbGold -= gameObject.GetComponent<Card>()._cardShow[nbCard]._price;
            _nbBomb += gameObject.GetComponent<Card>()._cardShow[nbCard]._bomb;
            _hp += gameObject.GetComponent<Card>()._cardShow[nbCard]._hp;
            _speed += gameObject.GetComponent<Card>()._cardShow[nbCard]._speed;
            PlayerPrefs.SetInt("nbBomb", _nbBomb);
            PlayerPrefs.SetInt("nbHp", _hp);
            PlayerPrefs.SetInt("nbGold", _nbGold);
            PlayerPrefs.SetInt("Speed", _speed);
            _textGold.text = PlayerPrefs.GetInt("nbGold").ToString();
        }
    }

    public void ResetButton()
    {
        for (int i = 0; i < 3; i++)
        {
            _button[i] = true;
        }
    }
}
