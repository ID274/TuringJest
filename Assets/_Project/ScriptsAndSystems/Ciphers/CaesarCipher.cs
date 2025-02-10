using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaesarCipher : BaseCipher
{
    // The Caesar cipher shifts the alphabet by a fixed amount. For example - if the shift is 3,
    // then A becomes D and so on.

    public int shiftAmount = 3;
    public override char Encrypt(char letter)
    {
        if (!CheckIfAlphabetLetter(letter))
        {
            return letter;
        }
        else
        {
            int index = System.Array.IndexOf(alphabet, letter);
            index = (index + shiftAmount) % alphabet.Length;
            return alphabet[index];
        }
    }

    protected override void SetKey()
    {
        char[] newKey = new char[alphabet.Length];

        for (int i = 0; i < alphabet.Length; i++)
        {
            int shiftedIndex = (i + shiftAmount) % alphabet.Length;
            newKey[i] = alphabet[shiftedIndex];
        }

        key = new string(newKey);
        Debug.Log($"Key: {key}, {key.Length} letters");
    }

    public override string GetKey()
    {
        return key;
    }
}