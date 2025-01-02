using UnityEngine;

public class MagicHealth: HealthSystem {
    Animator animator;
    public AudioSource getHurtSoure;
    // Start is called before the first frame update
    void Start() {
        hp = maxHp;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Hp <= 0) {
            getHurtSoure.PlayOneShot(Audios.Instance.Generator.GameOver, 0.7f);
            getHurtSoure.PlayOneShot(Audios.Instance.Generator.MagicCollapse);
            Destroy(GetComponent<BoxCollider2D>());
            DeadUI.Instance.ShowDead();
        }
    }

    public override void Damage(int num) {
        base.Damage(num);
        animator.SetTrigger("Attacked");
        getHurtSoure.Play();
    }
}
