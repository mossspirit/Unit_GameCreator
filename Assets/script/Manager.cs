using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UniRx;

public class Manager : MonoBehaviour {

    public Text timeText;
    public Text scoreText;
    public GameObject overText;
    public GameObject Player;
    public GameObject newobj;
    private float time = 60;
    private Controller controller;
    private int score;
    [NonSerialized]
    public bool over = false;
    [NonSerialized]
    public SerialController serial;
    public string com;

    void Start (){
        serial = GameObject.Find("SerialManager").GetComponent<SerialController>();
        controller = Player.GetComponent<Controller>();

        if (!PlayerPrefs.HasKey("score"))
        {
            PlayerPrefs.SetInt("score", 0);
        }
    }
	
	void Update () {

        time -= Time.deltaTime;
        timeText.text = "time:" + ((int)time).ToString();

        if (time < 0) {
            GameOver();
        }
        if (over){
            int btn = Int32.Parse(serial.messages[0]);
            
            if(btn == 2){
                SceneManager.LoadScene("Title");
            }
        }
    }

    public void GameOver()
    {
        time = 0;

        score = PlayerPrefs.GetInt("score");
        if(score < controller.score)
        {
            PlayerPrefs.SetInt("score", controller.score);
            PlayerPrefs.Save();
            newobj.SetActive(true);
        }
        scoreText.text = "your score:" + controller.score.ToString() + "\n" + "high score:" + PlayerPrefs.GetInt("score").ToString();

        if (!over)overText.SetActive(true);
        Player.GetComponent<Controller>().enabled = false;
        over = true;
    }
}
