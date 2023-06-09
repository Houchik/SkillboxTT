using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BagSlots : MonoBehaviour
{
    private List<Transform> _allSlots = new List<Transform>();

    private Text _slotTextText;
    private Transform _slotTextTransform;
    private int _slotItemsNumber;

    private Text _coinsAmount;
    private void Start()
    {
        foreach (Transform slot in transform)
        {
            _allSlots.Add(slot);
            _coinsAmount = GameObject.Find("Coins").transform.GetChild(0).GetComponent<Text>();
        }
    }

    public void AddItem(GameObject item) //��������� �������� � ���� ������� � ����
    {
        foreach(Transform slot in _allSlots) //�������� ����� ����� �� ������� � ���������
        {
            if (slot.childCount == 2) //���� ���� �����(����� ����� � �������)
            {
                if (slot.GetChild(0).CompareTag(item.tag)) //���� �����
                {
                    _slotTextText = slot.GetChild(1).GetComponent<Text>();
                    _slotItemsNumber = System.Convert.ToInt32(_slotTextText.text);
                    _slotTextText.text = (_slotItemsNumber + 1).ToString();
                    return;
                }
            }
        }

        foreach (Transform slot in _allSlots)
        {
            if (slot.childCount == 1) //���� ���� ������(������ ����� �����)
            {
                Instantiate(item, slot);
                _slotTextTransform = slot.GetChild(0);
                _slotTextTransform.gameObject.SetActive(true);
                _slotTextTransform.SetAsLastSibling(); //������ ���������
                return;
            }
        }
    }

    public void SellItem(DroppingItemInfo itemToSell)
    {
        foreach (Transform slot in _allSlots) //���� ����� �� ������� � ���������
        {
            if (slot.childCount == 2) //���� ���� �����(����� ����� � �������)
            {
                if (slot.GetChild(0).CompareTag(itemToSell.id))
                {
                    _slotTextText = slot.GetChild(1).GetComponent<Text>();
                    _slotItemsNumber = System.Convert.ToInt32(_slotTextText.text);
                    if(_slotItemsNumber == 1) //���� ������� ��������� �������
                    {
                        slot.GetChild(1).gameObject.SetActive(false); //������ ����� ����������
                        Destroy(slot.GetChild(0).gameObject); //������� �������
                    }
                    else
                    {
                        _slotTextText.text = (_slotItemsNumber - 1).ToString();
                    }
                    _coinsAmount.text = (System.Convert.ToInt32(_coinsAmount.text) + itemToSell.price).ToString();
                    return;
                }
            }
        }
    }
}
