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

    [Header("サウンド")]
    public AudioSource audioSource;
    public AudioClip crrectSound;
    public AudioClip incrrectSound;
    public AudioClip questionSound;

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
        audioSource.PlayOneShot(questionSound);
    }

    void OnPressOption(int index)
    {
        if (_isFinished || _isAncer) return;

        if (index == _current.correctIndex -1)
        {
            // 正解
            crrectImage.enabled = true;
            _crrectCount += 1;
            ResultManager.Answer(true);
            cracker.Stop();
            cracker.Clear();
            cracker.Play();
            audioSource.PlayOneShot(crrectSound);
        }
        else
        {
            // 不正解
            incrrectImage.enabled = true;
            ResultManager.Answer(false);
            audioSource.PlayOneShot(incrrectSound);
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
