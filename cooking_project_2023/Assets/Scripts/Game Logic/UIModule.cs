using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIModule : MonoBehaviour, IModule
{
    [SerializeField] private TextMeshProUGUI _visitorsCounter;
    [SerializeField] private TextMeshProUGUI _satisfiedVisitorsCounter;
    [SerializeField] private TextMeshProUGUI _sadVisitorsCounter;
    [SerializeField] private Button _pauseButton;

    [Header("Results Canvas Settings")]
    [SerializeField] private Sprite _earnedStarSprite;
    [SerializeField] private Sprite _failedStarSprite;
    [SerializeField] private Canvas _resultsCanvas;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private List<Image> _stars;
    [SerializeField] private Button _resetButton;

    public Button.ButtonClickedEvent OnResetButtonClick => _resetButton.onClick;

    private bool isPaused;

    private void Start()
    {
        ModuleLocator.GetModule<VisitorsModule>().OnVisitorCounterUpdate += UpdateUI;
        isPaused = false;
        _pauseButton.onClick.AddListener(PauseGame);
        _resultsCanvas.gameObject.SetActive(false);
        ModuleLocator.GetModule<VisitorsModule>().OnVisitorsRunOut += OpenResultsScreen;
        _resetButton.onClick.AddListener(() => Debug.Log("RESTARTING")); ;
    }

    private void OpenResultsScreen(int satisfied, int visitorsCount)
    {
        switch (satisfied / (visitorsCount * 1f) * 100)
        {
            case >= 90:
                {
                    FillStars(3);
                    break;
                }
            case >= 60 and < 90:
                {
                    FillStars(2);
                    break;
                }
            case >= 30 and < 60:
                {
                    FillStars(1);
                    break;
                }
            default:
                FillStars(0);
                break;
        }
        _titleText.text = "the working day is over!";
        _resultsCanvas.gameObject.SetActive(true);
    }

    private void FillStars(int earnedStarsCount)
    {
        for (int i = 0; i < _stars.Count; i++)
        {
            if (i <= earnedStarsCount)
            {
                _stars[i].sprite = _earnedStarSprite;
            }
            else
                _stars[i].sprite = _failedStarSprite;
        }
    }

    private void PauseGame()
    {
        if (isPaused)
            Time.timeScale = 1.0f;
        else
            Time.timeScale = 0.0f;
        isPaused = !isPaused;
    }

    public void UpdateUI(int left, int satisfied, int sad)
    {
        Debug.Log("Updating UI");
        _visitorsCounter.text = left.ToString();
        _satisfiedVisitorsCounter.text = satisfied.ToString();
        _sadVisitorsCounter.text = sad.ToString();
    }

    private void OnDestroy()
    {
        _pauseButton?.onClick.RemoveListener(PauseGame);
    }
}
