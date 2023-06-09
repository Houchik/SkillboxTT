using UnityEngine;

[CreateAssetMenu(fileName = "DroppingItemInfo", menuName = "DroppingItem/New DroppingItemInfo")]
public class DroppingItemInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private int _price;

    public string id => this._id;
    public int price => this._price;
}
