using UnityEngine;

public class DontDestroy: MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update() {

    }
}
