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
    public Image crrectImage;
    public Image incrrectImage;
    public ParticleSystem cracker;

    private QuestionData _current;
    private int _curQuestionNum = -1;
    private bool _IsSelect = false;
    private bool _isFinished = false;
    private bool _isAncer = false;
    private float _crrectCount = 0;

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

        _IsSelect = false;
        crrectImage.enabled = false;
        incrrectImage.enabled = false;

        if (_curQuestionNum >= database.questions.Length -1)
        {
            _isFinished = true;
            LoadNextScene();
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
    }

    void OnPressOption(int index)
    {
        if (_isFinished || _isAncer) return;

        if (index == _current.correctIndex -1)
        {
            // ê≥â
            crrectImage.enabled = true;
            _crrectCount += 1;
            ResultManager.Answer(true);
            cracker.Stop();
            cracker.Clear();
            cracker.Play();
        }
        else
        {
            // ïsê≥â
            incrrectImage.enabled = true;
            ResultManager.Answer(false);
        }
        _IsSelect = true;
        _isAncer = true;
        timerScript.currentTime = 0;

        Invoke(nameof(ZeroBlur), 2f);
    }

    void Next()
    {
        if (_isAncer) return;

        if (_IsSelect)
        {
            if (!_isFinished)
            {
                LoadNextQuestion();
            }
        }
        _IsSelect = false;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    void ZeroBlur()
    {
        timerScript.mat.SetFloat("_BlurAmount", 0f);
        timerScript.questionImage.color = Color.white;
        crrectImage.enabled = false;
        incrrectImage.enabled = false;
        _isAncer = false;
    }
}
