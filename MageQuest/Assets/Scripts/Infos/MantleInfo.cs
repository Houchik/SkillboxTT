using UnityEngine;

[CreateAssetMenu(fileName = "MantleInfo", menuName = "Equipment/New MantleInfo")]
public class MantleInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private int _armor;

    public string id => this._id;
    public int armor => this._armor;
}
