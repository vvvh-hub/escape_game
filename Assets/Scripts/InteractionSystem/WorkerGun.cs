using System.Collections;
using UnityEngine;

public class WorkerGun: EnemyBase {
    [Tooltip("感知距离")]
    public float distance = 10;
    [Tooltip("发射间隔")]
    public float launchPeriod = 5.0f;
    GameObject magic;
    [SerializeField]
    float timer = 0f;

    [SerializeField]
    bool isLaunch = true;
    // Start is called before the first frame update
    void Start() {
        magic = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if (canAttack && isLaunch && Vector2.Distance(transform.position, magic.transform.position) < distance) {
            Launch();
        }
    }

    /// <summary>
    /// 向四角发射大量子弹
    /// </summary>
    void Launch() {
        isLaunch = false;
        StartCoroutine(GeneratorBullet());
        StartCoroutine(LaunchTimer());
    }

    /// <summary>
    /// 发射子弹的动画
    /// </summary>
    /// <returns></returns>
    IEnumerator GeneratorBullet() {
        for (int i = 0; i < 15; i++) {
            GameObject bul1 = ObjectPool.Instance.GetObject(Prefabs.Instance.Generator.LineBullet, null, transform.position);
            bul1.GetComponent<LineBullet>().Set(20, 20, new Vector2(1, 1));
            bul1.GetComponent<LineBullet>().Launch();
            GameObject bul2 = ObjectPool.Instance.GetObject(Prefabs.Instance.Generator.LineBullet, null, transform.position);
            bul2.GetComponent<LineBullet>().Set(20, 20, new Vector2(1, -1));
            bul2.GetComponent<LineBullet>().Launch();
            GameObject bul3 = ObjectPool.Instance.GetObject(Prefabs.Instance.Generator.LineBullet, null, transform.position);
            bul3.GetComponent<LineBullet>().Set(20, 20, new Vector2(-1, 1));
            bul3.GetComponent<LineBullet>().Launch();
            GameObject bul4 = ObjectPool.Instance.GetObject(Prefabs.Instance.Generator.LineBullet, null, transform.position);
            bul4.GetComponent<LineBullet>().Set(20, 20, new Vector2(-1, -1));
            bul4.GetComponent<LineBullet>().Launch();
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator LaunchTimer() {
        while (timer < launchPeriod) {
            timer += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        timer = 0f;
        isLaunch = true;
    }
}
