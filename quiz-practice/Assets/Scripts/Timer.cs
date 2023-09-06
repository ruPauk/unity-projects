using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswerQuestion = 10f;
    [SerializeField] float timeBetweenQuestions = 5f;

    public bool isAnsweringQuestion;
    public bool loadNextQuestion;

    float timerValue = 0f;
    float fillFraction = 0f;
    
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0f;
    }

    public float getFillFraction()
    {
        return fillFraction;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            getFillFraction(timeToAnswerQuestion);
            if (timerValue <= 0)
            {
                timerValue = timeBetweenQuestions;
                isAnsweringQuestion = false;
            }
        }
        else
        {
            getFillFraction(timeBetweenQuestions);
            if (timerValue <= 0)
            {
                timerValue = timeToAnswerQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }
            
        }
        //Debug.Log(isAnsweringQuestion + " : " + timerValue + " = " + fillFraction);
    }

    void ChangeTimerState(float newTime, bool isAnsweringQuestionNew)
    {
        if (timerValue <= 0)
        {
            timerValue = newTime;
            isAnsweringQuestion = isAnsweringQuestionNew;
        }
    }

    void getFillFraction(float currentTimeType)
    {
        if (timerValue > 0)
            fillFraction = timerValue / currentTimeType;
    }
}
