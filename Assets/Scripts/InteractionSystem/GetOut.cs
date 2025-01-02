using TMPro;
using UnityEngine;

public class GetOut: MonoBehaviour {
    public TMP_Text text;

    // Start is called before the first frame update
    void Start() {
        text.transform.parent.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (DialogManager.dialogCompleted) {
                text.SetText("按P键出门");
            } else {
                text.SetText("请先寻找机械蜘蛛");
            }
            text.transform.parent.gameObject.SetActive(true);
            GameManager.Instance.canTransfer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            text.transform.parent.gameObject.SetActive(false);
            GameManager.Instance.canTransfer = false;
        }
    }
}
