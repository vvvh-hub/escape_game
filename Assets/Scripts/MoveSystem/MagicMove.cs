using UnityEngine;

public class MagicMove: MoveSystem {
    [Tooltip("走路音效")]
    AudioSource walkSound;

    [Tooltip("水平虚拟轴")]
    float horizontal;

    [Tooltip("竖直虚拟轴")]
    float vertical;

    [Tooltip("角色朝向")]
    Vector2 seeDirection;

    [Tooltip("现在能否移动")]
    public bool canMove = true;

    public bool CanMove = true;

    Animator animator;
    // Start is called before the first frame update
    void Start() {
        walkSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    void Update() {
        if (!canMove) {
            horizontal = vertical = 0;
            return;
        }
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    // Update is called once per frame
    void FixedUpdate() {
        if (!walkSound.isPlaying) {
            walkSound.Play();
        }

        if (Mathf.Approximately(horizontal, 0f) && Mathf.Approximately(vertical, 0f)) {
            animator.SetBool("Idle", true);
            if (walkSound.isPlaying)
                walkSound.Pause();
            return;
        }

        animator.SetBool("Idle", false);
        animator.SetFloat("x", horizontal);
        animator.SetFloat("y", vertical);

        if (vertical > 0) {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            seeDirection = new Vector2(0, 1);
        }

        if (horizontal < 0) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            seeDirection = new Vector2(-1, 0);
        }

        if (vertical < 0) {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            seeDirection = new Vector2(0, -1);
        }

        if (horizontal > 0) {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            seeDirection = new Vector2(1, 0);
        }
    }

    public Vector2 GetSeeDirection() {
        return seeDirection;
    }

    public void PlayDeadAnimation() {
        animator.SetTrigger("Dead");
    }
}

