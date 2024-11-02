using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MiniGameProgress miniGameProgress;

    public bool isPlayingMiniGames;

    public int storyIndex;

    public Dialogues storyDialogues;
    public Dialogues endDialogues;


    [Header("Test Settings")]
    public bool clearProgressOnPlay = false;

    public Transform InitCameraPos;
    public Transform InitPlayerPos;
    public GameObject Player;
    public GameObject HowToPlay;
    public bool GameEnd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        if (clearProgressOnPlay)
        {
            ClearProgress();
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        Camera cam = FindObjectOfType<Camera>();
        cam.gameObject.transform.position = InitCameraPos.position;
        Player.transform.position = InitPlayerPos.position;
        HowToPlay.SetActive(true);
    }

    public void StartMiniGame(string miniGameName)
    {
        var gameData = miniGameProgress.miniGames.Find(g => g.miniGameName == miniGameName);

        if (gameData != null && gameData.isCompleted)
        {
            Debug.Log(miniGameName + " 已經完成，無需重新遊玩。");
            return;
        }

        isPlayingMiniGames = true;
        PlayerMovement.instance.canMove = false;
        Debug.Log("開始小遊戲: " + miniGameName);
    }

    public void CompleteMiniGame(string miniGameName)
    {
        var gameData = miniGameProgress.miniGames.Find(g => g.miniGameName == miniGameName);
        if (gameData != null)
        {
            gameData.isCompleted = true;
        }
        PlayerMovement.instance.canMove = true;
    }

    public bool IsMiniGameCompleted(string miniGameName)
    {
        var gameData = miniGameProgress.miniGames.Find(g => g.miniGameName == miniGameName);
        return gameData != null && gameData.isCompleted;
    }

    public void ClearProgress()
    {
        foreach (var gameData in miniGameProgress.miniGames)
        {
            gameData.isCompleted = false;
        }
    }
    public void TriggerStory()
    {
        Debug.Log($"Start Story {storyIndex}");
        DialogueManager.instance.StartDialogue(storyDialogues, storyIndex, null);
        storyIndex++;
        if(storyIndex == 3)
        {
            GameEnd = true;
            StartCoroutine(TriggerStoryEnd());
        }
    }
    IEnumerator TriggerStoryEnd()
    {
        Debug.Log("Not yet finished");
        yield return new WaitUntil(() => DialogueManager.instance.IsDialogueFinished());

        if(GameEnd)
        {
            DialogueManager.instance.StartDialogue(endDialogues, 0, null);
        }
    }
}
