using System.Collections;
using UnityEngine;

public class MagicFireBullet: Bullet {
    [Tooltip("发射位置")]
    private Vector3 origin;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy" || collision.tag == "RobotEnemy" || collision.tag == "HumanEnemy") {
            StopAllCoroutines();
            collision.GetComponent<HealthSystem>().Damage(damage);
            ObjectPool.Instance.Push(gameObject);
        }
    }

    public override void Launch() {
        origin = transform.position;
        StartCoroutine(DoLaunch());
    }

    IEnumerator DoLaunch() {
        while (Vector2.Distance(origin, transform.position) < maxDistance) {
            transform.Translate(direction * speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        origin = default;
        ObjectPool.Instance.Push(gameObject);
    }

    public override void Set(float maxDistance, int speed, Vector2 direction) {
        Direction = direction;
    }

    public void init() {
        origin = transform.position;
    }
}
