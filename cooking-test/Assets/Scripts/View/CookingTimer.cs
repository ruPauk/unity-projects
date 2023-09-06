using System.Collections;
using System.Text;
using UnityEngine;


public class CookingTimer : MonoBehaviour {
    public float timeInSeconds = 60;
    private float _remainingTime = 0f;
    private StringBuilder _stringBuilder;

    private void Awake()
    {
        _stringBuilder = new StringBuilder();
        //this.timeInSeconds = app.settingsController.settings.timer;
    }

    void Start()
    {
        //здесь надо впиливать из настроек время
        _remainingTime = timeInSeconds;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while(_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }
    }

    private void UpdateTimerText()
    {
        if (_remainingTime < 0)
            _remainingTime = 0;

        int minutes = Mathf.FloorToInt(_remainingTime / 60);
        int seconds = Mathf.FloorToInt(_remainingTime % 60);
        //USE STRING BUILDER
        string min = minutes > 9 ? minutes.ToString() : "0" + minutes;
        string sec = seconds > 9 ? seconds.ToString() : "0" + seconds;
        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = min + " : " + sec;
    }
}
