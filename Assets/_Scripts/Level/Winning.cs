using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour {

    Text winText;
    public Image bg;
    int curLevel = 0;

    private void Awake()
    {
        winText = GetComponent<Text>();
        winText.enabled = false;
        bg.enabled = false;
        curLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && winText.enabled && curLevel < SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(curLevel + 1);
        }
        if(Input.GetKeyDown(KeyCode.Escape) && winText.enabled && curLevel == SceneManager.sceneCountInBuildSettings - 1)
        {
            Application.Quit();
        }
    }

    public void Victory()
    {
        if(curLevel == SceneManager.sceneCountInBuildSettings - 1)
        {
            winText.text = "You Beat the Game!" + "\nPress Esc to" + "\nto Quit!";
        }
        bg.enabled = true;
        winText.enabled = true;
    }
}
