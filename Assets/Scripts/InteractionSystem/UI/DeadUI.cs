using System.Collections;
using UnityEngine;

public class DeadUI: MonoBehaviour {
    public static DeadUI Instance;
    CanvasGroup group;
    float timer = 0f;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        group = GetComponent<CanvasGroup>();
        HideDead();
    }

    /// <summary>
    /// 展示死亡UI
    /// </summary>
    public void ShowDead() {
        gameObject.SetActive(value: true);
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// 隐藏死亡UI
    /// </summary>
    public void HideDead() {
        gameObject.SetActive(false);
        group.alpha = 0;
    }

    /// <summary>
    /// 死亡UI浮现
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn() {
        MagicUIManager.Instance.ShowRemainder("");
        GameManager.Instance.Forbid();
        GameManager.Instance.magic.GetComponent<MagicMove>().PlayDeadAnimation();
        while (timer <= 1) {
            group.alpha = timer;
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
