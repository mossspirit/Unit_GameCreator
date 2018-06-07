using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class score : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        Observable.Timer(TimeSpan.FromMilliseconds(8000)).Subscribe(_ => gameObject.SetActive(true));
    }
}
