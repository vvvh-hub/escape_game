using UnityEngine;

[CreateAssetMenu(fileName = "SoundAssets", menuName = "ScriptableObject/Sounds")]
public class AudioGenerator: ScriptableObject {
    [Tooltip("机器人受伤音效")]
    public AudioClip RobotGetHurt;

    [Tooltip("机器人死亡音效")]
    public AudioClip RobotDead;

    [Tooltip("人类受伤音效")]
    public AudioClip HumanGetHurt;

    [Tooltip("按钮点击")]
    public AudioClip ButtonClicked;

    [Tooltip("游戏结束音效")]
    public AudioClip GameOver;

    [Tooltip("人物倒下音效")]
    public AudioClip MagicCollapse;

    [Tooltip("隐身者死亡音效")]
    public AudioClip TheInvisibleDeath;
}
