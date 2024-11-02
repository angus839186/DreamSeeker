using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletSink : InteractableItem
{
    public bool TakePhoto;
    public override void Interact()
    {
        if (!BagManager.instance.GetItemInBag("DirtyPhotoFragments2"))
        {
            this.SetDialogue("one", 0);
        }
        else
        {
            if (BagManager.instance.GetItemInBag("cornmeal") &&
                BagManager.instance.GetItemInBag("GreenMag") &&
                BagManager.instance.GetItemInBag("SoapWater"))
            {
                if (TakePhoto)
                {
                    this.SetDialogue("two", 1);
                }
                else
                {
                    this.SetDialogue("three", 2);
                }
            }
            else
            {
                this.SetDialogue("two", 1);
            }
        }
        var OptionPool = GetOptionPoolByName(_OptionPoolName);
        DialogueManager.instance.StartDialogue(dialogue, _partIndex, OptionPool.InteractionOptionPool);
    }
    public void SetDialogue(string OptionName, int dialogueIndex)
    {
        this._partIndex = dialogueIndex;
        this._OptionPoolName = OptionName;
    }
    public void GetPhoto()
    {
        TakePhoto = true;
    }
}
