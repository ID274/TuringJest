using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class KeyWordCipher : BaseCipher
{
    [SerializeField] private string keyWord;
    public override char Encrypt(char letter)
    {
        if (!CheckIfAlphabetLetter(letter))
        {
            return letter;
        }
        int index = System.Array.IndexOf(alphabet, letter);
        return key[index];
    }

    protected override void SetKey()
    {
        List<char> alphabetList = alphabet.ToList();
        char[] keywordChars = keyWord.ToCharArray();


        alphabetList.RemoveAll(letter => keyWord.Contains(letter));

        int count = 0;
        foreach (char letter in keywordChars)
        {
            alphabetList.Insert(count, letter);
            count++;
        }

        key = new string(alphabetList.ToArray());
        Debug.Log($"Key: {key}, {key.Length} letters");
    }

    public void SetKeyWord(string keyWord)
    {
        this.keyWord = keyWord;
    }
}