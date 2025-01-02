using UnityEngine;

public class GasField: MonoBehaviour {
    [Tooltip("毒气区域边长")]
    public float length = 2.0f;
    [Tooltip("毒气区域单位时间造成的伤害")]
    public int damage = 2;
    [Tooltip("毒气区域存在时间")]
    private float existTime = 5.0f;
    [Tooltip("毒气区域存在时间")]
    public float existTimer = 5.0f;
    [Tooltip("毒气区域造成伤害的间隔时间")]
    public float period = 1.0f;
    [Tooltip("毒气伤害计时器")]
    private float damageTimer = 1f;
    [Tooltip("毒气伤害判定状态")]
    private bool isHarmable = true;

    // Start is called before the first frame update
    /// <summary>
    /// 设定毒气区域大小
    /// </summary>
    void Start() {
        transform.localScale = Vector3.one * length;
        this.existTime = this.existTimer;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init() {
        this.existTime = this.existTimer;
    }

    // Update is called once per frame
    /// <summary>
    /// 计时器功能
    /// </summary>
    void Update() {
        if (!isHarmable) {
            damageTimer -= Time.deltaTime;
            if (damageTimer < 0) {
                isHarmable = true;
            }
        }
        existTime -= Time.deltaTime;
        if (existTime < 0) {
            ObjectPool.Instance.Push(gameObject);
        }
    }
    /// <summary>
    /// 区域内停留造成伤害
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerStay2D(Collider2D collider) {
        if (!isHarmable) {
            return;
        }
        if (collider.gameObject.tag == "Player") {
            MagicHealth magicHealth = collider.GetComponent<MagicHealth>();
            if (magicHealth != null) {
                magicHealth.Damage(damage);
                isHarmable = false;
                damageTimer = period;
            }
        }
    }
}
