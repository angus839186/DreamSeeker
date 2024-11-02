using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeItem(string itemName)
    {
        item foundItem = Resources.Load<item>("Items/" + itemName);
        if (foundItem != null && !BagManager.instance.items.Contains(foundItem))
        {
            // �p�G���~�s�b��������A�h�d�������������
            if (foundItem.existsInScene)
            {
                GameObject itemObjectInScene = FindItemInScene(itemName);
                if (itemObjectInScene != null)
                {
                    BagManager.instance.AddItem(foundItem, itemObjectInScene);
                }
                else
                {
                    Debug.LogWarning($"No GameObject found in the scene for {itemName}");
                }
            }
            else
            {
                // ���~���b�������A�����[�J�I�]
                BagManager.instance.AddItem(foundItem);
            }

            Debug.Log($"{itemName} added to the bag.");
        }
        else
        {
            Debug.LogWarning($"Item {itemName} not found or already exists.");
        }
    }

    private GameObject FindItemInScene(string itemName)
    {
        // �ھڪ��~�W�٬d������������� GameObject
        GameObject foundObject = GameObject.Find(itemName);
        return foundObject;
    }


}
