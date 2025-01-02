using UnityEngine;

public class MagicRestrainMachine: MonoBehaviour {
    public MagicUIManager manager;

    private void Start() {
        manager = MagicUIManager.Instance;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<MagicSkill>().SetRestrain(true);
            manager.SetMagicGray(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<MagicSkill>().SetRestrain(true);
            manager.SetMagicGray(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<MagicSkill>().SetRestrain(false);
            manager.SetMagicGray(false);
        }
    }

    public void Destroy() {
        ObjectPool.Instance.Push(gameObject);
    }
}
