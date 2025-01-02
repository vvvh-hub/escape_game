using UnityEngine;

public class KillField: MonoBehaviour {
    public GameObject magic;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            magic.GetComponent<MagicHealth>().Hp = 0;
        }
    }

    private void Start() {
        magic = GameObject.Find("/Magic");
    }
}
