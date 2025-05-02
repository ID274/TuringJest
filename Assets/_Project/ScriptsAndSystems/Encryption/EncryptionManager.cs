using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CipherStrategy))]
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

    public void AddCipherStrategyClass(CipherStrategy cipherStrategy) // for some reason cipher strategy reference was null? so we will add it as a parameter
    {
        if (cipherStrategy == null)
        {
            Debug.LogError($"Cipher strategy is null.");
            return;
        }
        this.cipherStrategy = cipherStrategy;
    }

    public void SetCipherStrategy(BaseCipher strategy)
    {
        if (strategy == null)
        {
            Debug.LogError($"Cipher is null");
            return;
        }
        cipherStrategy.SetCipher(strategy);
    }

    public void Encrypt(string textToEncrypt)
    {
        encryptedText = cipherStrategy?.Encrypt(textToEncrypt);
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