using UnityEngine;

[CreateAssetMenu(fileName = "ElementInfo", menuName = "Element/New ElementInfo")]
public class ElementInfo : ScriptableObject
{
    public enum propertyTypes
    {
        None,
        Burning,
        Healing,
        Deceleration
    }

    [SerializeField] private string _id;
    [SerializeField] private int _damage;
    [SerializeField] private propertyTypes _propertyType;
    [SerializeField] private int _propertyValue;

    public string id => this._id;
    public int damage => this._damage;
    public propertyTypes propertyType => this._propertyType;
    public int propertyValue => this._propertyValue;
}
