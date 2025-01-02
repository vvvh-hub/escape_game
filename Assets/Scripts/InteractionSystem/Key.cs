using UnityEngine;

public class Key: MonoBehaviour {
    AudioSource audioSource;
    bool gotten = false;
    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0));
        if (gotten && !audioSource.isPlaying) {
            ObjectPool.Instance.Push(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            GameManager.Instance.AddKey();
            audioSource.Play();
            transform.position = new Vector3(0, 0, 0);
            gotten = true;
        }
    }
}
