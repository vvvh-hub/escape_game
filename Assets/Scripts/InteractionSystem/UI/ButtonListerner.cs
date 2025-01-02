using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonListerner: MonoBehaviour {
    /// <summary>
    /// 重新开始按钮响应
    /// </summary>
    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DeadUI.Instance.HideDead();
    }

    /// <summary>
    /// 退出按钮响应
    /// </summary>
    public void Close() {
        Application.Quit();
    }
}
