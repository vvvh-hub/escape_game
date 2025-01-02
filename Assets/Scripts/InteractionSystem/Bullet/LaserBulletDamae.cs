using UnityEngine;

public class LaserBulletDamae: MonoBehaviour {
    int damegeNum;
    // Start is called before the first frame update
    void Start() {
        damegeNum = transform.parent.GetComponent<LaserBullet>().damage;
        GetComponent<BoxCollider2D>().size = new Vector2(transform.parent.GetComponent<LaserBullet>().maxDistance, 0.2f);
        GetComponent<BoxCollider2D>().offset = new Vector2(transform.parent.GetComponent<LaserBullet>().maxDistance / 2, 0f);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.GetComponent<MagicHealth>().Damage(damegeNum);
        }
    }
}
