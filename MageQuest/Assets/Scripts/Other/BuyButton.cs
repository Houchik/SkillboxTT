using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    private Transform _itemToBuy;
    private GameObject _slotToAdd;

    private Text _coinsText;
    private int _coinsAmount;

    private Text _priceText;
    private int _price;
    private void Start()
    {
        _itemToBuy = transform.parent;
        _priceText = _itemToBuy.GetChild(0).GetComponent<Text>();
        _price = System.Convert.ToInt32(_priceText.text);
    }

    public void OnButtonClicked()
    {
        _coinsText = GameObject.Find("Coins").transform.GetChild(0).GetComponent<Text>();
        _coinsAmount = System.Convert.ToInt32(_coinsText.text);
        if (_coinsAmount >= _price) //если хватает денег
        {
            _coinsText.text = (_coinsAmount - _price).ToString(); //новое количество денег
            if (_itemToBuy.parent.name == "Equipment") //если снаряжение
            {
                string _itemToBuyWithoutLastSymbol = _itemToBuy.name.Remove(_itemToBuy.name.Length - 1);
                _slotToAdd = GameObject.Find(_itemToBuyWithoutLastSymbol + "Slot");
                Image imageToChange = _slotToAdd.transform.GetChild(0).GetComponent<Image>();
                Character character = GameObject.Find("Character").GetComponent<Character>();
                if (_itemToBuyWithoutLastSymbol == "Mantle")
                {
                    imageToChange.sprite = Resources.Load<Sprite>("Mantles/" + _itemToBuy.name); //меняем картинку мантии
                    character.ChangeMantle(Resources.Load("Mantles/" + _itemToBuy.name + "Info") as MantleInfo); //меняем статы мантии

                }

                else if (_itemToBuyWithoutLastSymbol == "Staff")
                {
                    imageToChange.sprite = Resources.Load<Sprite>("Staffs/" + _itemToBuy.name); //меняем картинку посоха
                    character.ChangeStaff(Resources.Load("Staffs/" + _itemToBuy.name + "Info") as StaffInfo); //меняем статы посоха
                }
                transform.GetChild(0).GetComponent<Text>().text = "Bought";
                GetComponent<Button>().interactable = false;
            }

            else if (_itemToBuy.parent.name == "Elements") //если элемент
            {
                GameObject[] standartSlots = GameObject.FindGameObjectsWithTag("StandartSlot");
                foreach (GameObject standartSlot in standartSlots) //ищем свободный слот
                {
                    if (standartSlot.transform.childCount == 0)
                    {
                        _slotToAdd = standartSlot;
                        Instantiate(Resources.Load("Elements/" + _itemToBuy.name), _slotToAdd.transform); //создаем новый
                        return;
                    }
                }
            }
        }
    }
}
