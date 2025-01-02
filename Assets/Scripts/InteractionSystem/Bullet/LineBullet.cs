using System.Collections;
using UnityEngine;

public class LineBullet: Bullet {
    [Tooltip("子弹生成坐标")]
    private Vector3 startPos;
    protected AudioSource launchAudio;
    // Start is called before the first frame update

    private void Start() {
        launchAudio = GetComponent<AudioSource>();
        launchAudio.Play();
    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// 发射子弹方法
    /// </summary>
    public override void Launch() {
        startPos = transform.position;
        StartCoroutine(DoLaunch());
    }

    /// <summary>
    /// 子弹运行动画协程
    /// </summary>
    /// <returns></returns>
    IEnumerator DoLaunch() {
        Vector3 forward = direction;
        forward.z = 0;
        float angle = Vector3.SignedAngle(Vector3.up, forward, Vector3.forward);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        while (Vector2.Distance(transform.position, startPos) < maxDistance) {
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
            yield return new WaitForSeconds(0.02f);
        }
        transform.position = default;
        ObjectPool.Instance.Push(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<HealthSystem>().Damage(damage);
            transform.position = default;
            ObjectPool.Instance.Push(gameObject);
        }
    }
}
