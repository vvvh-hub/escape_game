using System.Collections;
using UnityEngine;




public class LaserBullet: Bullet {
    [Tooltip("线")]
    public GameObject line;

    [Tooltip("激光")]
    public GameObject laser;

    [Tooltip("Magic对象")]
    public GameObject magic;

    [Tooltip("当前状态")]
    LaserBulletState state;

    [Tooltip("从chasing到readying需要的时间")]
    public float chasingToReadying = 3f;

    [Tooltip("从readying到launching需要的时间")]
    public float readyingToLaunching = 1f;

    [Tooltip("从Launching到Cooling需要的时间")]
    public float launchingToCooling = 1f;

    [Tooltip("从Cooling到Waiting需要的时间")]
    public float coolingToWaiting = 5f;

    [Tooltip("之前指向的位置")]
    public Vector3 positionBefore;

    [Tooltip("激光发射音效")]
    private AudioSource laserLaunchAudio;

    [SerializeField]
    [Tooltip("反向因子")]
    int reverse = 1;

    public LaserBulletState State {
        get => state;
        set {
            if (value != state) {
                state = value;
                switch (state) {
                    case LaserBulletState.Waiting:
                        line.SetActive(false);
                        laser.SetActive(false);
                        break;
                    case LaserBulletState.Chasing:
                        line.SetActive(true);
                        laser.SetActive(false);
                        StartCoroutine(WaitingToNextStage(chasingToReadying));
                        break;
                    case LaserBulletState.Readying:
                        StartCoroutine(WaitingToNextStage(readyingToLaunching));
                        break;
                    case LaserBulletState.Launching:
                        line.SetActive(false);
                        laser.SetActive(true);
                        StartCoroutine(WaitingToNextStage(launchingToCooling));
                        laserLaunchAudio.Play();
                        break;
                    case LaserBulletState.Cooling:
                        line.SetActive(false);
                        laser.SetActive(false);
                        StartCoroutine(WaitingToNextStage(coolingToWaiting));
                        break;
                }
            }
        }
    }

    public enum LaserBulletState {
        Waiting,
        Chasing,
        Readying,
        Launching,
        Cooling
    }

    void Start() {
        State = LaserBulletState.Waiting;
        line.GetComponent<LineRenderer>().positionCount = 2;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, 0));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(maxDistance, 0, 0));
        laser.transform.localScale = new Vector3(maxDistance, 1, 1);
        magic = GameObject.Find("Magic");
        laserLaunchAudio = GetComponent<AudioSource>();
    }

    void Update() {
        if (canAttack)
            CheckStateBehavior();
    }

    /// <summary>
    /// 用来渡过每个阶段的冷却时间
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator WaitingToNextStage(float waitTime) {
        float timer = 0f;
        while (timer < waitTime) {
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        NextState();
    }

    /// <summary>
    /// 切换到下一帧的函数
    /// </summary>
    void NextState() {
        switch (State) {
            case LaserBulletState.Waiting:
                break;
            case LaserBulletState.Chasing:
                State = LaserBulletState.Readying;
                break;
            case LaserBulletState.Readying:
                State = LaserBulletState.Launching;
                break;
            case LaserBulletState.Launching:
                State = LaserBulletState.Cooling;
                break;
            case LaserBulletState.Cooling:
                State = LaserBulletState.Waiting;
                break;
        }
    }

    /// <summary>
    /// 每一帧调用的函数，根据当前状态来确定行为
    /// </summary>
    void CheckStateBehavior() {
        if (Vector2.Distance(magic.transform.position, transform.position) > maxDistance)
            return;
        switch (State) {
            case LaserBulletState.Chasing:
                direction = (magic.transform.position - transform.position).normalized;
                Vector3 oriDir = Utils.EluerToVector(transform.eulerAngles);
                float z = Vector3.Cross(direction, oriDir).z;
                if (Utils.Approximately(z, 0))
                    reverse = 0;
                else if (z > 0)
                    reverse = -1;
                else
                    reverse = 1;
                transform.Rotate(0, 0, speed * Time.deltaTime * reverse);
                break;
            case LaserBulletState.Waiting:
                State = LaserBulletState.Chasing;
                break;
            default:
                break;
        }
    }


}
