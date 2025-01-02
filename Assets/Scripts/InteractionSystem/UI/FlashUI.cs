using System.Collections;
using UnityEngine;

public class FlashUI: MonoBehaviour {
    public static FlashUI Instance;

    CanvasGroup group;

    [Tooltip("闪光弹闪烁时间")]
    public float falshTime = 2f;
    // Start is called before the first frame update
    void Start() {
        Instance = this;
        group = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 闪光弹方法
    /// </summary>
    public void Flash() {
        StartCoroutine(DoFlash());
    }

    IEnumerator DoFlash() {
        // 第一阶段，迅速将group改为不透明
        float timer = 0f;
        while (timer <= 0.5f) {
            float alpha = Mathf.Lerp(0, 1, timer / 0.5f);
            timer += 0.1f;
            group.alpha = alpha;
            yield return new WaitForSeconds(0.1f);
        }

        // 第二阶段，保持
        timer = 0f;
        while (timer <= falshTime) {
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        // 第三阶段，逐渐消失
        timer = 0f;
        while (timer <= 1.5f) {
            float alpha = Mathf.Lerp(1, 0, timer / 1.5f);
            group.alpha = alpha;
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
