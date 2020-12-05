using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    //initializations
    private Question[] questions = null;
    public Question[] Questions { get { return questions; } }
    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> FinishedQuestions = new List<int>();
    private int currentQuestion = 0;
    private IEnumerator IE_WaitTillNextRound = null;
    //Serialized Fields
    [SerializeField] GameEvents events = null;
    
    //Check if game is finished
    private bool IsFinished
    {
        get
        {
            if (FinishedQuestions.Count < Questions.Length)
                return false;
            else return true;
        }
    }
   //setting up the game for different states
    void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
    }
 
    void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;
    }

    void Awake()
    {
        events.CurrentFinalScore = 0;
    }
   
    void Start()
    {
        //set high score to 0
        PlayerPrefs.SetInt(GameUtility.SavePrefKey, 0);
        //get high score saved
        events.StartupHighscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);
        LoadQuestions();
        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);
        Display();
    }
    //update user picked answers if user makes changes
    public void UpdateAnswers(AnswerData newAnswer)
    {
        if (Questions[currentQuestion].GetAnswerType == Question.AnswerType.Single)
        {
            foreach (var answer in PickedAnswers)
            {
                if (answer != newAnswer)
                {
                    answer.Reset();
                }
            }
            PickedAnswers.Clear();
            PickedAnswers.Add(newAnswer);
        }
        else
        {
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);
            if (alreadyPicked)
            {
                PickedAnswers.Remove(newAnswer);
            }
            else
            {
                PickedAnswers.Add(newAnswer);
            }
        }
    }
    //erase all picked answer choices
    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswerData>();
    }
    //display random question on screen
    void Display()
    {
        EraseAnswers();
        var question = GetRandomQuestion();
        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        } else { Debug.LogWarning("GameEvents.UpdateQuestionUI is null."); }
    }

    public void Accept()
    {
        bool isCorrect = CheckAnswers();
        FinishedQuestions.Add(currentQuestion);
        //if answer is correct, then add score; else -5 to total score
        if (isCorrect) UpdateScore(Questions[currentQuestion].AddScore);
        else UpdateScore(-5);
        //if game is finished, set high score and go to resolution screen finish
        var type = UIManager.ResolutionScreenType.Finish;
        if (IsFinished)
        {
            SetHighscore();
        }
        else
        {
            //if answer is correct, go to the resolution screen correct
            if (isCorrect) type = UIManager.ResolutionScreenType.Correct;
            //if answer is incorrect, go to resolution screen incorrect
            else type = UIManager.ResolutionScreenType.Incorrect;
        }
        if (events.DisplayResolutionScreen != null)
        {
            events.DisplayResolutionScreen(type, Questions[currentQuestion].AddScore);
        }
        //play sound effect
        if (isCorrect) AudioManager.Instance.PlaySound("CorrectSFX");
        else AudioManager.Instance.PlaySound("IncorrectSFX");
        if (type != UIManager.ResolutionScreenType.Finish)
        {
            if (IE_WaitTillNextRound != null)
            {
                StopCoroutine(IE_WaitTillNextRound);
            }
            IE_WaitTillNextRound = WaitTillNextRound();
            StartCoroutine(IE_WaitTillNextRound);
        }
    }
    IEnumerator WaitTillNextRound()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        Display();
    }
    bool CheckAnswers()
    {
        if (!CompareAnswers())
        {
            return false;
        }
        return true;
    }

    //check answers
    bool CompareAnswers()
    {
        if (PickedAnswers.Count > 0)
        {
            List<int> correct = Questions[currentQuestion].GetCorrectAnswers();
            List<int> picked = PickedAnswers.Select(x => x.AnswerIndex).ToList();

            var f = correct.Except(picked).ToList();
            var s = picked.Except(correct).ToList();

            return !f.Any() && !s.Any();
        }
        return false;
    }

    void LoadQuestions()
    {
        Object[] objs = Resources.LoadAll("Questions", typeof(Question));
        questions = new Question[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            questions[i] = (Question)objs[i];
        }
    }

    //restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //quit the game
    public void QuitGame()
    {
        AudioManager.Instance.StopSound("GameMusic");
        SceneManager.LoadScene("TopicScene");
    }
    //set high score - update when needed
    private void SetHighscore()
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);
        if (highscore < events.CurrentFinalScore)
        {
            PlayerPrefs.SetInt(GameUtility.SavePrefKey, events.CurrentFinalScore);
        }
    }
    //update final score
    private void UpdateScore(int add)
    {
        events.CurrentFinalScore += add;

        if (events.ScoreUpdated != null)
        {
            events.ScoreUpdated();
        }
    }
    //get a random question from list
    Question GetRandomQuestion()
    {
        Debug.LogWarning("In Get Random QUestion()");
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;

        return Questions[currentQuestion];
    }
    int GetRandomQuestionIndex()
    {
        var random = 0;
        if (FinishedQuestions.Count < Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, Questions.Length);
            } while (FinishedQuestions.Contains(random) || random == currentQuestion);
        }
        return random;
    }
}