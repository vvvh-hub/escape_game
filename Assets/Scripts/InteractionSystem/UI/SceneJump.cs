using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneJump: MonoBehaviour {
    public Image sceneTransit;
    public void JumpToGameScene() {
        sceneTransit.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }
    public void ExitGame() {
        Application.Quit();
    }

    void Start() {
        sceneTransit.gameObject.SetActive(false);
    }

    IEnumerator FadeIn() {
        float timer = 0;
        while (timer <= 1.2f) {
            sceneTransit.color = new Color(1, 1, 1, timer);
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
            Debug.Log(timer);
        }
        SceneManager.LoadScene(1);
    }
}
