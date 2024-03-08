using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public List<CardData> _card = new();
    public List<CardData> _cardShow = new();
    public List<TextMeshProUGUI> _text = new();
    public List<TextMeshProUGUI> _textTitle = new();
    public List<TextMeshProUGUI> _textPrice = new();
    private int _prestige = 1;

    public static Card instance;

    private void Awake()
    {
        instance = this;
    }

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
                _textTitle[_cardNb].text = _card[_rand]._name + " level " + _card[_rand]._prestige.ToString();
                _textPrice[_cardNb].text = _card[_rand]._price.ToString();


                _cardNb += 1;
                if (_cardNb >= 3)
                    return;
            }
            
        }
    }

    public void Reroll()
    {
        AudioManager.instance.PlaySong("Button");
        if (PlayerPrefs.GetInt("nbGold") >= 2)
        {
            ShowCard();
            CardEffect.instance.ResetButton();
            CardEffect.instance._nbGold -= 2;
            PlayerPrefs.SetInt("nbGold", CardEffect.instance._nbGold);
            CardEffect.instance._textGold.text = PlayerPrefs.GetInt("nbGold").ToString();

            for (int i = 0; i < 3 ; i++)
            {
                ColorBlock colorVar = CardEffect.instance._buttonColor[i].colors;
                colorVar.normalColor = new Color(1f, 1f, 1f, 0f);
                colorVar.highlightedColor = new Color(1f, 230f / 255f, 0f, 70f / 255f);
                CardEffect.instance._buttonColor[i].colors = colorVar;
            }
        } 
    }

    public void StartGame()
    {
        AudioManager.instance.PlaySong("Button");
        SceneManager.LoadScene("SceneLucas");
    }

    public void Upgrade()
    {
        AudioManager.instance.PlaySong("Button");
        if (PlayerPrefs.GetInt("nbGold") >= 3)
        {
            CardEffect.instance._nbGold -= 3;
            _prestige++;
            PlayerPrefs.SetInt("nbGold", CardEffect.instance._nbGold);
            CardEffect.instance._textGold.text = PlayerPrefs.GetInt("nbGold").ToString();
        }
    }
}
