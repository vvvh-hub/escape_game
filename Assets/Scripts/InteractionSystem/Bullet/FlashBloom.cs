using UnityEngine;

public class FlashBloom: Bullet {
    [Tooltip("Magic对象")]
    GameObject magic;
    // Start is called before the first frame update
    void Start() {
        magic = GameObject.Find("/Magic");
    }

    // Update is called once per frame
    void Update() {
        if (Vector2.Distance(magic.transform.position, transform.position) < maxDistance) {
            FlashUI.Instance.Flash();
            ObjectPool.Instance.Push(gameObject);
        }
    }
}
