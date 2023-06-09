using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropToTheSlot : MonoBehaviour, IDropHandler
{
    private int _maxPossibleMerge = 2;

    public void OnDrop(PointerEventData eventData)
    {
        Transform elementTransform = eventData.pointerDrag.transform; //получаем transform элемента брошенного в слот
        if(transform.childCount == 1) //если есть дочерний объект, то меняем местами или объединяем
        {
            Transform elementInTheSlotTransform = transform.GetChild(0); //получаем transform элемента находящегося в слоте
            string elementInTheSlotTag = elementInTheSlotTransform.tag;
            if (elementTransform.CompareTag(elementInTheSlotTag) == false) //если теги не совпадают, то меняем местами
            {
                //меняем их местами
                elementInTheSlotTransform.SetParent(elementTransform.parent);
                elementInTheSlotTransform.localPosition = Vector3.zero;
            }

            else
            {
                int previousElementLastIndex = System.Convert.ToInt32(elementInTheSlotTag.Substring(elementInTheSlotTag.Length - 1));
                elementInTheSlotTag = elementInTheSlotTag.Remove(elementInTheSlotTag.Length - 1); //удаляем последний символ
                if (previousElementLastIndex < _maxPossibleMerge && elementTransform != elementInTheSlotTransform) //если элемент не максимально возможного объединения и не брошен в тот же слот
                {
                    string newElementLastIndex = System.Convert.ToString(previousElementLastIndex + 1); //создаем новый последний символ
                    string newElementTag = "Elements/" + elementInTheSlotTag + newElementLastIndex; //т.к. в моем случае теги идентичны именам, то имя нового предмета будет таким же
                    GameObject newElementTransform = Resources.Load(newElementTag) as GameObject;

                    //создание нового и удаление старых объектов
                    Destroy(elementTransform.gameObject);
                    Destroy(elementInTheSlotTransform.gameObject);
                    elementTransform = Instantiate(newElementTransform).transform;
                }

                else //если элемент максимально возможного объединения или брошен в тот же слот
                {
                    return;
                }
            }
        }
        //помещение объекта внутрь слота
        elementTransform.SetParent(transform);
        elementTransform.localPosition = Vector3.zero;
        elementTransform.localScale = Vector3.one;
    }
}
