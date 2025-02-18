using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CipherStrategy : MonoBehaviour
{
    // The CipherStrategy class is an example of the Strategy pattern. It is used to encrypt a given text using
    // a specific cipher.

    // It is also an demonstration of the Dependency Inversion Principle (DIP) due to it's SetCipher class that takes it's reference from a higher level module (in
    // this case the EncryptionManager). It also showcases the Liskov's Substitution Principle due to the use of an abstraction rather than a concrete implementation
    // of the cipher used.

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

    public string GetKey()
    {
        return cipher.GetKey();
    }
}
