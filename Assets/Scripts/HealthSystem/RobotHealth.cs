using UnityEngine;

public class RobotHealth: HealthSystem {
    private AudioSource boomAudio;
    int num = 0;
    // Start is called before the first frame update
    void Start() {
        boomAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Hp <= 0 && num == 0) {
            boomAudio.PlayOneShot(Audios.Instance.Generator.RobotDead);
            transform.position = new Vector3(0, 0, 0);
            Invoke(nameof(Destroy), 3);
            num++;
        }
    }

    public override void Damage(int num) {
        boomAudio.Play();
        base.Damage(num);
    }

    public void Destroy() {
        ObjectPool.Instance.Push(gameObject);
    }

}
