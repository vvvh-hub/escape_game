using UnityEngine;

public class EnemyMove: MonoBehaviour {
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    SpriteRenderer renderer2d;

    Rigidbody2D rigidbody2DForEnemy;
    float timer;
    int direction = 1;

    // 在第一次帧更新之前调用 Start
    void Start() {
        rigidbody2DForEnemy = GetComponent<Rigidbody2D>();
        timer = changeTime;
        renderer2d = GetComponent<SpriteRenderer>();
    }

    void Update() {
        timer -= Time.deltaTime;

        if (timer < 0) {
            direction = -direction;
            timer = changeTime;
            renderer2d.flipX = !renderer2d.flipX;
        }
    }

    void FixedUpdate() {
        Vector2 position = rigidbody2DForEnemy.position;

        if (vertical) {
            position.y = position.y + Time.deltaTime * speed * direction;
            ;
        } else {
            position.x = position.x + Time.deltaTime * speed * direction;
            ;
        }

        rigidbody2DForEnemy.MovePosition(position);
    }
}
