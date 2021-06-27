using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVarity
{
    public static List<List<int>> LevelWeather = new List<List<int>> {
        new List<int> {-1, 0, -1}
    };
    public static List<List<string>> LevelName = new List<List<string>> {
        new List<string> {"陌生的世界", "新手村", "初次的冒險"}
    };
    public static List<int> keyMax = new List<int> { 1, 3, };
    public static List<int> doorKey = new List<int> { 0, 2, };
    public static List<string> adteller = new List<string> { "世界の声", "", "","嚮導", };
    public static List<List<string>> chat = new List<List<string>> {
        new List<string>{
        "歡迎來到蘑菇村!!! \n你知道松茸跟蘑菇的差別嗎?他們從生物分類的\"科\"就有所不同呢! \n(松茸：マツタケ)",
        "一說到冒險,就會想到什麼?...... \n迷宮!!!傳說中這裡的迷宮是活的呢。"}
    };
    public static List<List<string>> advice = new List<List<string>> {
        new List<string> {
        "勇者啊,你是腦袋浸水還是沒腦袋? \n你沒看到這門上的鑰匙孔數嗎?你鑰匙不夠啊!!!",
        "勇者啊,你是眼睛脫窗還是沒眼睛? \n你沒看到佈告欄還有最新訊息嗎?凡事不要超之過急!!!"},
        new List<string> {""},new List<string> {""},
        new List<string> {
        "歡迎來到蘑菇村!!! \n你知道松茸跟蘑菇的差別嗎?他們從生物分類的\"科\"就有所不同呢! \n(松茸：マツタケ)",
        "一說到冒險,就會想到什麼?...... \n迷宮!!!傳說中這裡的迷宮是活的呢。"
        }
    };
    public static List<List<int>> littlech = new List<List<int>> {
        new List<int> {0,0,0,0,1,2,1,2},
        new List<int> {0,0,1,0,1,0,0,0,0}
    };
    public static List<List<string>> teller = new List<List<string>> {
        new List<string> {"世界の声","世界の声","世界の声","世界の声","我","魔龍王","我","魔龍王"},
        new List<string> {"世界の声","世界の声","我","世界の声","我","世界の声","世界の声","世界の声","世界の声"}
    };
    public static List<List<string>> story = new List<List<string>> {
        new List<string> {
        "勇者啊,運用你的智慧、力量以及運氣在這新的世界大鬧一翻吧!  \n(點擊以繼續)",
        "想要獲得更多角色嗎？想要的話可以全部給你，去找吧！ 我把轉生的方法都放在那裡了,就在最後一道門的後面。",
        "操作說明 : \n 移動鍵 : W-A-S-D \n 一技 : f （已解鎖）    大招 : q (未解鎖) \n 撿拾道具、開門 : g    (詳情可見暫停->說明)",
        "史萊姆的特性是\"彈性\",能夠以\"撞擊牆壁的方式進行近一步地跳躍(再按一次 W + (A or D))\"!",
        "??????",
        "...這只卑賤的史萊姆就是勇者嗎",
        "啊不...我只是來冒險的 \n (你又是誰啊!?)",
        "哼,想挑戰我就來吧。啊...多久沒人來挑戰我了。 \n 我等你,就在最後那道門前......"},
        new List<string> {
        "歡迎來到新手村第一層!在這裡你可以看到許多新的敵人、新的機關,也是個能讓你快速成長的好地方喔~",
        "在這裡請記住：「每當心情鬱悶的時候,用手托腮就好,手臂會因為幫上忙而開心的。」",
        "???   查理布朗?",
        "啊不是搞錯了,是：「心之所嚮,均歸虛空!」 \n 放開雙手去冒險吧!!!",
        "放開雙手要怎麼操作啊......",
        "小心草叢!",
        "謹慎行事是冒險者公會的第一守則!",
        "只有右邊這種紫色的特殊牆壁才能讓史萊姆二連跳喲,要記住!",
        "只有下面這種紫色的特殊牆壁才能讓史萊姆二連跳喲,要記住!"
        }
    };
    public static List<Vector2> spawnpoint = new List<Vector2> {
        new Vector2(5, 12), new Vector2(5, 60), new Vector2(54, 76)
    };
}
