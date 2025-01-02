using UnityEngine;

public class UIBase: MonoBehaviour {
    [Tooltip("当前血量百分比")]
    protected float nowPercent;

    [Tooltip("当前血量目标值")]
    protected float target;

    protected float scale = 0f;


    [Tooltip("最大X缩放值,仅供敌方使用")]
    public float maxXScale = 5f;

    [Tooltip("最大Y缩放值,仅供敌方使用")]
    public float maxYScale = 3f;


    public float Target {
        get => target;
        set {
            target = value;
            scale = 0f;
        }
    }


    // Start is called before the first frame update
    void Start() {

    }

    /// <summary>
    /// 更改主角麦吉柯生命条进度
    /// </summary>
    /// <param name="begin">开始时的生命体进度，范围为[0,1]</param>
    /// <param name="end">结束时的生命体进度，范围[0,1]</param>
    public virtual void ChangeHealthPercent(float begin, float end) {
        if (Utils.CheckInputAvailable(begin, end, 0f, 1f)) {
            Target = end;
        }
        return;
    }
}
