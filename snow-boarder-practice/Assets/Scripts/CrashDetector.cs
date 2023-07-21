using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float restartDelay = 0.5f;
    [SerializeField] ParticleSystem failEffect;
    [SerializeField] AudioClip failSound;
    PlayerController playerController;

    AudioSource failSoundSource;
    bool hasCrashed = false;

    private void Start()
    {
        failSoundSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && !hasCrashed)
        {
            hasCrashed = true;
            playerController.DisableController();
            failEffect.Play();
            failSoundSource.PlayOneShot(failSound);
            Invoke("RestartLevel", restartDelay);
            Debug.Log("YOU DIED");
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
