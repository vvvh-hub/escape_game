using System.Collections;
using UnityEngine;

public class RobotGun: EnemyBase {
    [Tooltip("发射多少个子弹")]
    public int bulletNum = 5;

    [Tooltip("每两个子弹发射的间隔")]
    public float period = 1f;

    [Tooltip("感知距离")]
    public float distance = 10;

    [Tooltip("两次发射之间的时间")]
    public float periodTime = 10;

    float periodTimer;

    public GameObject magic;



    // Start is called before the first frame update
    void Start() {
        magic = GameObject.Find("/Magic");
    }

    // Update is called once per frame
    void Update() {
        if (canAttack) {
            if (periodTimer < periodTime) {
                periodTimer += Time.deltaTime;
            } else {
                if (Vector2.Distance(transform.position, magic.transform.position) < distance) {
                    Launch();
                    periodTimer = 0;
                }
            }
        }
    }

    /// <summary>
    /// 发射多个子弹
    /// </summary>
    /// <returns></returns>
    IEnumerator DoLaunch() {
        int num = 0;
        while (num < bulletNum) {
            var obj = ObjectPool.Instance.GetObject(Prefabs.Instance.Generator.ArchimedesBullet, null, transform.position);
            num++;
            obj.GetComponent<ArchimentsBullet>().Init();
            yield return new WaitForSeconds(period);
        }
    }

    /// <summary>
    /// 发射子弹的方法
    /// </summary>
    public void Launch() {
        StartCoroutine(DoLaunch());
    }
}
