using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MUI;
using System;


public abstract class InteractableItem : MonoBehaviour
{
    [System.Serializable]
    public class InteractionOption
    {
        public string OptionText;
        public UnityEvent Option;
    }
    [System.Serializable]
    public class OptionPool
    {
        public string PoolName;
        public InteractionOption[] InteractionOptionPool;
    }
    [SerializeField]
    private OptionPool[] optionPools;
    public OptionPool[] OptionPools => optionPools;
    public OptionPool GetOptionPoolByName(string poolName)
    {
        foreach (var pool in optionPools)
        {
            if (pool.PoolName == poolName)
            {
                return pool;
            }
        }
        return null;
    }

    public abstract void Interact();

    [SerializeField]
    protected Dialogues dialogue;

    [SerializeField]
    protected int _partIndex = 0;

    [SerializeField]
    protected string _OptionPoolName = "one";
    public virtual void SetDialogueIndex(int partIndex)
    {
        _partIndex = partIndex;
    }

    public virtual void SetOptionPoolName(string OptionPoolName)
    {
        _OptionPoolName = OptionPoolName;
    }
}
