using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour
{
    public List<CardData> _card = new();
    public List<CardData> _cardShow = new();
    public List<TextMeshProUGUI> _text = new();
    public List<TextMeshProUGUI> _textTitle = new();
    public List<TextMeshProUGUI> _textPrice = new();
    private int _prestige = 1;

    private void Start()
    {
        ShowCard(); 
    }

    private void ShowCard()
    {
        bool _skipCard = false;
        int _cardNb = 0;
        for (int i = 0; i < 4; i++)
        {
            if (_skipCard)
            {
                i -= 1;
                _skipCard = false;
            }
                
            int _rand = Random.Range(0, _card.Count);
            if (_card[_rand]._prestige > _prestige)
            {
                _skipCard = true;
            }
            else
            {
                _cardShow[_cardNb] = _card[_rand];
                _text[_cardNb].text = _card[_rand]._description;
                _textTitle[_cardNb].text = _card[_rand]._name + "  " + _card[_rand]._prestige.ToString();
                _textPrice[_cardNb].text = _card[_rand]._price.ToString();


                _cardNb += 1;
                if (_cardNb >= 3)
                    return;
            }
            
        }
    }

    public void Reroll()
    {
        if (PlayerPrefs.GetInt("nbGold") >= 2)
        {
            ShowCard();
            CardEffect.instance.ResetButton();
            CardEffect.instance._nbGold -= 2;
            PlayerPrefs.SetInt("nbGold", CardEffect.instance._nbGold);
            CardEffect.instance._textGold.text = PlayerPrefs.GetInt("nbGold").ToString();
        } 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SceneLucas");
    }

    public void Upgrade()
    {
        if (PlayerPrefs.GetInt("nbGold") >= 8)
        {
            CardEffect.instance._nbGold -= 8;
            _prestige++;
            PlayerPrefs.SetInt("nbGold", CardEffect.instance._nbGold);
            CardEffect.instance._textGold.text = PlayerPrefs.GetInt("nbGold").ToString();
        }
    }
}
