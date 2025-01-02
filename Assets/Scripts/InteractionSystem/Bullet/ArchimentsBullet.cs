using UnityEngine;

public class ArchimentsBullet: Bullet {
    float theta = 0;
    float length = 0;

    [Tooltip("theta因子")]
    public float thetaFactor = 0.1f;

    [Tooltip("length因子")]
    public float lengthFactor = 10f;

    private Vector3 originPos;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Vector2.Distance(originPos, transform.position) < maxDistance) {
            theta += thetaFactor * Time.deltaTime;
            length = theta * lengthFactor;
            var x = length * Mathf.Cos(theta);
            var y = length * Mathf.Sin(theta);
            transform.localPosition = originPos + new Vector3(x, y, 0);
        } else {
            Init();
            ObjectPool.Instance.Push(gameObject);
        }
    }

    /// <summary>
    /// 清洗数据的方法
    /// </summary>
    public void Init() {
        theta = 0;
        length = 0;
        originPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<MagicHealth>().Damage(damage);
            Init();
            ObjectPool.Instance.Push(gameObject);
        }
    }
}
