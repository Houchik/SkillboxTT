using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private float _maxHP;

    [SerializeField] private GameObject _characterHP;
    [SerializeField] private GameObject _lossScreen;
    [SerializeField] private GameObject _mobPlace;

    private Mob _mob;

    private StaffInfo _staffInfo;
    private MantleInfo _mantleInfo;

    private ElementInfo _elementSlot1ElementInfo;
    private ElementInfo _elementSlot2ElementInfo;
    private ElementInfo _usingSphereElementInfo;

    private float _currentHP;
    private Image _hp;
    private void Start()
    {
        _currentHP = _maxHP;
        _staffInfo = Resources.Load("Staffs/Staff0Info") as StaffInfo;
        _mantleInfo = Resources.Load("Mantles/Mantle0Info") as MantleInfo;
        _hp = _characterHP.GetComponent<Image>();
    }

    public void CastElement(int sphereSlotNumber)
    {
        if (sphereSlotNumber == 1) //верхний слот для элемента
        {
            _usingSphereElementInfo = _elementSlot1ElementInfo;
        }

        else if (sphereSlotNumber == 2) //нижний слот для элемента
        {
            _usingSphereElementInfo = _elementSlot2ElementInfo;
        }

        int sumAtack = _staffInfo.damage + _usingSphereElementInfo.damage;
        string elementIDWithoutLastSymbol = _usingSphereElementInfo.id.Remove(_usingSphereElementInfo.id.Length - 1);
        _mob = _mobPlace.transform.GetChild(0).GetComponent<Mob>();
        _mob.TakeDamage(sumAtack, elementIDWithoutLastSymbol); //наносит урон

        string propertyType = _usingSphereElementInfo.propertyType.ToString(); //особенность элемента

        if (propertyType != "None")
        {
            if (propertyType == "Burning")
            {
                _mob.TakePeriodicDamage(_usingSphereElementInfo.propertyValue, elementIDWithoutLastSymbol); //наносим периодический урон мобу
            }

            else if (propertyType == "Deceleration")
            {
                _mob.TakeDeceleration(_usingSphereElementInfo.propertyValue); //замедляем периодичность атаки моба
            }

            else if (propertyType == "Healing") //лечимся
            {
                _currentHP += _usingSphereElementInfo.propertyValue;
                if(_currentHP > _maxHP)
                {
                    _currentHP = _maxHP;
                }
                _hp.fillAmount = _currentHP / _maxHP;
            }
        }
    }

    public void ChangeElementInElementSlotStats(int elementSlotNumber, ElementInfo elementInfo) //меняем статы элемента в специальном слоте
    {
        if (elementSlotNumber == 1)
        {
            _elementSlot1ElementInfo = elementInfo;
        }

        else if (elementSlotNumber == 2)
        {
            _elementSlot2ElementInfo = elementInfo;
        }
    }

    public void TakeDamage(int value) //получаем урон
    {
        value -= _mantleInfo.armor;
        _currentHP -= value;
        _hp.fillAmount = _currentHP / _maxHP; //индикатор хп
        if(_currentHP <= 0) //проиграл
        {
            Time.timeScale = 0;
            _lossScreen.SetActive(true);
        }
    }

    public void ChangeStaff(StaffInfo staffInfo) //новые статы посоха
    {
        _staffInfo = staffInfo;
    }

    public void ChangeMantle(MantleInfo mantleInfo) // новые статы мантии
    {
        _mantleInfo = mantleInfo;
    }
}
