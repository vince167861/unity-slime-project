using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVarity
{
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
        new Vector2(5, 12), new Vector2(5, 40), new Vector2(1, 1)
    };
    public static List<Vector2> portalpoint = new List<Vector2> {
    	new Vector2(1,14),
    	new Vector2(14,11)
    };
    /// <summary>
    /// False is to left.
    /// </summary>
    public static List<List<bool>> enemyDirection = new List<List<bool>>
    {
        new List<bool> {false, true, false},
        new List<bool> {false, true, false, true, false}
    };
    public static List<List<Vector2>> enemyPos = new List<List<Vector2>>
    {
        new List<Vector2> {
            new Vector2(30, 1), new Vector2(1, 5), new Vector2(30, 9)
        },
        new List<Vector2> {
            new Vector2(30, 1), new Vector2(1, 3), new Vector2(30, 5), new Vector2(1, 7), new Vector2(30, 9)
        }
    };
    public static List<List<float>> enemySpawnRate = new List<List<float>>
    {
        new List<float> {10.0f, 10.0f, 10.0f}
    };
}
