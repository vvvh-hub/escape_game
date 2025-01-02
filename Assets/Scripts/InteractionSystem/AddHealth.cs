using UnityEngine;

public class AddHealth: MonoBehaviour {
    public int addNum;
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<MagicHealth>().Heal(addNum);
            ObjectPool.Instance.Push(gameObject);
        }
    }
}
