using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoomWardrobe : InteractableItem
{
    public bool TakePhoto;

    public Animator anime;
    public override void Interact()
    {
        ToggleCabinet();
        if (!GameManager.Instance.IsMiniGameCompleted("Sokobanbox"))
        {
            this.SetDialogue("one", 0);
        }
        else
        {
            if(!TakePhoto)
            {
                this.SetDialogue("two", 1);
            }
            else
            {
                this.SetDialogue("one", 0);
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
    public void ToggleCabinet()
    {
        anime.SetTrigger("Toggle");
    }
}
