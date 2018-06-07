using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour {

    public SerialController serial;
    private AudioSource audioSource;
    public Text name;
    public InputField input;
    public AudioClip se1;
    private float alpha=0;
    bool com;
    string comport;
    int button = 0;

    void Start()
    {
        serial = GameObject.Find("SerialManager").GetComponent<SerialController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {
        textalpha();

       if(com)button = Int32.Parse(serial.messages[0]);

        if (Input.GetKeyDown(KeyCode.Space) || button == 3){
            audioSource.PlayOneShot(se1);

            //Observable.Timer(TimeSpan.FromMilliseconds(1000)).Subscribe(_ => SceneManager.LoadScene("MainScene"));
            SceneManager.LoadScene("MainScene");
        }
	}
    public void OnClick()
    {
        Debug.Log(input.text);
        serial.Connect(input.text);
        com = true;
    }
    void textalpha() { 
        alpha += (Time.deltaTime);
        alpha = (alpha - 0.001f) % 1;
        name.color = new Color(255, 0, 0, alpha);
    }
}
