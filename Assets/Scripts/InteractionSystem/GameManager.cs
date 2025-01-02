using Cinemachine;
using System.Collections;
using UnityEngine;

public class GameManager: MonoBehaviour {
    [Tooltip("主角Magic游戏对象")]
    public GameObject magic;

    [Tooltip("能否进行传送")]
    public bool canTransfer;

    [Tooltip("传送目标带点")]
    public Vector3 target;

    [Tooltip("现有钥匙数量")]
    int keyNum = 0;

    [Tooltip("得分")]
    int score;

    [Tooltip("主角UI")]
    public MagicUIManager magicUI;

    public CinemachineVirtualCamera thecamera;

    public static GameManager Instance;

    public WinUI win;

    private void Awake() {
        Instance = this;
        
    }

    // Start is called before the first frame update
    void Start() {
        magicUI = MagicUIManager.Instance;
        if (DialogManager.first) {
            DialogManager.Instance.ShowDialog();
            canTransfer = false;
        } else {
            canTransfer = true;
            ChangeOrthSize(10f);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (canTransfer && DialogManager.dialogCompleted) {
                Transfer();
            }
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            if (keyNum == 3) {
                Win();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Comma)) {
            keyNum = 3;
        }
    }

    /// <summary>
    /// 将magic的位置改为target
    /// </summary>
    public void Transfer() {
        magic.transform.position = target;
    }

    /// <summary>
    /// 增加钥匙数量
    /// </summary>
    public void AddKey() {
        keyNum++;
    }

    /// <summary>
    /// 游戏胜利的方法
    /// </summary>
    public void Win() {
        win.ShowWin();
    }

    /// <summary>
    /// 加分
    /// </summary>
    /// <param name="num"></param>
    public void AddScore(int num) {
        score += num;
    }

    /// <summary>
    /// 减分
    /// </summary>
    /// <param name="num"></param>
    public void DeleteScore(int num) {
        score -= num;
    }

    /// <summary>
    /// 返回当前的分数
    /// </summary>
    /// <returns></returns>
    public int GetScore() {
        return score;
    }

    /// <summary>
    /// 获取当前拥有的钥匙数量
    /// </summary>
    /// <returns></returns>
    public int GetKeyNum() {
        return keyNum;
    }

    /// <summary>
    /// 阻止Magic移动和释放技能
    /// </summary>
    public void Forbid() {
        magic.GetComponent<MagicMove>().canMove = false;
        magic.GetComponent<MagicSkill>().canSkill = false;
        magicUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// 让Magic可以移动和释放技能
    /// </summary>
    public void Allow() {
        magic.GetComponent<MagicMove>().canMove = true;
        magic.GetComponent<MagicSkill>().canSkill = true;
        magicUI.gameObject.SetActive(true);
    }

    /// <summary>
    /// 更改相机跟随目标
    /// </summary>
    /// <param name="trans"></param>
    public void Follow(Transform trans) {
        thecamera.Follow = trans;
    }

    /// <summary>
    /// 更改镜头聚焦
    /// </summary>
    /// <param name="size"></param>
    public void ChangeOrthSize(float size) {
        StartCoroutine(DoChangeOrthSize(size));
    }

    IEnumerator DoChangeOrthSize(float size) {
        float oldsize = thecamera.m_Lens.OrthographicSize;
        float factor = 0f;
        while (factor < 1.1f) {
            thecamera.m_Lens.OrthographicSize = Mathf.Lerp(oldsize, size, factor);
            factor += 0.03f;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
