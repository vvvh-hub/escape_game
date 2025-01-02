using System;
using System.Collections;
using UnityEngine;

public class MagicSkill: SkillSystem {
    [Tooltip("当前法力值")]
    private int magic;

    [Tooltip("最大法力值")]
    public int maxMagic;

    [Tooltip("技能释放方向")]
    public Vector2 bulletDirection;

    [Tooltip("同步更改的法力UI")]
    private MagicUIManager magicUI;

    [Tooltip("技能昏睡的配置")]
    public Sleep sleep;

    [Tooltip("技能魔力场的配置")]
    public RemoveBullet removeBullet;

    [Tooltip("技能掠夺的配置")]
    public SleepRobot sleepRobot;

    [Tooltip("技能火焰弹的配置")]
    public FireBullet fireBullet;

    [Tooltip("是否被抑制")]
    bool restrained = false;

    [Tooltip("现在能否释放技能")]
    public bool canSkill = true;

    public void ChangeSleepRaius(float radius) {
        sleep.radius = radius;
    }

    public int Magic {
        get => magic;
        set {
            if (value < 0 || value > maxMagic)
                return;
            float begin = magic * 1.0f / maxMagic;
            magic = value;
            float end = magic * 1.0f / maxMagic;
            magicUI.ChangeMagicPercent(begin, end);
        }
    }

    public void AddMagic(int num) {
        Magic += num;
    }
    // Start is called before the first frame update
    void Start() {
        magicUI = MagicUIManager.Instance;
        magic = maxMagic;
        sleep = new Sleep();
        sleepRobot = new SleepRobot();
        fireBullet = new FireBullet();
        removeBullet = new RemoveBullet();
    }

    // Update is called once per frame
    void Update() {
        if (!canSkill)
            return;
        if (restrained) {
            magicUI.ShowRemainder("由于附近有魔力抑制装置，无法释放技能！");
            return;
        }
        /****************普通攻击**************************/
        if (Input.GetKeyDown(KeyCode.J)) {
            var normalBullet = ObjectPool.Instance.GetObject(Prefabs.Instance.Generator.MagicNormalBullet, null, transform.position);
            bulletDirection = gameObject.GetComponent<MagicMove>().GetSeeDirection();
            normalBullet.GetComponent<MagicNormalBullet>().Set(0, 0, bulletDirection);
            normalBullet.GetComponent<MagicNormalBullet>().Launch();
        }
        /****************普通攻击结束**************************/

        /****************火球技能**************************/
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (magic - fireBullet.magicCost < 0) {
                magicUI.ShowRemainder("魔力不足！");
                return;
            }
            var magicFireBullet = ObjectPool.Instance.GetObject(Prefabs.Instance.Generator.MagicFireBullet, null, transform.position);
            bulletDirection = gameObject.GetComponent<MagicMove>().GetSeeDirection();
            magicFireBullet.GetComponent<MagicFireBullet>().Set(0, 0, bulletDirection);
            magicFireBullet.GetComponent<MagicFireBullet>().Launch();
            Cost(fireBullet.magicCost);
        }
        /*****************火球技能结束*********************/

        /****************睡眠技能**************************/
        if (Input.GetKeyDown(KeyCode.V)) {
            if (magic - sleep.magicCost < 0) {
                magicUI.ShowRemainder("魔力不足！");
                return;
            }
            if (!sleep.IsInSkill && sleep.IsInCoolTime) {
                sleep.IsInCoolTime = false;
                StartCoroutine(sleep.DoLaunch());
                StartCoroutine(sleep.CDCount());
                Cost(sleep.magicCost);
            } else if (sleep.IsInSkill) {
                magicUI.ShowRemainder("该技能还没结束！");
            } else if (!sleep.IsInCoolTime) {
                magicUI.ShowRemainder("该技能正在冷却！");
            }
        }
        /*****************睡眠技能结束*********************/

        /****************睡眠机械装置技能**************************/
        if (Input.GetKeyDown(KeyCode.R)) {
            if (magic - sleepRobot.magicCost < 0) {
                magicUI.ShowRemainder("魔力不足！");
                return;
            }
            if (!sleepRobot.IsInSkill && sleepRobot.IsInCoolTime) {
                sleepRobot.IsInCoolTime = false;
                StartCoroutine(sleepRobot.DoLaunch());
                StartCoroutine(sleepRobot.CDCount());
                Cost(sleepRobot.magicCost);
            } else if (sleepRobot.IsInSkill) {
                magicUI.ShowRemainder("该技能还没结束！");
            } else if (!sleepRobot.IsInCoolTime) {
                magicUI.ShowRemainder("该技能还在冷却！");
            }
        }
        /*****************睡眠机械装置技能结束*********************/

