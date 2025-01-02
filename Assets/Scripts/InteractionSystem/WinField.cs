using TMPro;
using UnityEngine;

public class WinField: MonoBehaviour {
    [Tooltip("提示文本")]
    public TMP_Text text;

    [Tooltip("可开门文本")]
    public string canOpen = "Press M To Win The Game";

    [Tooltip("不可开门文本")]
    public string needKey;

    [Tooltip("Canvas组件")]
    public Canvas canvas;

    [Tooltip("获胜UI")]
    public GameObject winUI;
    // Start is called before the first frame update
    void Start() {
        canvas.gameObject.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M) && GameManager.Instance.GetKeyNum() == 3)
        {
            winUI.GetComponent<WinUI>().ShowWin();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Player")
            return;
        if (GameManager.Instance.GetKeyNum() != 3) {
            needKey = "你需要3把钥匙来开门，而目前仅拥有 " + GameManager.Instance.GetKeyNum() + "把";
            text.SetText(needKey);
        } else {
            text.SetText(canOpen);
        }
        canvas.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag != "Player")
        {
            return;
        }
        canvas.gameObject.SetActive(false);
    }
}
