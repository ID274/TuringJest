using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shifter : MonoBehaviour
{
    string key = "";
    [SerializeField] private GameObject shifterIndexPrefab;
    [SerializeField] private Transform shifterIndexHolder;

    private int[] originalOrder;
    private (GameObject, int)[] shifterObjects;

    private void Start()
    {
        TestMethod();
    }

    public void TestMethod()
    {
        Debug.LogWarning("Test method called", this);
        Equip();
    }

    public void Equip()
    {
        if (SetKey())
        {
            PopulateShifter();
        }
    }

    private bool SetKey()
    {
        key = EncryptionManager.Instance.GetKey();

        if (key != "" && key != null && key.Length > 0)
        {
            return true;
        }
        else
        {
            Debug.LogError("Key is empty or null", this);
            return false;
        }
    }

    private void PopulateShifter()
    {
        shifterObjects = new (GameObject, int)[key.Length];
        originalOrder = new int[key.Length];
        Debug.Log("Key: " + key);
        for (int i = 0; i < key.Length; i++)
        {
            GameObject shifter = Instantiate(shifterIndexPrefab, shifterIndexHolder);
            shifter.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
            shifter.transform.parent = shifterIndexHolder;
            shifterObjects[i] = (shifter, i);
            originalOrder[i] = i;
        }
    }

    public void Shift(bool left)
    {
        if (shifterObjects == null || shifterObjects.Length == 0) return;

        int length = shifterObjects.Length;
        (GameObject, int)[] newOrder = new (GameObject, int)[length];

        if (left)
        {
            for (int i = 0; i < length - 1; i++)
            {
                newOrder[i] = shifterObjects[i + 1];
            }
            newOrder[length - 1] = shifterObjects[0];
        }
        else
        {
            for (int i = 1; i < length; i++)
            {
                newOrder[i] = shifterObjects[i - 1];
            }
            newOrder[0] = shifterObjects[length - 1];
        }

        shifterObjects = newOrder;

        for (int i = 0; i < length; i++)
        {
            shifterObjects[i].Item1.transform.SetSiblingIndex(i);
        }
    }
}