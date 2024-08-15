using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlockInventory : MonoBehaviour
{
    [SerializeField] private DataBase data;

    [SerializeField] private TextMeshProUGUI textBlock;
    [SerializeField] private Image imgBlock;
    [SerializeField] private Transform trBlock;

    public int _currentID = 0;

    private void Update()
    {
        textBlock.text = StaticData.saveData.CountBlocks[_currentID].ToString();
    }

    public void ToLeftBlock()
    {
        if (_currentID == 0)
        {
            StartCoroutine(NoBlock(Vector2.left));
        }
        else
        {
            _currentID -= 1;
            StartCoroutine(ToBlock(Vector2.left));
        }
    }

    public void ToRightBlock()
    {
        if (_currentID == StaticData.saveData.IdBlocks.Length)
        {
            StartCoroutine(NoBlock(Vector2.right));
        }
        else
        {
            _currentID += 1;
            StartCoroutine(ToBlock(Vector2.right));
        }
    }

    IEnumerator NoBlock(Vector2 _vec)
    {
        while ((trBlock.localPosition.x < 50 && _vec.x == Vector2.left.x) || (trBlock.localPosition.x >= -50 && _vec.x == Vector2.right.x))
        {
            trBlock.localPosition += new Vector3(-_vec.x * 15, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        
        while ((trBlock.localPosition.x >= 0 && _vec.x == Vector2.left.x) || (trBlock.localPosition.x <= 0 && _vec.x == Vector2.right.x))
        {
            trBlock.localPosition -= new Vector3(-_vec.x * 15, 0, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ToBlock(Vector2 _vec)
    {
        while ((trBlock.localPosition.x <= 2000 && _vec.x == Vector2.left.x) || (trBlock.localPosition.x >= -2000 && _vec.x == Vector2.right.x))
        {
            trBlock.localPosition += new Vector3(-_vec.x * 50, 0, 0);
            yield return new WaitForEndOfFrame();
        }

        foreach (Block b in data.Blocks)
        {
            if (b.Id == StaticData.saveData.IdBlocks[_currentID])
            {
                imgBlock.sprite = b.ImageBlock;
                textBlock.text = StaticData.saveData.CountBlocks[_currentID].ToString();
            }
        }
        if (_vec.x == Vector2.right.x) 
            trBlock.localPosition = new Vector3(2000, trBlock.localPosition.y, 0);
        else
            trBlock.localPosition = new Vector3(-2000, trBlock.localPosition.y, 0);

        while ((trBlock.localPosition.x <= 0 && _vec.x == Vector2.left.x) || (trBlock.localPosition.x >= 0 && _vec.x == Vector2.right.x))
        {
            trBlock.localPosition += new Vector3(-_vec.x * 50, 0, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
