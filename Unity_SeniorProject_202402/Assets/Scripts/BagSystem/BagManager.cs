using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagManager : MonoBehaviour
{
    public static BagManager instance;
    public GameObject BagUI;
    public Transform itemsParent;
    public GameObject itemButtonPrefab;
    public GameObject selectedItemNameBoard;
    public Text selectedItemName;
    public Image selectedItemImage;
    public GameObject selectedItemDescriptionBoard;
    public Text selectedItemDescription;
    public Button discardButton;

    public Sprite emptyMask;

    public List<item> items = new List<item>();
    private Dictionary<item, GameObject> itemObjectMap = new Dictionary<item, GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BagUI.SetActive(!BagUI.activeSelf);
        }
    }


    public void AddItem(item newItem, GameObject itemObjectInScene = null)
    {
        if (!items.Contains(newItem) && items.Count < 9)
        {
            items.Add(newItem);


            if (newItem.existsInScene && itemObjectInScene != null)
            {
                itemObjectMap[newItem] = itemObjectInScene;
                itemObjectInScene.SetActive(false);
            }

            if(newItem.name == "soap" || newItem.name == "Cup")
            {
                MixUpSoap();
            }

            RefreshUI();
        }
        else
        {
            Debug.Log("Bag is full or item already exists.");
        }
    }


    public void RemoveItem(item item)
    {
        if (items.Contains(item))
        {
            if (item.canRemove)
            {
                items.Remove(item);


                if (item.existsInScene && itemObjectMap.ContainsKey(item))
                {
                    itemObjectMap[item].SetActive(true);
                    itemObjectMap.Remove(item);
                }
            }
            else
            {
                Debug.Log("不能移除這個東西");
            }
        }
        RefreshUI();
    }

    public void RemoveByManager(item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            RefreshUI();
        }
        else
        {
            Debug.Log("你沒有這個道具");
        }
    }

    private void RefreshUI()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in items)
        {
            GameObject button = Instantiate(itemButtonPrefab, itemsParent);
            button.GetComponent<itemButton>().Setup(item, this);
        }
        selectedItemName.text = "";
        selectedItemImage.sprite = emptyMask;
        selectedItemDescription.text = "";
        selectedItemNameBoard.gameObject.SetActive(false);
        selectedItemDescriptionBoard.gameObject.SetActive(false);
        discardButton.gameObject.SetActive(false);
        discardButton.onClick.RemoveAllListeners();
    }

    public void SelectItem(item item)
    {
        selectedItemName.text = item.itemName;
        selectedItemImage.sprite = item.itemSprite;
        selectedItemNameBoard.gameObject.SetActive(true);
        selectedItemDescriptionBoard.gameObject.SetActive(true);
        selectedItemDescription.text = item.description;
        if(!item.canRemove)
        {
            discardButton.gameObject.SetActive(false);
        }
        else
        {
            discardButton.gameObject.SetActive(true);
            discardButton.onClick.RemoveAllListeners();
            discardButton.onClick.AddListener(() => RemoveItem(item));
        }
    }
    public item GetItemInBag(string name)
    {
        foreach (item _item in items)
        {
            if (_item.name == name)
            {
                return _item;
            }
        }
        return null;
    }

    public bool FirstPhotoMixUp()
    {
        foreach (item _item in items)
        {
            if (GetItemInBag("PhotoFragments1") &&
                GetItemInBag("PhotoFragments2") &&
                GetItemInBag("PhotoFragments3"))
            {
                RemoveByManager(GetItemInBag("PhotoFragments1"));
                RemoveByManager(GetItemInBag("PhotoFragments2"));
                RemoveByManager(GetItemInBag("PhotoFragments3"));
                ItemManager.instance.TakeItem("Photo1");
                GameManager.Instance.TriggerStory();
                return FirstPhotoMixUp();
            }
        }
        return false;
    }
    public bool SecondPhotoMixUp()
    {
        foreach (item _item in items)
        {
            if (GetItemInBag("PhotoFragments4") &&
                GetItemInBag("PhotoFragments5") &&
                GetItemInBag("PhotoFragments6"))
            {
                RemoveByManager(GetItemInBag("PhotoFragments4"));
                RemoveByManager(GetItemInBag("PhotoFragments5"));
                RemoveByManager(GetItemInBag("PhotoFragments6"));
                ItemManager.instance.TakeItem("Photo2");
                GameManager.Instance.TriggerStory();
                return SecondPhotoMixUp();
            }
        }
        return false;
    }
    public bool ThirdPhotoMixUp()
    {
        foreach (item _item in items)
        {
            if (GetItemInBag("PhotoFragments7") &&
                GetItemInBag("PhotoFragments8") &&
                GetItemInBag("PhotoFragments9"))
            {
                RemoveByManager(GetItemInBag("PhotoFragments7"));
                RemoveByManager(GetItemInBag("PhotoFragments8"));
                RemoveByManager(GetItemInBag("PhotoFragments9"));
                ItemManager.instance.TakeItem("Photo3");
                GameManager.Instance.TriggerStory();
                return ThirdPhotoMixUp();
            }
        }
        return false;
    }
    public void CheckItem()
    {
        FirstPhotoMixUp();
        SecondPhotoMixUp();
        ThirdPhotoMixUp();
    }
    public bool MixUpSoap()
    {
        foreach (item _item in items)
        {
            if(GetItemInBag("Cup") && GetItemInBag("soap"))
            {
                RemoveByManager(GetItemInBag("Cup"));
                RemoveByManager(GetItemInBag("soap"));
                ItemManager.instance.TakeItem("SoapWater");
                return MixUpSoap();
            }
        }
        return false;
    }
    public void OnDestroy()
    {
        
    }
}
