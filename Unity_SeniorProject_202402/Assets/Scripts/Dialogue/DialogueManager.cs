using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static InteractableItem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject DialogueBox;
    public Text DialogueText;
    public Image DialogueImage;
    public float textSpeed;
    private PartOfDialogue currentPartOfDialogue;
    private Dialogues currentDialogue;
    private int currentDialogueIndex;
    public bool isDialoguing;

    public Transform OptionContainer;
    public Button OptionButtonPrefab;

    public GameObject OptionBG;

    private void Awake()
    {
        instance = this;
    }

    public void StartDialogue(Dialogues dialogue, int partIndex, InteractionOption[] options)
    {
        EndDialogue();
        currentDialogue = dialogue;
        if (currentDialogue == null)
        {
            Debug.LogError("Dialogue not found");
            return;
        }

        if (partIndex < 0 || partIndex >= currentDialogue._partOfDialogue.Length)
        {
            Debug.LogError("Invalid partIndex: " + partIndex);
            return;
        }

        currentPartOfDialogue = currentDialogue._partOfDialogue[partIndex];
        PlayerMovement.instance.canMove = false;

        isDialoguing = true;
        currentDialogueIndex = 0;
        DialogueBox.SetActive(true);
        StartCoroutine(DisplayDialogue());

        SetupOptions(options);
    }

    private IEnumerator HandleLastLine()
    {
        yield return StartCoroutine(DisplayLine());

        if (OptionContainer.childCount > 0)
        {
            ShowOptions(); 
            yield return new WaitUntil(() => OptionContainer.childCount == 0);
        }
        else
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            EndDialogue();
        }
    }

    private IEnumerator DisplayDialogue()
    {
        while (isDialoguing)
        {
            if (currentDialogueIndex < currentPartOfDialogue._dialogues._dialogueText.Length - 1)
            {
                yield return StartCoroutine(DisplayLine());
                currentDialogueIndex++;
            }
            else
            {
                yield return StartCoroutine(HandleLastLine());
            }
        }
    }

    private IEnumerator DisplayLine()
    {
        if (currentPartOfDialogue == null || currentPartOfDialogue._dialogues == null)
        {
            Debug.LogError("Current part of dialogue or dialogues is null.");
            yield break;
        }

        var dialogueTexts = currentPartOfDialogue._dialogues._dialogueText;
        var dialogueImages = currentPartOfDialogue._dialogues._images;

        if (dialogueImages != null && currentDialogueIndex < dialogueImages.Length && dialogueImages[currentDialogueIndex] != null)
        {
            DialogueImage.gameObject.SetActive(true);
            DialogueImage.sprite = dialogueImages[currentDialogueIndex];
            DialogueImage.SetNativeSize();
        }
        else
        {
            DialogueImage.gameObject.SetActive(false);
        }


        if (dialogueTexts == null || currentDialogueIndex >= dialogueTexts.Length)
        {
            Debug.LogError("Dialogue text is null or index out of range.");
            yield break;
        }

        DialogueText.text = "";
        foreach (char letter in dialogueTexts[currentDialogueIndex].ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }

    private void SetupOptions(InteractionOption[] options = null)
    {
        ClearOptions();
        if(options != null)
        {
            foreach (var option in options)
            {
                Button button = Instantiate(OptionButtonPrefab, OptionContainer);
                button.GetComponentInChildren<Text>().text = option.OptionText;
                button.onClick.AddListener(() => OnOptionSelected(option));
            }
        }
    }

    private void ShowOptions()
    {
        OptionBG.gameObject.SetActive(true);
        OptionContainer.gameObject.SetActive(true);
    }

    private void OnOptionSelected(InteractionOption option)
    {
        option.Option.Invoke();
        EndDialogue();
    }

    private void ClearOptions()
    {
        foreach (Transform child in OptionContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void EndDialogue()
    {
        currentDialogue = null;
        currentPartOfDialogue = null;
        currentDialogueIndex = 0;
        isDialoguing = false;
        DialogueText.text = "";
        ClearOptions();
        DialogueBox.SetActive(false);
        OptionBG.gameObject.SetActive(false);
        DialogueImage.gameObject.SetActive(false);
        BagManager.instance.Invoke("CheckItem", 1.5f);

        if (PlayerMovement.instance != null)
        {
            PlayerMovement.instance.canMove = true;
        }
    }
    public bool IsDialogueFinished()
    {
        return !isDialoguing;
    }
}
