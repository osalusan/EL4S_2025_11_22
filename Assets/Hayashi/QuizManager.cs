using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public QuestionDatabase database;

    public Text questionText;
    public Button[] optionButtons;
    public Image optionImage;

    private QuestionData _current;
    private int _curQuestionNum = -1;
    private bool _isFinished = false;

    void Start()
    {
        LoadNextQuestion();
    }

    void LoadNextQuestion()
    {
        if (database == null) return;

        if(_curQuestionNum >= database.questions.Length -1)
        {
            FinishQuestion();
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

        LoadNextQuestion();
    }

    void FinishQuestion()
    {
        _isFinished = true;
        Invoke(nameof(LoadNextScene), 3f);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
