using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubstitutionCipher : BaseCipher
{
    // The Substitution cipher replaces each letter in the alphabet with another letter.

    private char[] shuffledAlphabet;

    private SubstitutionKeyType keyType;

    private void Awake()
    {
        if (keyType == SubstitutionKeyType.Simple)
        {
            InitializeShuffledAlphabet();
        }
    }

    public void SetKeyType(SubstitutionKeyType keyType)
    {
        this.keyType = keyType;
    }

    public override char Encrypt(char letter)
    {
        if (!CheckIfAlphabetLetter(letter))
        {
            return letter;
        }
        else
        {
            if (keyType == SubstitutionKeyType.Atbash)
            {
                return EncryptMirrored(letter);
            }
            else
            {
                return EncryptRandom(letter);
            }
        }
    }

    public void InitializeShuffledAlphabet()
    {
        shuffledAlphabet = (char[])alphabet.Clone();
        FisherYatesShuffle(shuffledAlphabet);
    }

    private void FisherYatesShuffle(char[] array)
    {
        // using the Fisher-Yates shuffle algorithm to shuffle the array https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            char temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }

        SetKey(); // setting key here again to work with unit tests
    }

    private char EncryptRandom(char letter)
    {
        int index = System.Array.IndexOf(alphabet, letter);
        return shuffledAlphabet[index];
    }

    private char EncryptMirrored(char letter)
    {
        int index = System.Array.IndexOf(alphabet, letter);
        index = alphabet.Length - index - 1;
        return alphabet[index];
    }

    protected override void SetKey()
    {
        if (keyType == SubstitutionKeyType.Simple)
        {
            key = new string(shuffledAlphabet);
        }
        else
        {
            foreach (char letter in alphabet)
            {
                key += EncryptMirrored(letter);
            }
        }
        Debug.Log($"Key: {key}, {key.Length} letters");
    }
}