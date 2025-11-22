using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("ê›íË")]
    public float startTime = 10f;
    public bool autoStart = true;
    public Material mat;

    public float blurAttenuationSpeed = 0.005f;
    public float blurAttenuationStartTime = 3.0f;

    [Header("UI")]
    public Text timerText;
    public Image questionImage;
    public Image needleImage;

    public float currentTime;
    private bool isCounting = false;
    private float _rotationSpeed = 0f;

    void Start()
    {
        currentTime = startTime;
        _rotationSpeed = 360f / startTime;

        if (autoStart)
            StartTimer();
    }

    void Update()
    {
        if (!isCounting) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isCounting = false;
            OnTimerEnd();
        }
        else
        {
            needleImage.rectTransform.localEulerAngles -= new Vector3(0f, 0f, _rotationSpeed * Time.deltaTime);
        }

        if (currentTime < blurAttenuationStartTime)
        {
            float blur = mat.GetFloat("_BlurAmount");
            blur -= blurAttenuationSpeed * Time.deltaTime;
            mat.SetFloat("_BlurAmount", blur);
        }

        UpdateText();
    }

    public void StartTimer()
    {
        currentTime = startTime;
        mat.SetFloat("_BlurAmount", 0.02f);
        isCounting = true;
        questionImage.color = Color.white;
        needleImage.rectTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
        UpdateText();
    }

    public void StopTimer()
    {
        isCounting = false;
    }

    void UpdateText()
    {
        if (timerText != null)
            timerText.text = currentTime.ToString("0.0");
    }

    void OnTimerEnd()
    {
        questionImage.color = Color.black;
    }
}
