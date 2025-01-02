using System.Collections;
using UnityEngine;

public class GasGun: EnemyBase {
    [Tooltip("子弹预制件")]
    public GameObject gasBulletPrefab;

    [Tooltip("两次发射之间的时间间隔")]
    public float periodTime = 3;

    [Tooltip("响应距离")]
    public float distance = 20;

    [Tooltip("攻击时显形的时间")]
    public float betrayTime = 2;

    GameObject magic;

    float periodTimer;


    private void Start() {
        magic = GameObject.Find("/Magic");
    }

    // Start is called before the first frame update
    void Update() {
        if (canAttack) {
            if (periodTimer < periodTime) {
                periodTimer += Time.deltaTime;
            } else {
                if (Vector2.Distance(magic.transform.position, transform.position) < distance) {
                    LaunchGasBullet();
                    periodTimer = 0;
                }
            }
        }
    }
    /// <summary>
    /// 发射子弹
    /// </summary>
    private void LaunchGasBullet() {
        GameObject gasBulletObject = ObjectPool.Instance.GetObject(gasBulletPrefab, null, new Vector3(transform.position.x, transform.position.y, 0));
        GasBullet gasBullet = gasBulletObject.GetComponent<GasBullet>();
        gasBullet.Launch();
        Betary();
    }
    /// <summary>
    /// 现形的方法
    /// </summary>
    public void Betary() {
        StartCoroutine(DoBetray());
    }

    /// <summary>
    /// 隐身时间结束的协程
    /// </summary>
    /// <returns></returns>
    IEnumerator DoBetray() {
        float timer = 0;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        while (timer < betrayTime) {
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -25);
    }
}
