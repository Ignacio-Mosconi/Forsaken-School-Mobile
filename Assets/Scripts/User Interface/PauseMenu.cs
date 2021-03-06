﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject hudUI;
    [SerializeField] GameObject mobileControls;
    [SerializeField] GameObject firstMenuElement;
    [SerializeField] Animator pauseMenuAnimator;
    [SerializeField] AnimationClip fadeOutAnimation;
    
    static bool isPaused = false;

#if UNITY_STANDALONE
    bool wasInitialized = false;
    bool wasControllerConnected = false;
#endif

    UnityEvent onPauseToggle = new UnityEvent();

    void Update()
    {
        if (InputManager.Instance.GetPauseButton() && !LevelManager.Instance.GameOver)
        {
            if (!isPaused)
                Pause();
            else
                Resume();
        }
#if UNITY_STANDALONE
        if (isPaused && wasInitialized)
            HandleControllerConnection();
#endif
    }

#if UNITY_STANDALONE
    IEnumerator Initialize()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        wasControllerConnected = false;
        wasInitialized = true;
    }

    void HandleControllerConnection()
    {
        bool isConnected = InputManager.Instance.CheckControllerConnection();

        if (isConnected)
        {
            if (!wasControllerConnected)
            {
                GameManager.Instance.HideCursor();
                InputManager.Instance.ChangeFirstMenuItemSelected(firstMenuElement);
                wasControllerConnected = true;
            }
        }
        else
            if (wasControllerConnected)
            {
                GameManager.Instance.ShowCursor();
                InputManager.Instance.ChangeFirstMenuItemSelected(null);
                wasControllerConnected = false;
            }
    }
#endif

    void Continue()
    {
#if UNITY_STANDALONE
        GameManager.Instance.HideCursor();
#else
        mobileControls.SetActive(true);
#endif
        pauseMenuUI.SetActive(false);
        hudUI.SetActive(true);
        isPaused = false;
        onPauseToggle.Invoke();
        InputManager.Instance.ChangeFirstMenuItemSelected(null);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        hudUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        onPauseToggle.Invoke();
#if UNITY_STANDALONE
        if (InputManager.Instance.CheckControllerConnection())
            InputManager.Instance.ChangeFirstMenuItemSelected(firstMenuElement);
        else
            GameManager.Instance.ShowCursor();
#else
        mobileControls.SetActive(false);
#endif
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuAnimator.SetTrigger("Fade Out");
        Invoke("Continue", fadeOutAnimation.length);
    }

    public void LoadMenu()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        GameManager.Instance.FadeToScene(0);
    }

#if UNITY_STANDALONE
    public void OnPauseMenuUIEnable()
    {
        wasInitialized = false;
        wasControllerConnected = false;
        StartCoroutine(Initialize());
    }
#endif

    public static bool IsPaused
    {
        get { return isPaused; }
    }

    public UnityEvent OnPauseToggle
    {
        get { return onPauseToggle; }
    }
}