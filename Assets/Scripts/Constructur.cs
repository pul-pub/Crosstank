using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Constructur : MonoBehaviour
{
    [SerializeField] private DataBase data;

    [SerializeField] private int countCellBlock;

    [SerializeField] private Object objectBlock;
    [SerializeField] private Transform parent;

    private BlockInventory _inventory;
    private SaveController _controller;

    private List<BlockConstructor> _blocks = new List<BlockConstructor>();
    private int _curentID = -1;

    private void Awake()
    {
        _inventory = GetComponent<BlockInventory>();
        _controller = GetComponent<SaveController>();
        AddGraphics();
        UpdateCells();
    }

    private void AddGraphics()
    {
        for (int i = 0; i < countCellBlock; i++)
        {
            GameObject _gObj = Instantiate(objectBlock, parent) as GameObject;
            _gObj.name = i.ToString();

            BlockConstructor _blockScript = new BlockConstructor();

            _blockScript.block = data.Blocks[0].Clone();
            _blockScript.isSelected = false;
            _blockScript.gObject = _gObj;
            
            Button _button = _gObj.GetComponent<Button>();
            RectTransform _rt = _gObj.GetComponent<RectTransform>();

            _rt.localPosition = new Vector3(0, 0, 0);
            _rt.localScale = new Vector3(1, 1, 1);
            _gObj.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            _blockScript.imgBlock = _rt.GetComponentsInChildren<Image>()[0];
            _blockScript.imgFrame = _rt.GetComponentsInChildren<Image>()[1];

            _button.onClick.AddListener(delegate { Select(int.Parse(_gObj.name)); });

            _blocks.Add(_blockScript);
        }
    }

    public void UpdateCells()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            _blocks[i].imgBlock.sprite = _blocks[i].block.ImageBlock;
            if (_blocks[i].isSelected )
            {
                _blocks[i].imgFrame.sprite = _blocks[i].block.ImageFrameSelected;
            }
            else
            {
                _blocks[i].imgFrame.sprite = _blocks[i].block.ImageFrame;
            }
        }
    }


    private void Select(int _id)
    {
        if (_curentID == -1)
        {
            _curentID = _id;
            _blocks[_id].isSelected = true;
            UpdateCells();
        }
        else
        {
            _blocks[_curentID].isSelected = false;
            _curentID = _id;
            _blocks[_id].isSelected = true;
            UpdateCells();
        }
    }

    public void SwitchBlock()
    {
        if (_curentID != -1) 
        {
            foreach (Block block in data.Blocks) 
            {
                if (block.Id == StaticData.saveData.IdBlocks[_inventory._currentID] && StaticData.saveData.CountBlocks[_inventory._currentID] > 0)
                {
                    _blocks[_curentID].block = block.Clone();
                    StaticData.saveData.CountBlocks[_inventory._currentID] -= 1;
                    UpdateCells();
                }
            }
        }
    }

    public void DeliteBlock()
    {
        if (_curentID != -1)
        {
            for (int i = 0; i < StaticData.saveData.IdBlocks.Length; i++)
            {
                if (_blocks[_curentID].block.Id == StaticData.saveData.IdBlocks[_inventory._currentID])
                {
                    StaticData.saveData.CountBlocks[i] += 1;
                    _blocks[_curentID].block = data.Blocks[0]; 
                    UpdateCells();
                }
            }
        }
    }
}
