using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks", fileName = "Block-Name")]
public class Block : ScriptableObject
{
    public int Id;
    public int Count;

    public string Name;
    public string Description;

    public Sprite ImageBlock;
    public Sprite ImageFrame;
    public Sprite ImageFrameSelected;


    public Block Clone()
    {
        Block clone = new Block();

        clone.Id = Id;
        clone.Name = Name;
        clone.Description = Description;
        clone.ImageBlock = ImageBlock;
        clone.ImageFrame = ImageFrame;
        clone.ImageFrameSelected = ImageFrameSelected;

        return clone;
    }
}
