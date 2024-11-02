using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public Image[] puzzlePieces; 
    public Sprite[] pieceSprites; 
    private int emptyIndex = 8; 

    public GameObject puzzleCanvas;

    public Dialogues _dialogues;

    public GameObject Photo4_Hint;
    public GameObject Photo6_Hint;

    void Start()
    {
        InitializePuzzle();
    }

    public void OpenPuzzleCanvas()
    {
        puzzleCanvas.SetActive(true);
        PlayerMovement.instance.canMove = false;
    }
    public void ClosePuzzleCanvas()
    {
        puzzleCanvas.SetActive(false);
        PlayerMovement.instance.canMove = true;
    }

    void InitializePuzzle()
    {

        for (int i = 0; i < pieceSprites.Length; i++)
        {
            puzzlePieces[i].sprite = pieceSprites[i];
        }


        puzzlePieces[emptyIndex].sprite = null;

        for (int i = 0; i < 100; i++) 
        {
            List<int> possibleMoves = new List<int>();

            for (int j = 0; j < puzzlePieces.Length; j++)
            {
                if (CanMove(j))
                {
                    possibleMoves.Add(j);
                }
            }

            if (possibleMoves.Count > 0)
            {
                int randomIndex = possibleMoves[Random.Range(0, possibleMoves.Count)];
                MovePiece(randomIndex);
            }
        }
    }

    public void OnPieceClicked(int index)
    {
        if (CanMove(index))
        {
            MovePiece(index);
            CheckForWin();
        }
    }

    bool CanMove(int index)
    {
        return IsAdjacent(index, emptyIndex);
    }

    void MovePiece(int index)
    {

        Image clickedPiece = puzzlePieces[index]; 
        Image emptyPiece = puzzlePieces[emptyIndex];


        Sprite tempSprite = clickedPiece.sprite; 
        clickedPiece.sprite = emptyPiece.sprite; 
        emptyPiece.sprite = tempSprite; 


        emptyIndex = index; 
    }

    bool IsAdjacent(int index1, int index2)
    {
        int row1 = index1 / 3;
        int col1 = index1 % 3;
        int row2 = index2 / 3;
        int col2 = index2 % 3;

        return (Mathf.Abs(row1 - row2) + Mathf.Abs(col1 - col2) == 1);
    }

    void CheckForWin()
    {
        for (int i = 0; i < puzzlePieces.Length - 1; i++)
        {
            if (puzzlePieces[i].sprite != pieceSprites[i]) return;
        }

        if (puzzlePieces[emptyIndex].sprite == null)
        {
            Debug.Log("«÷¹Ï§¹¦¨¡I");
            GameManager.Instance.CompleteMiniGame("Puzzle");
            DialogueManager.instance.StartDialogue(_dialogues, 0, null);
            Photo4_Hint.SetActive(true);
            Photo6_Hint.SetActive(true);
            ItemManager.instance.TakeItem("PhotoFragments5");
            Invoke("ClosePuzzleCanvas", 2f);
        }
    }
}
