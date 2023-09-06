using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question;
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;

   // public string GetQuestion => question;
    public string GetQuestion() { return question; }
    public int GetCorrectAnswerIndex() { return correctAnswerIndex; }
    public string GetAnswerByIndex(int index) {return answers[index]; }
}
