using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    private float alpha = 0f;
    private float alphaSpeed = 2.0f;
    private CanvasGroup cg;
    // Start is called before the first frame update

    private void Start()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
    }
    // Update is called once per frame
    void Update()
    {
        if (alpha != cg.alpha) {
            cg.alpha = Mathf.Lerp(cg.alpha, alpha, alphaSpeed * Time.deltaTime);
        }
        if (Mathf.Abs(alpha - cg.alpha) <= 0.01) { 
            cg.alpha = alpha;
        }
    }

    public void ShowWin() {
        alpha = 1;
        cg.blocksRaycasts = true;
        GameManager.Instance.Forbid();
    }
}
