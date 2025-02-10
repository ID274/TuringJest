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
    }

    public void SetCipherStrategy(CipherStrategy strategy)
    {
        cipherStrategy = strategy;
    }

    public void Encrypt(string textToEncrypt)
    {
        encryptedText = cipherStrategy.Encrypt(textToEncrypt);
        Debug.Log($"Encrypted Text: {encryptedText}");
    }

    public string GetEncryptedText()
    {
        return encryptedText;
    }
}