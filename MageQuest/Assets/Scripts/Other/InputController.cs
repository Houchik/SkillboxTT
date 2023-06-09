using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class InputController : MonoBehaviour
{
    [SerializeField] private int _castReloadTime;
    
    [SerializeField] private Character _character;

    [SerializeField] private MobSpawner _mobSpawner;

    [SerializeField] private Transform _elementSlot1;
    [SerializeField] private Transform _elementSlot2;
    [SerializeField] private Transform _mobPlace;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _shop;

    private bool _isPauseActive;
    private bool _isShopActive;

    private bool _canCast1 = true;
    private bool _canCast2 = true;

    private DroppingItemInfo[] _allDroppingItemInfo;

    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    private void Start()
    {
        m_Raycaster = _canvas.GetComponent<GraphicRaycaster>();
        m_EventSystem = _canvas.GetComponent<EventSystem>();
        _allDroppingItemInfo = Resources.LoadAll<DroppingItemInfo>("DroppingItems/");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //пауза
        {
            _isPauseActive = !_isPauseActive;
            if (_isPauseActive == true)
            {
                Time.timeScale = 0;
            }

            else
            {
                Time.timeScale = 1;
            }
            _pause.SetActive(_isPauseActive);
        }

        if (_mobPlace.GetChild(0).tag != "Traider")
        {
            if (Input.GetKeyDown(KeyCode.Q) && _elementSlot1.childCount != 0 && _canCast1) //верхний слот для элемента
            {
                _character.CastElement(1);
                StartCoroutine(CastReload(1, _castReloadTime)); //перезарядка
            }

            if (Input.GetKeyDown(KeyCode.E) && _elementSlot2.childCount != 0 && _canCast2) //нижний слот для элемента
            {
                _character.CastElement(2);
                StartCoroutine(CastReload(2, _castReloadTime)); //перезарядка
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Space)) //открываем магаз
            {
                _isShopActive = !_isShopActive;
                _shop.SetActive(_isShopActive);
            }

            if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && _isShopActive == false) //спавним следующего моба
            {
                _mobSpawner.SpawnMob();
                Destroy(_mobPlace.GetChild(0).gameObject);
            }

            if (Input.GetMouseButtonDown(0) && _isShopActive == true) //клик в магазине
            {
                //считываем рейкаст в месте клика
                m_PointerEventData = new PointerEventData(m_EventSystem);
                m_PointerEventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                m_Raycaster.Raycast(m_PointerEventData, results);
                foreach (RaycastResult result in results) //проходим по каждому задетому объекту
                {
                    if (result.gameObject.transform.CompareTag("BagSlot") && result.gameObject.transform.childCount == 2) //если клик по слоту рюкзака и если слот не пустой(текст слота и предмет)
                    {
                        foreach (DroppingItemInfo droppingItemInfo in _allDroppingItemInfo) //ищем выпавший предмет
                        {
                            if (result.gameObject.transform.GetChild(0).CompareTag(droppingItemInfo.id))
                            {
                                result.gameObject.transform.parent.GetComponent<BagSlots>().SellItem(droppingItemInfo); //продаем его
                            }
                        }
                    }
                }
            }
        }
    }

    private IEnumerator CastReload(int slotNumber, int reloadTime)
    {
        if (slotNumber == 1)
        {
            _canCast1 = false;
            yield return new WaitForSeconds(reloadTime);
            _canCast1 = true;
        }
        
        else if (slotNumber == 2)
        {
            _canCast2 = false;
            yield return new WaitForSeconds(reloadTime);
            _canCast2 = true;
        }
    }
}
