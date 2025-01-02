using UnityEngine;

public class HealthSystem: MonoBehaviour {
    [SerializeField]
    [Tooltip("对象当前血量")]
    protected int hp;

    [Tooltip("对象最大血量")]
    public int maxHp;

    [Tooltip("需要同步更改的UI组件")]
    public UIBase ui;

    public int Hp {
        get => hp;
        set {
            float begin = hp * 1.0f / maxHp;
            hp = Mathf.Clamp(value, 0, maxHp);
            float end = hp * 1.0f / maxHp;
            ui.ChangeHealthPercent(begin, end);
        }
    }

    /// <summary>
    /// 更改对象的最大生命值
    /// </summary>
    /// <param name="maxHealth">对象新的最大生命值</param>
    public void ChangeMaxHealth(int maxHealth) {
        maxHp = maxHealth;
        Hp = maxHp;
    }

    /// <summary>
    /// 造成伤害
    /// </summary>
    /// <param name="num">伤害数值</param>
    public virtual void Damage(int num) {
        Hp -= num;
    }

    /// <summary>
    /// 回血
    /// </summary>
    /// <param name="num">回血量</param>
    public void Heal(int num) {
        Hp += num;
    }

    void Start() {
        hp = maxHp;
    }
}
