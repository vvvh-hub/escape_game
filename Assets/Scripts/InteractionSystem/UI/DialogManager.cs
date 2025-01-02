using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager: MonoBehaviour {
    public static DialogManager Instance;

    public Transform spider;
    public Transform magic;

    List<(string name, string content, int id)> dialogs = new List<(string name, string content, int id)> {
        ("麦吉柯","这魔力抑制装置的效果是真的好啊，真的一点魔法都用不出来啊……", 0),
        ("麦吉柯","当时做实验的时候根据我的计算按理来说应该是完全不会出现能量外泄的，出现能量泄漏只有两种可能，有人动了手脚，否则就是……", 0),
        ("麦吉柯", "至今的魔法理论中存在着致命的疏漏，如果是第二种情况的话……我不敢想象以后会出现什么后果。", 0),
        ("麦吉柯", "算了，也许就是我自己操作出现了失误吧，也不知道赛娅丝那之后怎么样了……", 0),
        ("麦吉柯", "外面怎么回事？", 0)
    };

    List<(string name, string content, int id)> seconddialogs = new List<(string name, string content, int id)> {
        ("？？？", "麦吉…柯，我和依思来救你了", 3),
        ("麦吉柯", "这是赛娅丝的声音！", 0),
        ("麦吉柯", "赛娅丝！你们在哪？！", 0),
        ("赛娅丝", "我们在监狱…外围，现在还没办法进入，你带上这…只蜘蛛，跟随我们的指…示行动！", 1),
        ("麦吉柯", "什么意思，这是要越狱？", 0),
        ("依思", "麦吉柯，那你说还能怎…么出去呀，", 2),
        ("依思", "不能我们像小说里…一样带着千军万马大张旗鼓冲进监狱把他们全部打趴下…然后把你抱出来吧哈哈哈~", 2),
        ("麦吉柯", "依思，这么久不见了你还是这么‘伶牙俐齿’啊！", 0),
        ("赛娅思", "你们俩先别…斗嘴了，你们当这是在学校啊，时间紧迫，先带麦吉柯出来…才是最重要的。", 1),
        ("麦吉柯", "你们确定要越狱？我跟你们说，这监狱里的戒备可不是一般的森严，虽然现在最难处理的典狱长没在，但是也不好折腾啊！", 0),
        ("依思", "你放心好了，从你被关起来…之后我们就开始秘密对监狱里的结构和守备各…个方面进行了全方位的研究，", 2),
        ("依思", "我们跟你可不一样，不会带着同伴做那…么危险还的事情咯咯咯……你就放心按照我们的指示走就行了，而且我们做的这个蜘蛛可以实时…观测里面的情况的，", 2),
        ("依思", "我们还给它上了一些空间魔法，里面储存了一些道具，我看见你藏的那些东西了，你也可以把它们交给蜘蛛然后随用随取，", 2),
        ("依思", "你要知道目前的魔力抑制装置并没有考虑到也限制不了这种我们自己研究的施加在物体上的魔法，我们做这个蜘蛛可是花了大功夫的，它……", 2),
        ("麦吉柯", "停，不用介绍这个蜘蛛了，这种高科技东西我也听不懂，而且越狱怎么就不算危险的事情了……", 0),
        ("麦吉柯", "既然说到这份上了，那行吧，我跟你们走！", 0),
        ("依思", "对嘛对嘛，早这么说不就行了，直接跟我们走就行了，我们你还不信嘛！", 2),
        ("赛娅思", "行了依思，别说这么多了。麦吉柯，我们先跟你说一下大概的计划。", 1),
        ("赛娅思", "你先从蜘蛛中取出空间置换仪，这个仪器可以使你自身所在的空间区域和门外的空间区域互换位置，你只需要把手放在上面闭上眼，过一会睁开眼你就会发现你已经在监狱门外了，", 1),
        ("赛娅思", "当然空间置换这个过程可能会很晕，你稍微忍一忍。", 1),
        ("赛娅思", "机械蜘蛛里面有许多我们准备的道具，你可以自己查看列表，我就不多介绍了。", 1),
        ("赛娅思", "当然你也知道监狱中有很多巡逻的普通员工和机器人，以及一些隐形者，还有很多魔力抑制装置，不过我们已经摸清了，", 1),
        ("赛娅思", "监狱中的魔力抑制装置大部分都是壁挂式的，少数员工可能有便携式的，魔力抑制装置很脆弱，甚至只需要轻轻砸一下就会失效，破坏之后你就可以使用魔法了，", 1),
        ("赛娅思", "这就是你的专长了，就不需要我教了。", 1),
        ("赛娅思", "你需要做的就是用这些道具来对付那些员工和机器人，不过你肯定不能从监狱大门出去，监狱其实是有个通往外面的暗门的，不过暗门上面有三把锁，", 1),
        ("赛娅思", "你得去找到对应的三把不同的钥匙来开门，暗门的位置和钥匙的位置我们已经在机械蜘蛛自带的地图中给你标好了。", 1),
        ("赛娅思", "我们现在在监狱外面的隔墙附近，这周围也有很多警卫在巡逻，很危险，我们会时刻看着你的行动，但是恐怕不能随时跟你保持联系，", 1),
        ("赛娅思", "不过我们会在必要时刻提醒你怎么行动，主要行动还是要靠你自己。", 1),
        ("赛娅思", "我们在监狱外会合，一定要平安逃出来，我们都很想你。", 1),
        ("赛娅思", "哦对，崔尔德校长也知道这次行动，你回去之后他还有事要找你。", 1),
        ("赛娅思", "还有件很重要的事，务必小心监狱中的死亡之井，千万不能触碰它，这是崔尔德校长给的额外提醒，一定要小心！", 1),
        ("赛娅思", "好，现在开始行动吧！", 1),
        ("麦吉柯", "看来那次的事情，果然没那么简单呢……不过还是先逃出去再说吧。", 0),
        ("麦吉柯", "我们果然都是疯狂的人啊~", 0)
    };

    IEnumerator<(string name, string content, int id)> firstEnumerator;
    IEnumerator<(string name, string content, int id)> secondEnumerator;

    [Tooltip("显示主角Magic的UI控件")]
    public Image magicImage;

    [Tooltip("显示配角的UI控件")]
    public Image npcImage;

    [Tooltip("名字")]
    public TMP_Text characterName;

    [TooltipAttribute("内容")]
    public TMP_Text content;

    [Tooltip("是否完成初始对话，完成才让传送")]
    public static bool dialogCompleted = false;

    public static bool first = true;

    private void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        gameObject.SetActive(false);
        firstEnumerator = dialogs.GetEnumerator();
        secondEnumerator = seconddialogs.GetEnumerator();
        npcImage.gameObject.SetActive(false);
        magicImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (first) {
                NextDialog(firstEnumerator);
            } else {
                NextDialog(secondEnumerator);
            }
        }
    }

    /// <summary>
    /// 展示对话UI的方法，调用后会展示对话UI
    /// 同时禁止Magic移动以及释放技能
    /// </summary>
    public void ShowDialog() {
        GameManager.Instance.Forbid();
        gameObject.SetActive(true);
        if (first) {
            NextDialog(firstEnumerator);
        } else {
            NextDialog(secondEnumerator);
        }
    }

    /// <summary>
    /// 根据ID更改对话方的颜色与sprite
    /// </summary>
    /// <param name="id"></param>
    public void ChangeMainImage(int id) {
        switch (id) {
            case 0:
                magicImage.gameObject.SetActive(true);
                magicImage.color = Color.white;
                npcImage.color = new Color(121 / 255f, 121 / 255f, 121 / 255f);
                break;
            case 1:
            case 2:
                magicImage.color = new Color(121 / 255f, 121 / 255f, 121 / 255f);
                npcImage.gameObject.SetActive(true);
                npcImage.color = Color.white;
                break;
            case 3:
                npcImage.gameObject.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// 推进下一句的方法，调用后对话推进到下一句
    /// 当结束时会让对话UI消失
    /// </summary>
    public void NextDialog(IEnumerator<(string name, string content, int id)> enumerator) {
        if (enumerator.MoveNext()) {
            characterName.SetText(enumerator.Current.name);
            content.SetText(enumerator.Current.content);
            int id = enumerator.Current.id;
            ChangeMainImage(id);
        } else {
            gameObject.SetActive(false);
            if (first) {
                FollowSpider();
                Invoke(nameof(FollowMagic), 2);
            } else {
                GameManager.Instance.ChangeOrthSize(10f);
                dialogCompleted = true;
            }

            GameManager.Instance.Allow();
            first = false;
        }
    }

    void FollowMagic() {
        GameManager.Instance.Follow(magic);
        GameManager.Instance.ChangeOrthSize(10f);
    }

    void FollowSpider() {
        GameManager.Instance.Follow(spider);
    }
}
