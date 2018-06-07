using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class Board : MonoBehaviour {

    public InputField inputField;
    public Controller controller;

	void Start () {
        InitInputField();
        
        
    }

    public void InputLogger()
    {

        string inputValue = inputField.text;

        Debug.Log(inputValue);
        Debug.Log(controller.score);
        StreamWriter sw = new StreamWriter(@"saveData.csv", true, Encoding.GetEncoding("Shift_JIS"));
        // ヘッダー出力
        string[] s1 = {inputValue, controller.score.ToString() };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        sw.Close();

        StreamReader sr = new StreamReader(@"saveData.csv", Encoding.GetEncoding("Shift_JIS"));
        string line;
        // 行がnullじゃない間(つまり次の行がある場合は)、処理をする
        while ((line = sr.ReadLine()) != null)
        {
            // コンソールに出力
            Debug.Log(line);
        }
        // StreamReaderを閉じる
        sr.Close();
        InitInputField();
    }
    void InitInputField()
    {

        // 値をリセット
        inputField.text = "";

        // フォーカス
        inputField.ActivateInputField();
    }
}
