using System.Collections;
using UnityEngine;

public class GasBullet: Bullet {
    [Tooltip("毒气区域预制件")]
    public GameObject gasFieldPrefab;

    [Tooltip("毒气弹旋转速率")]
    public float rotationRate = 8f;

    [Tooltip("毒气弹生成坐标")]
    private Vector3 startPos;
    // Start is called before the first frame update

    /// <summary>
    /// 协程发射毒气弹
    /// </summary>
    public override void Launch() {
        direction = GetDirection(direction);
        startPos = transform.position;
        StartCoroutine(DoLaunch());
    }

    IEnumerator DoLaunch() {
        while (Vector2.Distance(transform.position, startPos) < maxDistance) {
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
            transform.eulerAngles += new Vector3(0, 0, rotationRate);
            yield return new WaitForSeconds(0.02f);
        }
        CreatField();
        transform.position = default;
        ObjectPool.Instance.Push(gameObject);
    }
    /// <summary>
    /// 毒气弹与主角碰撞判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            CreatField();
            transform.position = default;
            ObjectPool.Instance.Push(gameObject);
        }
    }
    /// <summary>
    /// 当毒气弹到达最大运行距离或者与主角接触，当即创建毒气区域
    /// </summary>
    private void CreatField() {
        GameObject gasField = ObjectPool.Instance.GetObject(gasFieldPrefab);
        gasField.GetComponent<GasField>().Init();
        gasField.transform.position = transform.position;
    }
    /// <summary>
    /// 获取毒气弹运行方向
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private Vector2 GetDirection(Vector2 direction) {
        GameObject player = GameObject.FindWithTag("Player");
        direction = (player.transform.position - transform.position).normalized;
        return direction;
    }
}
