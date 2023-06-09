using UnityEngine;

public class SphereSlot : MonoBehaviour
{
    private Character _character;

    private ElementInfo[] _allElementInfo;

    private ElementInfo _elementInElementSlotInfo;

    private Transform _elementInElementSlot;

    private void Start()
    {
        _character = GameObject.Find("Character").GetComponent<Character>();
        _allElementInfo = Resources.LoadAll<ElementInfo>("Elements/");
    }

    private void OnTransformChildrenChanged() //отслеживаем изменение дочернего объекта
    {
        if (transform.childCount != 0) //во избежание ошибки вызванной изменением родительского объекта
        {
            _elementInElementSlot = transform.GetChild(0);
            foreach (ElementInfo elementInfo in _allElementInfo) //нахождение элемента лежащего в слоте
            {
                if (_elementInElementSlot.tag == elementInfo.id)
                {
                    _elementInElementSlotInfo = elementInfo;
                }
                int lastSymbol = System.Convert.ToInt32(transform.name.Substring(transform.name.Length - 1));
                _character.ChangeElementInElementSlotStats(lastSymbol, _elementInElementSlotInfo); //меняем статы элемента в специально слоте у героя
            }
        }
    }
}