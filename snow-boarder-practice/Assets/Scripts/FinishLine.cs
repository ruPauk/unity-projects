using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float restartDelay = 0.5f;
    [SerializeField] ParticleSystem finishEffect;
    AudioSource finishSound;
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            finishEffect.Play();
            finishSound.Play();
            Invoke("RestartLevel", restartDelay);
            Debug.Log("You have reached the finish line!");
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
