using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; //요고 추가해주고
using System;

namespace OdinInspectorSample
{
    public class Test
    {
        [PreviewField(50, ObjectFieldAlignment.Right)]
        public Texture icon;
        [PreviewField(50, ObjectFieldAlignment.Center)]
        public Sprite image;
        public string name;
        [VerticalGroup("Info")]
        public int age;
        [VerticalGroup("Info")]
        public float weight;
        [VerticalGroup("Info")]
        public float height;
    }
    public class OdinSample : SerializedMonoBehaviour //요고 바까주고
    {
        [Title("Sample")]
        [InfoBox("Simple Example")]
        [DetailedInfoBox("Detail Example", "이 페이지는 접었다 열었다 할 수 있습니다.\n잘 이용해보세요.")]
        public int a = 0;

        [Title("Blank Between Two Value in Inspector")]
        [Space]
        public int b = 0;
        [PropertySpace(10.0f, 10.0f)]
        public int c = 0;

        [Title("Readonly")]
        [ReadOnly]
        public string _a = "고쳐보세요. 안될걸~";

        [Title("Dropdown")]
        [ValueDropdown("DropDownNum")]
        public int d;
        private int[] DropDownNum = new int[] { -1, 0, 1 };

        [Title("Search")]
        [Searchable]
        public List<string> itemList = new List<string>()
        {
            "sword", "arrow", "shield"
        };

        [Title("Enum Utility")]
        public enum Weapon
        {
            sword,
            arrow,
            shield
        };
        [EnumPaging]
        public Weapon w;
        [EnumToggleButtons]
        public Weapon _w;

        [Title("Table")]
        [TableList]
        public List<Test> t = new List<Test>();

        [Title("More about info box")]
        [InfoBox("조심해야할 것 같은 노란 느낌표", InfoMessageType.Warning)]
        [InfoBox("뭔가 틀린거 같은 빨간 느낌표", InfoMessageType.Error)]
        [InfoBox("뭔가 허전한 정보박스", InfoMessageType.None)]
        public bool toggle;
        [InfoBox("토글을 활성화해보세요")]
        [InfoBox("짜자잔", "toggle")]
        public bool aa; //인포박스만 남겨둘 수 없구나
    }
}
