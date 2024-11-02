using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryManager : MonoBehaviour
{
    public Text leftPageText;
    public Text rightPageText;
    public Button nextButton;
    public Button prevButton;

    private int currentPage = 1;
    public string[] diaryPages = {
    };

    void Start()
    {
        UpdatePages();
        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PreviousPage);
        UpdateButtonInteractability();
    }

    void UpdatePages()
    {
        leftPageText.text = diaryPages[currentPage - 1];
        rightPageText.text = currentPage < diaryPages.Length ? diaryPages[currentPage] : "";
    }

    void NextPage()
    {
        if (currentPage + 2 < diaryPages.Length)
        {
            currentPage += 2;
            UpdatePages();
            UpdateButtonInteractability();
        }
    }

    void PreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage -= 2;
            UpdatePages();
            UpdateButtonInteractability();
        }
    }

    void UpdateButtonInteractability()
    {
        prevButton.interactable = currentPage > 1;
        nextButton.interactable = currentPage + 2 < diaryPages.Length;
    }
    public void Reset()
    {
        currentPage = 1;
        UpdatePages();
        UpdateButtonInteractability();
    }
}
