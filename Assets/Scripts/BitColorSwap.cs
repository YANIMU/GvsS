using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BitColorSwap : MonoBehaviour
{
    Sprite sprite;
    Texture2D texture2D;
    SerializableDictionary<Color32, Color32> SwapColorDict = new();
    // Start is called before the first frame update
    void Start()
    {
        texture2D = sprite.texture;
        Vector2 xy = new(sprite.texture.width, sprite.texture.height);
        texture2D.GetPixels32().Select(x => SwapColor(x)).ToArray();
        //sprite = ;
    }

    // Update is called once per frame
    void Update()
    {
    }
    Color32 SwapColor(Color32 color)
    {
        if (SwapColorDict.ContainsKey(color))
        {
            return SwapColorDict[color];
        }
        else return color;
    }
}