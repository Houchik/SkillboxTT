using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropToTheSlot : MonoBehaviour, IDropHandler
{
    private int _maxPossibleMerge = 2;

    public void OnDrop(PointerEventData eventData)
    {
        Transform elementTransform = eventData.pointerDrag.transform; //�������� transform �������� ���������� � ����
        if(transform.childCount == 1) //���� ���� �������� ������, �� ������ ������� ��� ����������
        {
            Transform elementInTheSlotTransform = transform.GetChild(0); //�������� transform �������� ������������ � �����
            string elementInTheSlotTag = elementInTheSlotTransform.tag;
            if (elementTransform.CompareTag(elementInTheSlotTag) == false) //���� ���� �� ���������, �� ������ �������
            {
                //������ �� �������
                elementInTheSlotTransform.SetParent(elementTransform.parent);
                elementInTheSlotTransform.localPosition = Vector3.zero;
            }

            else
            {
                int previousElementLastIndex = System.Convert.ToInt32(elementInTheSlotTag.Substring(elementInTheSlotTag.Length - 1));
                elementInTheSlotTag = elementInTheSlotTag.Remove(elementInTheSlotTag.Length - 1); //������� ��������� ������
                if (previousElementLastIndex < _maxPossibleMerge && elementTransform != elementInTheSlotTransform) //���� ������� �� ����������� ���������� ����������� � �� ������ � ��� �� ����
                {
                    string newElementLastIndex = System.Convert.ToString(previousElementLastIndex + 1); //������� ����� ��������� ������
                    string newElementTag = "Elements/" + elementInTheSlotTag + newElementLastIndex; //�.�. � ���� ������ ���� ��������� ������, �� ��� ������ �������� ����� ����� ��
                    GameObject newElementTransform = Resources.Load(newElementTag) as GameObject;

                    //�������� ������ � �������� ������ ��������
                    Destroy(elementTransform.gameObject);
                    Destroy(elementInTheSlotTransform.gameObject);
                    elementTransform = Instantiate(newElementTransform).transform;
                }

                else //���� ������� ����������� ���������� ����������� ��� ������ � ��� �� ����
                {
                    return;
                }
            }
        }
        //��������� ������� ������ �����
        elementTransform.SetParent(transform);
        elementTransform.localPosition = Vector3.zero;
        elementTransform.localScale = Vector3.one;
    }
}
