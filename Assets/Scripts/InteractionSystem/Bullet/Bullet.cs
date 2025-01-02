using UnityEngine;

public class Bullet: EnemyBase {
    [Tooltip("子弹与父对象的最大值，超过后直接销毁")]
    public float maxDistance;

    [SerializeField]
    [Tooltip("这个子弹的运行方向")]
    protected Vector2 direction;

    [Tooltip("当前子弹运行速度")]
    public float speed;

    [Tooltip("子弹造成的伤害")]
    public int damage;


    public Vector2 Direction {
        get => direction;
        set => direction = value.normalized;
    }

    /// <summary>
    /// 设置各项数值
    /// </summary>
    /// <param name="maxDistance"></param>
    /// <param name="speed"></param>
    /// <param name="direction"></param>
    public virtual void Set(float maxDistance, int speed, Vector2 direction) {
        Direction = direction;
        this.speed = speed;
        this.maxDistance = maxDistance;
    }

    public virtual void Launch() { }
}
