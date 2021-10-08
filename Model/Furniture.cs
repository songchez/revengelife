using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Furniture
{

[System.Serializable]
    public class FurnitureData{
        enum f_Type
        {
            Wall, Door, Barricade
        }
        public int cost = 0; //설치비용
        public int defense = 0;//방어력
        public Sprite sprite = null;//이미지
        public int level = 1;//레벨
        
    }
}
