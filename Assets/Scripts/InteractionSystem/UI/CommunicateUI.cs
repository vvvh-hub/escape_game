using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommunicateUI: MonoBehaviour {
    [Header("UI组件")]
    public TMP_Text textLable;

    [Header("文本组件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    private bool textFinished;
    private bool isTyping;

    private List<string> textList = new List<string>();

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && index == textList.Count) {
            gameObject.SetActive(false);
            ObjectPool.Instance.Push(gameObject);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (textFinished) {
                StartCoroutine(setCommunicateUI());
            } else if (!textFinished) {
                isTyping = false;
            }
        }
    }


    private void Awake() {
        GetTextFromFile(textFile);
    }

    void GetTextFromFile(TextAsset file) {
        textList.Clear();
        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate) {
            textList.Add(line);
        }
    }

    private void OnEnable() {
        index = 0;
        textFinished = true;
        StartCoroutine(setCommunicateUI());
    }

    IEnumerator setCommunicateUI() {
        textFinished = false;
        textLable.text = "";
        int word = 0;
        while (isTyping && word < textList[index].Length - 1) {
            textLable.text += textList[index][word];
            word++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLable.text = textList[index];
        isTyping = true;
        textFinished = true;
        index++;
    }
}
