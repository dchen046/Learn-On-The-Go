using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable()]
public struct Answer
{
    [SerializeField] private string _info;
    public string Info { get { return _info; } }

    [SerializeField] private bool _isCorrect;
    public bool IsCorrect { get { return _isCorrect; } }
}
//make it initializable and creatable object upon right click 
[CreateAssetMenu(fileName = "New Question", menuName = "Fill-In-Blank/new Question")]
//setting up question object
public class Question : ScriptableObject {
    //two types of answer choices: multi or single
    public enum AnswerType { Multi, Single }
    //question info
    [SerializeField] private String _info = String.Empty;
    public String Info { get { return _info; } }
    //answer choices
    [SerializeField] Answer[] _answers = null;
    public Answer[] Answers { get { return _answers; } }
    //answer type
    [SerializeField] private AnswerType _answerType = AnswerType.Multi;
    public AnswerType  GetAnswerType { get { return _answerType; } }
    //score information
    [SerializeField] private int _addScore = 10;
    public int AddScore { get { return _addScore; } }
    //put the selected correct answer in CorrectAnswers
    public List<int> GetCorrectAnswers ()
    {
        List<int> CorrectAnswers = new List<int>();
        for (int i = 0; i < Answers.Length; i++)
        {
            if (Answers[i].IsCorrect)
            {
                CorrectAnswers.Add(i);
            }
        }
        return CorrectAnswers;
    }
}