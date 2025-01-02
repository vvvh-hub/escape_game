using UnityEngine;

public class MacineInner: MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            transform.parent.GetComponent<MagicRestrainMachine>().Destroy();
        }
    }
}
