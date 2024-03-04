using UnityEngine;

public class CardDataReader : MonoBehaviour
{

    [SerializeField] private CardData _statsCard;

    public int _cardBomb;
    public int _cardHp;
    public string _cardDescription;
    public Sprite _cardImage;
    public Sprite _cardImageDos;
    public string _cardName;
    public int _cardPrestige;

    void Awake()
    {
        _cardBomb = _statsCard._bomb;
        _cardHp = _statsCard._hp;
        _cardDescription = _statsCard._description;
        _cardImage = _statsCard._image;
        _cardImageDos = _statsCard._imageDos;
        _cardName = _statsCard._name;
        _cardPrestige = _statsCard._prestige;
    }
}