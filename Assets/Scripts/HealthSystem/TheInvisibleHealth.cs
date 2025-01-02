using UnityEngine;

public class TheInvisibleHealth: HealthSystem {
    int num = 0;
    private AudioSource deathAudio;
    // Start is called before the first frame update
    void Start() {
        deathAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Hp <= 0 && num == 0) {
            deathAudio.PlayOneShot(Audios.Instance.Generator.TheInvisibleDeath);
            num++;
            Invoke(nameof(Destroy), 3);
            transform.position = new Vector3(0, 0, 0);
        }
    }

    void Destroy() {
        ObjectPool.Instance.Push(gameObject);
    }

    public override void Damage(int num) {
        base.Damage(num);
        deathAudio.Play();
    }
}
