using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Shifter : MonoBehaviour, ICipherTool
{
    string key = "";
    [SerializeField] private GameObject shifterObjectPrefab;
    [SerializeField] private Transform shifterIndexHolder;

    [SerializeField] private string[] shifterArray;
    [SerializeField] private TextMeshProUGUI[] textMeshProObjects;

    private void OnEnable()
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
        int num = left ? -1 : 1;
        string[] tempArray = shifterArray;

        for (int i = 0; i < shifterArray.Length; i++) 
        {
            if (i != 0 && i != shifterArray.Length - 1)
            {
                shifterArray[i] = tempArray[i + num];
            }
            else
            {
                shifterArray[i] = tempArray[0];
            }

            textMeshProObjects[i].text = shifterArray[i];
        }
    }
}