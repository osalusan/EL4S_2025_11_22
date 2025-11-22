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

    private float currentTime;
    private bool isCounting = false;

    void Start()
    {
        currentTime = startTime;

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

        if(currentTime < blurAttenuationStartTime)
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
