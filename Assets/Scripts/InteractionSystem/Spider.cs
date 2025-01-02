using UnityEngine;

public class Spider: MonoBehaviour {
    public static int num = 0;
    public bool isDialog;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && num == 0 && !DialogManager.dialogCompleted) {
            num++;
            DialogManager.Instance.ShowDialog();
            GameManager.Instance.ChangeOrthSize(5f);
        }
    }

    public void Reset() {
        num = 0;
        isDialog = false;
    }
}
