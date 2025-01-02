using UnityEngine;

[CreateAssetMenu(fileName = "PrefabGenerator", menuName = "ScriptableObject/Prefabs", order = 0)]
public class PrefabGenerator: ScriptableObject {
    [Tooltip("机械蜘蛛")]
    public GameObject CyberSpider;

    [Tooltip("直线子弹")]
    public GameObject LineBullet;

    [Tooltip("阿基米德螺线轨迹子弹")]
    public GameObject ArchimedesBullet;

    [Tooltip("闪光弹")]
    public GameObject FlashBoom;

    [Tooltip("毒气弹")]
    public GameObject GasBullet;

    [Tooltip("激光枪")]
    public GameObject LaserGun;

    [Tooltip("毒气区域")]
    public GameObject GasField;

    [Tooltip("机器人")]
    public GameObject Robot;

    [Tooltip("隐形者")]
    public GameObject TheVisible;

    [Tooltip("普通员工")]
    public GameObject TheWorker;

    [Tooltip("麦吉柯")]
    public GameObject Magic;

    [Tooltip("麦吉柯普通子弹")]
    public GameObject MagicNormalBullet;

    [Tooltip("麦吉柯火焰子弹")]
    public GameObject MagicFireBullet;

    [Tooltip("生物昏睡场")]
    public GameObject SleepRange;

    [Tooltip("机械昏睡场")]
    public GameObject RobotRange;

    [Tooltip("无敌场")]
    public GameObject MagicRange;
}
