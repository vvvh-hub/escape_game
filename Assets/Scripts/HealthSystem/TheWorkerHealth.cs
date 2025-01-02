public class TheWorkerHealth: HealthSystem {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Hp <= 0) {
            ObjectPool.Instance.Push(gameObject);
        }
    }
}
