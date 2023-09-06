using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private bool _pause;
    private Button _button;

    private void Start()
    {
        _pause = false;
        _button = this.GetComponent<Button>();
        _button.onClick.AddListener(ButtonHandler);
    }

    void ButtonHandler()
    {
        if (!_pause)
            this.PauseGame();
        else
            this.UnpauseGame();
    }

    private void PauseGame()
    {
        _pause = true;
        Time.timeScale = 0;
    }

    private void UnpauseGame()
    {
        _pause = false;
        Time.timeScale = 1;
    }

}
