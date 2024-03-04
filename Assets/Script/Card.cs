using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour
{
    public List<CardData> _card = new();
    public List<CardData> _cardShow = new();
    public List<TextMeshProUGUI> _text = new();

    private void Start()
    {
        ShowCard(); 
    }

    private void ShowCard()
    {
        for (int i = 0; i < 3; i++)
        {
            int _rand = Random.Range(0, _card.Count);
            _cardShow[i] = _card[_rand];
            _text[i].text = _card[_rand]._description;
        }
    }

    public void Reroll()
    {
        ShowCard();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SceneLucas");
    }
}
