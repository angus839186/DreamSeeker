using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : InteractableItem
{
    public bool opened;
    public bool TakePhoto;

    public Animator anime;
    public override void Interact()
    {
        if(!opened)
        {
            if(BagManager.instance.GetItemInBag("LivingRoomWardrobeKey"))
            {
                this.SetDialogue("two", 1);
                opened = true;
                ToggleWardrobe();
                BagManager.instance.RemoveByManager(BagManager.instance.GetItemInBag("LivingRoomWardrobeKey"));
            }
            else
            {
                this.SetDialogue("one", 0);
            }
        }
        else
        {
            ToggleWardrobe();
            if(GameManager.Instance.IsMiniGameCompleted("Password"))
            {
                if(GameManager.Instance.IsMiniGameCompleted("Piano"))
                {
                    if(!TakePhoto)
                    {
                        this.SetDialogue("three", 2);
                    }
                    else
                    {
                        this.SetDialogue("four", 3);
                    }
                }
                else
                {
                    this.SetDialogue("four", 3);
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

    public void ToggleWardrobe()
    {
        anime.SetTrigger("Toggle");
    }
}
