using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("設定")]
    public float startTime = 10f;
    public bool autoStart = true;   

    [Header("UI")]
    public Text timerText;

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

        UpdateText();
    }

    public void StartTimer()
    {
        currentTime = startTime;
        isCounting = true;
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
        Debug.Log("タイマー終了！");
        // 必要ならここで処理を書く（シーン遷移など）
    }
}
