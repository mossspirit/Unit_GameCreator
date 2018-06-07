using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Controller : MonoBehaviour {

    
    public Manager mng;
    public Text countText;
    public AudioClip jmp;
    public AudioClip scr;
    private Rigidbody rb;
    private AudioSource audioSource;
    private bool jump;
    private bool jumpse;
    [NonSerialized]
    public int score = 0;
    [NonSerialized]
    public SerialController serial;
    [SerializeField]
    private float speed;

    void Start()
    {
        serial = GameObject.Find("SerialManager").GetComponent<SerialController>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        var dir = Vector3.zero;
        int btn = Int32.Parse(serial.messages[0]);
        float v = Int32.Parse(serial.messages[2]);
        float t = Int32.Parse(serial.messages[1]);

        if (dir.sqrMagnitude > 1) dir.Normalize();

        if (btn == 3 &&  !jump /*&& this.transform.position.y >= 0.5*/) {
            if(!jumpse)audioSource.PlayOneShot(jmp);
            Vector3 balljump = new Vector3(0.0f, 6f, 0.0f);
            rb.AddForce(balljump * 4);
            jump = true;
        }
        
        Vector3 force = new Vector3(v%speed, 0f, -t%speed);    // 力を設定
        rb.AddForce(force);

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Trigger")
        {
            jump = false;
            jumpse = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Score"){
            score += 10;
            countText.text = "score: " + score.ToString();
            audioSource.PlayOneShot(scr);
        }
        if(other.gameObject.tag == "GameOver"){
            mng.GameOver();
        }
    }
}
