using UnityEngine;

[CreateAssetMenu(fileName = "MobInfo", menuName = "Mob/New MobInfo")]
public class MobInfo : ScriptableObject
{
    public enum resistances //сопротивления к элементам
    {
        None,
        Fire,
        Water,
        Earth,
        Wind
    }

    public enum weaknesses //слабости к элементам
    {
        None,
        Fire,
        Water,
        Earth,
        Wind
    }

    [SerializeField] private string _id;
    [SerializeField] private int _damage;
    [SerializeField] private int _atackInterval;
    [SerializeField] private float _health;
    [SerializeField] private resistances _resistance;
    [Range(0, 1)]
    [SerializeField] private float _resistanceValue;
    [SerializeField] private weaknesses _weakness;
    [Range(0, 1)]
    [SerializeField] private float _weaknessValue;
    [SerializeField] private GameObject _droppingItem;

    public string id => this._id;
    public int damage => this._damage;
    public int atackInterval => this._atackInterval;
    public float health => this._health;
    public resistances resistance => this._resistance;
    public float resistanceValue => this._resistanceValue;
    public weaknesses weakness => this._weakness;
    public float weaknessValue => this._weaknessValue;
    public GameObject droppingItem => this._droppingItem;
}
