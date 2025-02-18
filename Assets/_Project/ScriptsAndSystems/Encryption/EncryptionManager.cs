using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncryptionManager : MonoBehaviour
{
    public static EncryptionManager Instance { get; private set; }

    private CipherStrategy cipherStrategy;

    private string encryptedText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        cipherStrategy = GetComponent<CipherStrategy>();

        // temporary

        CaesarCipher caesarCipher = gameObject.AddComponent<CaesarCipher>();
        SetCipherStrategy(caesarCipher);
        Encrypt("HELLO, WORLD!");

        // temporary
    }

    public void SetCipherStrategy(BaseCipher strategy)
    {
        cipherStrategy.SetCipher(strategy);
    }

    public void Encrypt(string textToEncrypt)
    {
        encryptedText = cipherStrategy.Encrypt(textToEncrypt);
        Debug.Log($"Encrypted Text: {encryptedText}");
    }

    public string GetKey()
    {
        return cipherStrategy.GetKey();
    }

    public string GetEncryptedText()
    {
        return encryptedText;
    }
}