        /****************魔力场技能**************************/
        if (Input.GetKeyDown(KeyCode.E)) {
            if (magic - removeBullet.magicCost < 0) {
                magicUI.ShowRemainder("魔力不足！");
                return;
            }
            if (!removeBullet.IsInSkill && removeBullet.IsInCoolTime) {
                removeBullet.IsInCoolTime = false;
                StartCoroutine(removeBullet.DoLaunch());
                StartCoroutine(removeBullet.CDCount());
                Cost(removeBullet.magicCost);
            } else if (removeBullet.IsInSkill) {
                magicUI.ShowRemainder("该技能还没结束！");
            } else if (!removeBullet.IsInCoolTime) {
                magicUI.ShowRemainder("该技能正在冷却！");
            }
        }
        /*****************魔力场技能结束*********************/
    }

    /// <summary>
    /// 减少法力值
    /// </summary>
    /// <param name="num"></param>
    public void Cost(int num) {
        Magic -= num;
    }

    /// <summary>
    /// 补充法力值
    /// </summary>
    /// <param name="num"></param>
    public void Supply(int num) {
        Magic += num;
    }

    public void SetRestrain(bool isRestrain) {
        restrained = isRestrain;
    }
}

public class Sleep {
    [Tooltip("技能半径")]
    public float radius = 6.56f;
    public Transform transform;
    [Tooltip("技能消耗")]
    public int magicCost;
    [Tooltip("技能持续时间")]
    public float remainTime;
    [Tooltip("技能CD")]
    public float CD;
    float remainTimer = 0;
    float cdTimer = 0;
    public bool IsInCoolTime = true;
    public bool IsInSkill { get => remainTimer != 0; }

    public Sleep() {
        radius = 6.56f;
        transform = GameManager.Instance.magic.transform;
        magicCost = 100;
        remainTime = 5f;
        CD = 10f;
    }

    public IEnumerator DoLaunch() {
        var range = ObjectPool.Instance.GetObject(Prefabs.Instance.Generator.SleepRange, transform, relative: true);
        range.GetComponent<Range>().Set(Enter, Exit, "HumanEnemy");
        while (remainTimer < remainTime) {
            remainTimer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        remainTimer = 0;
        ObjectPool.Instance.Push(range);
    }

    public IEnumerator CDCount() {
        while (cdTimer < CD) {
            cdTimer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        cdTimer = 0;
        IsInCoolTime = true;
    }

    public void Enter(GameObject obj) {
        obj.GetComponent<EnemyBase>().canAttack = false;
    }

    public void Exit(GameObject obj) {
        obj.GetComponent<EnemyBase>().canAttack = true;
    }
}

public class SleepRobot {
    [Tooltip("技能半径")]
    public float radius;
    public Transform transform;
    [Tooltip("技能消耗")]
    public int magicCost;
    [Tooltip("技能持续时间")]
    public float remainTime;
    [Tooltip("技能CD")]
    public float CD;
    float remainTimer = 0;
    float cdTimer = 0;
    public bool IsInCoolTime = true;
    public bool IsInSkill { get => remainTimer != 0; }
    public IEnumerator DoLaunch() {
        var range = ObjectPool.Instance.GetObject(Prefabs.Instance.Generator.RobotRange, transform, relative: true);
        range.GetComponent<Range>().Set(Enter, Exit, "RobotEnemy");
        while (remainTimer < remainTime) {
            remainTimer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        remainTimer = 0;
        ObjectPool.Instance.Push(range);
    }

    public SleepRobot() {
        radius = 6.56f;
        transform = GameManager.Instance.magic.transform;
        magicCost = 100;
        remainTime = 5f;
        CD = 10f;
    }

    public IEnumerator CDCount() {
        while (cdTimer < CD) {
            cdTimer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        cdTimer = 0;
        IsInCoolTime = true;
    }

    public void Enter(GameObject obj) {
        obj.GetComponent<EnemyBase>().canAttack = false;
    }

    public void Exit(GameObject obj) {
        obj.GetComponent<EnemyBase>().canAttack = true;
    }
}

public class RemoveBullet {
    [Tooltip("技能半径")]
    public float radius = 6.11f;
    public Transform transform;
    [Tooltip("技能消耗")]
    public int magicCost;
    [Tooltip("技能持续时间")]
    public float remainTime;
    [Tooltip("技能CD")]
    public float CD;
    float remainTimer = 0;
    float cdTimer = 0;
    public bool IsInCoolTime = true;
    public bool IsInSkill { get => remainTimer != 0; }

    public IEnumerator DoLaunch() {
        var range = ObjectPool.Instance.GetObject(Prefabs.Instance.Generator.MagicRange, transform, relative: true);
        range.GetComponent<Range>().Set(Enter, null, "EnemyBullet");
        while (remainTimer < remainTime) {
            remainTimer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        remainTimer = 0;
        ObjectPool.Instance.Push(range);
    }

    public RemoveBullet() {
        radius = 6.11f;
        transform = GameManager.Instance.magic.transform;
        magicCost = 100;
        remainTime = 5f;
        CD = 10f;
    }

    public IEnumerator CDCount() {
        while (cdTimer < CD) {
            cdTimer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        cdTimer = 0;
        IsInCoolTime = true;
    }

    public void Enter(GameObject obj) {
        ObjectPool.Instance.Push(obj);
    }

}

public class FireBullet {
    [Tooltip("技能消耗")]
    public int magicCost;

    public FireBullet() {
        magicCost = 25;
    }

}