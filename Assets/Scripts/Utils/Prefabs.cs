using UnityEngine;

public class Prefabs: MonoBehaviour {
    public PrefabGenerator Generator;
    public static Prefabs Instance;
    // Start is called before the first frame update

    private void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update() {

    }
}
