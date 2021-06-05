using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVarity
{
    public static List<int> keyMax = new List<int> { 1, 3, };
    public static List<int> doorKey = new List<int> { 0, 2, };
    public static List<string> adteller = new List<string> { "世界の声", "嚮導", };
    public static List<List<string>> advice = new List<List<string>> {
        new List<string> {
        "勇者啊,你是腦袋浸水還是沒腦袋? \n你沒看到這門上的鑰匙孔數嗎?你鑰匙不夠啊!!!",
        "勇者啊,你是眼睛脫窗還是沒眼睛? \n你沒看到佈告欄還有最新訊息嗎?凡事不要超之過急!!!"}
    };
    public static List<List<int>> littlech = new List<List<int>> {
        new List<int> {0,0,0,0,1,2,1,2}
    };
    public static List<List<string>> teller = new List<List<string>> {
        new List<string> {"世界の声","世界の声","世界の声","世界の声","我","魔龍王","我","魔龍王"}
    };
    public static List<List<string>> story = new List<List<string>> {
        new List<string> {
        "勇者啊,運用你的智慧、力量以及運氣在這新的世界大鬧一翻吧!  \n(點擊以繼續)",
        "想要成為更有用的角色嗎？想要的話可以全部給你，去找吧！ 我把轉生的方法都放在那裡了,就在最後一道門的後面。",
        "操作說明 : \n 移動鍵 : W-A-S-D \n 一技 : f （已解鎖）    大招 : q (未解鎖) \n 撿拾道具、開門 : g ",
        "史萊姆的特性是\"彈性\",能夠以\"撞擊牆壁的方式進行近一步地跳躍(再按一次 W + (A or D))\"!",
        "??????",
        "...這只卑賤的史萊姆就是勇者嗎",
        "啊不...我只是來冒險的 \n (而且我也不想當史萊姆啊~>_<)",
        "哼,想挑戰我就來吧。啊...多久沒人來挑戰我了。 \n 我等你,就在最後那道門前......"}
    };
    public static List<Vector2> spawnpoint = new List<Vector2> {
        new Vector2(5, 12), new Vector2(5, 60), new Vector2(1, 1)
    };
}
