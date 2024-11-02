using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;
using TMPro;

public class itemButton : MonoBehaviour, IPointerClickHandler
{
    public item item;
    public Image icon;
    private BagManager bagManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        bagManager.SelectItem(item);
    }
    public void Setup(item newItem, BagManager manager)
    {
        item = newItem;
        icon.sprite = item.itemSprite;
        bagManager = manager;
    }


}
