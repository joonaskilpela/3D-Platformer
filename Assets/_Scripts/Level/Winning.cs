using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour
{
    Text winText;
    public Image bg;
    int curLevel = 0;

    private void Awake()
    {
        winText = GetComponent<Text>();
        winText.enabled = false;
        bg.enabled = false;
        curLevel = SceneManager.GetActiveScene().buildIndex;
        winText.text = "You Win!" + "\nPress Enter/Start to" + "\nGo to Next Level!";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (winText.enabled && curLevel < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(curLevel + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) ||Input.GetKeyDown(KeyCode.Joystick1Button6)){
            if (winText.enabled && curLevel == SceneManager.sceneCountInBuildSettings - 1)
        {
                Application.Quit();
            }
        }
    }

    public void Victory()
    {
        if (curLevel == SceneManager.sceneCountInBuildSettings - 1)
        {
            winText.text = "You Beat the Game!" + "\nPress Esc/Back to" + "\nto Quit!";
        }
        bg.enabled = true;
        winText.enabled = true;
    }
}
