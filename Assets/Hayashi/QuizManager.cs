using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public QuestionDatabase database;

    public Text questionText;
    public Button[] optionButtons;
    public Image optionImage;
    public Timer timerScript;

    private QuestionData _current;
    private int _curQuestionNum = -1;
    private bool _IsSelect = false;
    private bool _isFinished = false;

    void Start()
    {
        LoadNextQuestion();
    }
    private void Update()
    {
        if(_IsSelect)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Next();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Next();
            }
        }
    }

    void LoadNextQuestion()
    {
        if (database == null) return;

        if(_curQuestionNum >= database.questions.Length -1)
        {
            _isFinished = true;
            return;
        }
        else
        {
            _curQuestionNum++;
        }

        _current = database.questions[_curQuestionNum];

        questionText.text = _current.questionText;

        for (int i = 0; i < 4; i++)
        {
            int index = i;

            optionButtons[i].GetComponentInChildren<Text>().text =
                _current.choices[i];

            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnPressOption(index));

            optionImage.sprite = _current.image;
        }

        timerScript.StartTimer();
        _IsSelect = false;
    }

    void OnPressOption(int index)
    {
        if (_isFinished) return;

        if (index == _current.correctIndex -1)
        {
            Debug.Log("ê≥âÅI");
        }
        else
        {
            Debug.Log("ïsê≥âÅI");
        }
        _IsSelect = true;
    }

    void Next()
    {
        if (_IsSelect)
        {
            if (!_isFinished)
            {
                LoadNextQuestion();
            }
            else
            {
                LoadNextScene();
            }
        }

        _IsSelect = false;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
