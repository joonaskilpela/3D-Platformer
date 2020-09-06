using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitCounter : MonoBehaviour {

    int curCount = 0;
    //public List<Collectible> fruits = new List<Collectible>();
    [HideInInspector]
    public int maxCount;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void CountFruits(int addition = 0)
    {
        curCount += addition;
        if (text == null)
            text = GetComponent<Text>();
        text.text = "Fruits: " + curCount + " / " + maxCount;
        if(curCount == maxCount)
        {
            FindObjectOfType<Winning>().Victory();
        }
    }
}
