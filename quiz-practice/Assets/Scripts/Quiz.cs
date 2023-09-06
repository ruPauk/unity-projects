using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;

    Timer timer;
    

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.getFillFraction(); 
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonsState(false);
        }
    }

    void DisplayAnswer(int index)
    {
        Image imageButton;
        Debug.Log("Current quest = " + index);

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            imageButton = answerButtons[index].GetComponent<Image>();
            imageButton.sprite = correctAnswerSprite;
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            imageButton = answerButtons[correctAnswerIndex].GetComponent<Image>();
            imageButton.sprite = correctAnswerSprite;
            string correctAnswer = currentQuestion.GetAnswerByIndex(correctAnswerIndex);
            questionText.text = "Sorry, but the correct answer was - " + correctAnswer;
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswerByIndex(i);
        }
        Debug.Log("Displaying question #" + currentQuestion);
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonsState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }
    }

    void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        Debug.Log("Random index = " + index);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
            questions.Remove(currentQuestion);
    }

    private void SetDefaultButtonSprites()
    {
        for (int i = 0;i < answerButtons.Length;i++)
        {
            answerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonsState(false);
        timer.CancelTimer();
    }

    void SetButtonsState(bool state)
    {
        for (int i = 0; i < answerButtons.Length;i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

}
