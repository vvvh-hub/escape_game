using UnityEngine;

public class TheInvisibleUI: UIBase {
    public GameObject ui;

    // Start is called before the first frame update
    void Start() {
        ui.transform.localScale = new Vector3(maxXScale, maxYScale, 1);
        nowPercent = target = 1;
    }

    // Update is called once per frame
    void Update() {
        if (!Utils.Approximately(target, nowPercent)) {
            nowPercent = Mathf.Lerp(nowPercent, target, scale);
            scale += Time.deltaTime;
            ui.transform.localScale = new Vector3(nowPercent * maxXScale, maxYScale, 1);
        }
    }
}
