using System.Collections.Generic;
using UnityEngine;
//使用一个List存储主角的位置，追随者延迟读取位置信息实现跟随
public class SpiderMove: MonoBehaviour {
    public Transform TargetTF;                          //跟随对象
    public float RecordGap = 0.1f;                      //储存角主角位置时主角与追随者之间的间隔
    public float StopCount = 20f;                       //停止移动时剩余的位置信息个数
    public List<Vector2> PosList = new List<Vector2>();//位置信息表
    private AudioSource moveAudio;                      //蜘蛛移动音效
    Animator animator;

    // Start is called before the first frame update
    void Start() {
        PosList.Clear();
        moveAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (!DialogManager.dialogCompleted)
            return;
        if (TargetTF) {
            //删除已经到达的点
            while (PosList.Count > 0 && Vector2.Distance(transform.position, PosList[0]) < RecordGap) {
                PosList.RemoveAt(0);
                if (moveAudio.isPlaying) {
                    moveAudio.Pause();
                }
            }
            if (PosList.Count > 0) {
                //添加当前主角的位置信息
                if (Vector2.Distance(TargetTF.position, PosList[PosList.Count - 1]) > RecordGap) {
                    PosList.Add(TargetTF.position);
                }
                //移动处理
                if (PosList.Count > StopCount) {
                    transform.position = PosList[0];
                    if (!moveAudio.isPlaying) {
                        moveAudio.Play();
                        Vector2 pos0 = PosList[0], pos1 = PosList[1];
                        if (pos0.x - pos1.x < 0) {
                            animator.SetFloat("x", 1f);
                            animator.SetFloat("y", 0f);
                            animator.SetBool("Idle", false);
                            return;
                        } else if (pos0.x - pos1.x > 0) {
                            animator.SetFloat("x", -1f);
                            animator.SetFloat("y", 0f);
                            animator.SetBool("Idle", false);
                            return;
                        }

                        if (pos0.y - pos1.y > 0) {
                            animator.SetFloat("x", 0f);
                            animator.SetFloat("y", -1f);
                            animator.SetBool("Idle", false);
                        } else if (pos0.y - pos1.y < 0) {
                            animator.SetFloat("x", 0f);
                            animator.SetFloat("y", 1f);
                            animator.SetBool("Idle", false);
                        }
                    }
                    animator.SetBool("Idle", true);
                }
            } else {
                PosList.Add(TargetTF.position);
            }
        }
    }
}
