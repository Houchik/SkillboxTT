using UnityEngine;

[CreateAssetMenu(fileName = "StaffInfo", menuName = "Equipment/New StaffInfo")]
public class StaffInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private int _damage;

    public string id => this._id;
    public int damage => this._damage;
}
