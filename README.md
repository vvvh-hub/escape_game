# Escape项目介绍

本项目(Escape)是2021级软开大作业，是一款弹幕射击游戏。主要任务是操控主角麦吉柯逃出监狱。开发组人员有六个，他们分担着策划美术音乐程序的任务。本仓库用于管理项目。

# Unity版本

Unity 2021.3.15f1c1

# 工作流程

+ 如果第一次打开此仓库，那么：

  首先，使用以下命令，将仓库克隆到本地

  ```shell
  git clone https://github.com/EscapePro/Escape.git
  ```

  然后在该仓库进行修改

+ 如果不是第一次，也建议工作开始前拉取最新版本的仓库，使用:

  ```shell
  git pull
  ```

  然后进行修改

当修改完成后，使用

```shell
git add .
```

增加文件内容，注意：这里由于`.gitignore`的存在不会添加不必要的文件，但是如果你自己在外层添加了不必要的文件，请确保上传的只有`Assets`和`ProjectSettings`文件夹

```shell
git commit -m "这里填这次修改的内容"
```

来记录修改，然后使用

```shell
git push
```

将修改同步到远程。

<font size=4 color=red>注意：push前确保自己的修改已经完成请正确，并确保没有和另一个人一起push!</font>

# 每个职位的注意事项

### 美术

美术同学请在`Assets/Sprites/`里存放自己的美术作品，若要在其中增加文件夹，请确保命名合理。

也可以直接保存为预制件，此时请将预制件放入`Assets/Prefabs/`，若有冲突，请联系管理者。

其余的同学在制作其他部分时，可以先自己找素材，之后再进行修改。

### 文本

<font size=4 color=red>注意：目前处于开发早期，如果觉得麻烦可以先把内容写纸上，以后可能会有更优方案</font>

对话信息存放于`Resources/Dialog/dialog.json`，其格式类似于

```json
[
    "id: 0",
    "Escape: 亻尔女子",
    "Science: 我们来帮助你出狱",
    {
        "choice1": [
            "id: 1",
            "Magic: OK",
            "jumpto: 4"
        ],
        "choice2": [
            "id: 2",
            "Magic: OK",
            "jumpto: 4"
        ],
        "choice3": [
            "id: 3",
            "Magic: OK",
            "jumpto: 4"
        ]
    },
    "id: 4",
    "Escape: 那走吧"
]
```

`[]`内表示对话内容，对话一开始必须有一个`id: [num]`来表示这是第几个对话，每一句对话前必须加上说话者的名字。可以在对话结束使用`"jumpto: [num]"`来表示跳到第`num`段对话。`{}`表示选项，最多表示四个选项，选项中的键表示了选项要游戏UI里展示的样子，值是用`[]`包裹起来的对话内容，规则和上面一样，例如:

```json
{
    "choice1": [
        "id: 1",
        "Magic: OK",
        "jumpto: 4"
    ]
}
```

表示UI中会显示"choice1"这个选项，当这个选项被选中，接下来Magic会说"OK"，然后对话挑战至对话ID为4处。

### 音乐和音效

音乐和音效统一使用网上的素材。

### 编码

1. 禁止使用`new`以及`Destroy`，请使用`ObjectPool.Instance.GetObject`与`ObjectPool.Instance.Push`，如何使用请查看注释。
2. 请用四格缩进
3. 写下的每一个函数都必须有详尽得当的注释
4. 如果有哪里不知道怎么起名字了，可以使用中文命名，但是请注意：`public`函数请使用英文，其他无限制
5. 所有需要人为配置的字段均设为`public`
6. 有不懂的地方请积极在群里交流！

### 测试

找出的bug必须有截图，并且描述复现该bug的方法。
