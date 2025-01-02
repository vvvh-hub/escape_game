using UnityEngine;

public class AddMagic: MonoBehaviour {
    public int addNum;
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<MagicSkill>().AddMagic(addNum);
            ObjectPool.Instance.Push(gameObject);
        }
    }
}
