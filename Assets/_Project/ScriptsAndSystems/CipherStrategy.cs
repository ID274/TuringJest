using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CipherStrategy : MonoBehaviour
{
    // The CipherStrategy class is an example of the Strategy pattern. It is used to encrypt a given text using
    // a specific cipher.

    private BaseCipher cipher;

    public void SetCipher(BaseCipher cipher)
    {
        this.cipher = cipher;
    }

    public string Encrypt(string textToEncrypt)
    {
        string encryptedText = "";
        foreach (char letter in textToEncrypt)
        {
            char newLetter = cipher.Encrypt(letter);
            encryptedText += newLetter;
        }
        return encryptedText;
    }
}
