using UnityEngine;
using UnityEngine.Events;

public class Range: MonoBehaviour {
    // Start is called before the first frame update
    [Tooltip("进入此范围执行的函数")]
    UnityAction<GameObject> enterAction;

    [Tooltip("离开范围执行的函数")]
    UnityAction<GameObject> exitAction;

    [Tooltip("需要响应的标签")]
    string activeTag;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (enterAction == null || activeTag == null)
            return;
        if (collision.tag == activeTag) {
            enterAction(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (exitAction == null || activeTag == null)
            return;
        if (collision.tag == activeTag) {
            exitAction(collision.gameObject);
        }
    }

    public void Set(UnityAction<GameObject> enterAction, UnityAction<GameObject> exitAction, string tag) {
        this.enterAction += enterAction;
        this.exitAction += exitAction;
        activeTag = tag;
    }
}
