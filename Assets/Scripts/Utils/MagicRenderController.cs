using System.Collections.Generic;
using UnityEngine;

public class MagicRenderController: MonoBehaviour {
    SpriteRenderer render;

    List<Rect> rects = new List<Rect>() {
        new Rect(new Vector2(-31.6f, -11.3f), new Vector2(11.5f, -14.5f)),
        new Rect(new Vector2(379.73f, 175.6f), new Vector2(414.4f, 172.04f)),
        new Rect(new Vector2(382.76f, 152.44f), new Vector2(435.23f, 149.56f)),
        new Rect(new Vector2(418.64f, 164.41f), new Vector2(449.46f, 160.9f)),
        new Rect(new Vector2(383.77f, 127.58f), new Vector2(449.47f, 124.48f)),
        new Rect(new Vector2(392.93f, 138.6f), new Vector2(414.34f, 136.48f)),
        new Rect(new Vector2(451.68f, 130f), new Vector2(481.522f, 126.8f)),
        new Rect(new Vector2(483.7f, 114.6f), new Vector2(550.65f, 110f)),
        new Rect(new Vector2(419.54f, 96.85f), new Vector2(461.314f, 93.37f)),
        new Rect(new Vector2(441.77f, 83.84f), new Vector2(447.65f, 81.36f)),
        new Rect(new Vector2(451.76f, 148.14f), new Vector2(469.53f, 144.49f)),
        new Rect(new Vector2(470f, 153.83f), new Vector2(484.274f, 150.76f)),
        new Rect(new Vector2(486.8f, 154.2f), new Vector2(506.364f, 151.65f)),
        new Rect(new Vector2(464.18f, 99.5f), new Vector2(496.65f, 97.79f)),
        new Rect(new Vector2(482.81f, 90.4f), new Vector2(583.39f, 86.92f)),
        new Rect(new Vector2(553.25f, 134.3f), new Vector2(636.291f, 130.75f)),
        new Rect(new Vector2(638.86f, 114.6f), new Vector2(675.681f, 110.96f)),
        new Rect(new Vector2(590.70f, 89.65f), new Vector2(623.5f, 86.58f)),
        new Rect(new Vector2(625.54f, 99.51f), new Vector2(638.531f, 96.44f)),
    };

    private void Start() {
        render = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        var first = FindFirst(transform.position, rects);
        if (first == null) {
            render.sortingOrder = 0;
        } else {
            render.sortingOrder = -20;
        }

    }

    public Rect FindFirst(Vector2 position, List<Rect> lists) {
        foreach (var rect in lists) {
            if (rect.InThisRect(position)) {
                return rect;
            }
        }
        return null;
    }

    public class Rect {
        Vector2 leftTop;
        Vector2 rightBottom;

        public Rect(Vector2 leftTop, Vector2 rightBottom) {
            this.leftTop = leftTop;
            this.rightBottom = rightBottom;
        }

        public bool InThisRect(Vector2 pos) {
            return pos.x > leftTop.x && pos.x < rightBottom.x &&
                pos.y > rightBottom.y && pos.y < leftTop.y;
        }
    }
}
