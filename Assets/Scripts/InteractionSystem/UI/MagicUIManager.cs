/// <summary>
/// 注意可优化动画效果
/// TODO: 使动画不会出现明显的被打断现象
/// </summary>

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicUIManager: UIBase {

    [SerializeField]
    RectTransform magicBar;

    public Image magicBarInner;

    [SerializeField]
    RectTransform healthBar;
    // Start is called before the first frame update

    [Tooltip("提示")]
    public TMP_Text remainder;

    bool needRemainder;

    float remainderTimer = 0;

    float magicNowPercent;

    float magicTarget;

    float magicScale = 0;

    public static MagicUIManager Instance;

    public float MagicTarget {
        get => magicTarget;
        set {
            magicTarget = value;
            magicScale = 0;
        }
    }

    void Awake() {
        healthBar = GameObject.Find("MagicHealthBarMask").GetComponent<RectTransform>();
        magicBar = GameObject.Find("MagicMagicPointBarMask").GetComponent<RectTransform>();
        Instance = this;
    }

    void Start() {
        magicNowPercent = magicTarget = 1;
        nowPercent = target = 1;
    }

    // Update is called once per frame
    void Update() {
        if (!Utils.Approximately(target, nowPercent)) {
            nowPercent = Mathf.Lerp(nowPercent, target, scale);
            scale += Time.deltaTime;
            healthBar.localScale = new Vector3(nowPercent, 1, 1);
        }
        if (!Utils.Approximately(MagicTarget, magicNowPercent)) {
            magicNowPercent = Mathf.Lerp(magicNowPercent, MagicTarget, magicScale);
            magicScale += Time.deltaTime;
            magicBar.localScale = new Vector3(magicNowPercent, 1, 1);
        }

        if (needRemainder) {
            remainderTimer += Time.deltaTime;
            if (remainderTimer > 3.0f) {
                remainder.SetText("");
                needRemainder = false;
            }
        }
    }

    /// <summary>
    /// 更改主角麦吉柯魔力条进度
    /// </summary>
    /// <param name="begin">开始时的魔力条进度，范围为[0,1]</param>
    /// <param name="end">结束时的魔力条进度，范围[0,1]</param>
    public void ChangeMagicPercent(float begin, float end) {
        if (Utils.CheckInputAvailable(begin, end, 0f, 1f)) {
            MagicTarget = end;
        }
        return;
    }

    /// <summary>
    /// 显示提示文字
    /// </summary>
    /// <param name="text">提示文字内容</param>
    public void ShowRemainder(string text) {
        needRemainder = true;
        remainder.SetText(text);
        remainder.gameObject.SetActive(true);
        remainderTimer = 0;
    }

    /// <summary>
    /// 设置蓝条是否变灰
    /// </summary>
    /// <param name="needGray"></param>
    public void SetMagicGray(bool needGray) {
        if (needGray) {
            magicBarInner.color = Color.black;
        } else {
            magicBarInner.color = Color.white;
        }
    }
}
