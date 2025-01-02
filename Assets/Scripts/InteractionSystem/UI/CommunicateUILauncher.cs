using UnityEngine;

public class CommunicateUILauncher: MonoBehaviour {
    public GameObject communicateUI;
    // Start is called before the first frame update
    void Start() {
        communicateUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            communicateUI.SetActive(true);
        }
    }
}
