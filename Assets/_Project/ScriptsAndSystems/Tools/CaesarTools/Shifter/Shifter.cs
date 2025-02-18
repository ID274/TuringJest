using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Linq;
using Codice.Client.Common.GameUI;

public class Shifter : MonoBehaviour, ICipherTool
{
    string key = "";
    [SerializeField] private GameObject shifterObjectPrefab;
    [SerializeField] private Transform shifterIndexHolder;

    private string[] shifterArray;
    private TextMeshProUGUI[] textMeshProObjects;

    [SerializeField] private TextMeshProUGUI shiftCountText;
    private int shiftCount = 0;

    // temporary

    private void OnEnable()
    {
        TestMethod();
    }

    public void TestMethod()
    {
        Debug.LogWarning("Test method called", this);
        Equip();
    }

    //temporary

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
        Debug.LogWarning($"Key length: {key.Length}. Key: {key}.");
        shifterArray = new string[key.Length];
        textMeshProObjects = new TextMeshProUGUI[shifterArray.Length];

        for (int i = 0; i < key.Length; i++)
        {
            string letter = key[i].ToString();
            Debug.Log($"Letter being added: '{letter}'");
            if (!string.IsNullOrEmpty(letter))
            {
                (string, TextMeshProUGUI) references = AddLetterToShifter(letter);
                shifterArray[i] = references.Item1;
                textMeshProObjects[i] = references.Item2;
            }
        }
    }

    private (string, TextMeshProUGUI) AddLetterToShifter(string letter)
    {
        GameObject shifterObject = Instantiate(shifterObjectPrefab, shifterIndexHolder);
        TextMeshProUGUI shifterText = shifterObject.GetComponentInChildren<TextMeshProUGUI>();
        shifterText.text = letter;
        return (letter, shifterText);
    }

    public void Shift(bool left)
    {
        int ProcessIndex(int index, int maxIndex)
        {
            if (index > maxIndex)
            {
                return 0;
            }

            if (index < 0)
            {
                return maxIndex;
            }

            return index;
        }

        int num = left ? -1 : 1;

        string[] tempArray = new string[shifterArray.Length];

        Array.Copy(shifterArray, tempArray, tempArray.Length);

        for (int i = 0; i < tempArray.Length; i++)
        {
            // shift the array to either left or right, and wrap around to the other end

            int processedIndex = ProcessIndex(i + num, shifterArray.Length - 1);
            shifterArray[i] = tempArray[processedIndex];
            textMeshProObjects[i].text = shifterArray[i];
        }

        shiftCount += num;

        if (shiftCount == shifterArray.Length || shiftCount == -shifterArray.Length)
        {
            shiftCount = 0;
        }

        shiftCountText.text = shiftCount.ToString();
    }
}