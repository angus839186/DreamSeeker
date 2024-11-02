using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomLocker : InteractableItem
{
    public GameObject StorageRoomDoor;

    public override void Interact()
    {
        if (BagManager.instance.GetItemInBag("StorageroomKey"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            BagManager.instance.RemoveByManager(BagManager.instance.GetItemInBag("StorageroomKey"));
            StorageRoomDoor.SetActive(true);
            DialogueManager.instance.StartDialogue(dialogue, 1, null);
        }
        else
        {
            DialogueManager.instance.StartDialogue(dialogue, 0, null);
        }
    }
}
