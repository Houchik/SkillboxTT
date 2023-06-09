using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mob : MonoBehaviour
{
    private Character _character;
    private MobSpawner _mobSpawner;
    private BagSlots _bagSlots;

    private MobInfo[] _allMobInfo;

    private MobInfo _activeMobInfo;

    private float _timer;
    private float _atackInterval;

    private float _currentHP;
    private Image _hp;

    private int _periodicDamageInterval = 1; //����� ���������� ������������� ����� �������, �� �� �������� ���
    private void Start()
    {
        _character = GameObject.Find("Character").GetComponent<Character>();
        _mobSpawner = GameObject.Find("MobSpawner").GetComponent<MobSpawner>();
        _bagSlots = GameObject.Find("BagSlots").GetComponent<BagSlots>();
        _hp = transform.GetChild(0).GetComponent<Image>();

        _allMobInfo = Resources.LoadAll<MobInfo>("Mobs/");
        foreach (MobInfo mobInfo in _allMobInfo) //���������� ��������� ����
        {
            if (transform.CompareTag(mobInfo.id))
            {
                _activeMobInfo = mobInfo;
                _currentHP = mobInfo.health;
                _atackInterval = mobInfo.atackInterval;
            }
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _atackInterval) //������� ���� �����
        {
            _character.TakeDamage(_activeMobInfo.damage);
            _timer = 0;
        }
    }

    public void TakeDamage(float value, string elementType) //�������� ����
    {
        if (elementType == _activeMobInfo.resistance.ToString()) //���� ������������� � ��������
        {
            value -= value * _activeMobInfo.resistanceValue;
        }

        else if (elementType == _activeMobInfo.weakness.ToString()) // ���� �������� � �������� 
        {
            value += value * _activeMobInfo.weaknessValue;
        }
        _currentHP -= value;
        _hp.fillAmount = _currentHP / _activeMobInfo.health; //��������� ��������
        if(_currentHP <= 0) //���� �����, �� �������� ��� � ������� ������
        {
            _bagSlots.AddItem(_activeMobInfo.droppingItem);
            _mobSpawner.SpawnMob();
            Destroy(gameObject);
        }
    }

    public void TakePeriodicDamage(float value, string elementType) //��������� ������ ��� � _periodicDamageInterval ������
    {
        StartCoroutine(PeriodicDamage(value, elementType));
    }

    public void TakeDeceleration(float value) //���������� ��������� ����� �������
    {
        _atackInterval += _atackInterval * (value / 100);
    }

    private IEnumerator PeriodicDamage(float value, string elementType)
    {
        yield return new WaitForSeconds(_periodicDamageInterval);
        TakeDamage(value, elementType);
        StartCoroutine(PeriodicDamage(value, elementType));
    }
}
