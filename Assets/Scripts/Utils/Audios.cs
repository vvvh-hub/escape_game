using UnityEngine;

public class Audios: MonoBehaviour {
    public AudioGenerator Generator;
    public static Audios Instance;

    private void Awake() {
        Instance = this;
    }
}
