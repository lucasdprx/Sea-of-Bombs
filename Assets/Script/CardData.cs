using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "My Game/CardData")]
public class CardData : ScriptableObject
{
    public int _bomb;
    public int _hp;
    public string _description;
    public Sprite _cardImage;
    public Sprite _cardImageDos;
    public string _name;
    public int _prestige;
}
