using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using Furniture;
using Room;

public class TilespriteController : MonoBehaviour
{
    public GameObject SampleTile;
    public int Width;
    public int Height;
    public List<CharacterData> characters;
    public List<FurnitureData> furnitures;
    public List<RoomData> rooms;
    // Start is called before the first frame update
    void Start()
    {
        SetupTiles(Width,Height);
    }

    //건설타일 생성
    void SetupTiles(int width, int height)
    {
        int x = 0;
        int y = 0;
        for (x = 0; x < width; x++)
        {
            for (y = 0; y < height; y++)
            {
                GameObject Tile = Instantiate(SampleTile, new Vector3(x, y), Quaternion.identity);
                Tile.name = "타일_"+ x +","+ y;
                Tile.transform.parent = this.transform;
            }
        }
    }
}
