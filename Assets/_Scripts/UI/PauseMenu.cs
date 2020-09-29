using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject instructionsPanel;
    public Button resumeButton;
    public Button instructionsButton;
    public Button[] levelButtons;
    public Slider musicSlider;
    public Button quitButton;
    public Button returnButton;
    public AudioMixer mixer;

    [HideInInspector]
    public bool won = false;
    bool paused = false;

	void Start () {
        Resume();
        musicSlider.value = musicSlider.maxValue;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if(paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        paused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        instructionsPanel.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    public void Instruct()
    {
        pausePanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void TimeToQuit()
    {
        Application.Quit();
    }

    public void ReturnToPausePanel()
    {
        instructionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void GoToLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void MusicVolume(float volume)
    {
        mixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }
}
