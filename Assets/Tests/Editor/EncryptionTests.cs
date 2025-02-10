using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EncryptionTests
{
    private GameObject encryptionManagerObject;
    private GameObject cipherHolderObject;
    private EncryptionManager encryptionManager;
    private CipherStrategy cipherStrategy;
    private BaseCipher cipher;

    private string textToEncrypt = "HELLO, WORLD!";

    [SetUp]
    public void Setup()
    {
        cipherHolderObject = new GameObject();
        encryptionManagerObject = new GameObject();
    }

    public void AddScripts()
    {
        encryptionManager = encryptionManagerObject.AddComponent<EncryptionManager>();
        cipherStrategy = encryptionManagerObject.AddComponent<CipherStrategy>();
    }

    public void Encrypt()
    {
        encryptionManager.SetCipherStrategy(cipherStrategy);
        cipherStrategy.SetCipher(cipher);
        encryptionManager.Encrypt(textToEncrypt);
    }

    [Test]
    public void CaesarCipherTest()
    {
        // Arrange

        cipher = cipherHolderObject.AddComponent<CaesarCipher>();

        AddScripts();

        // Act

        cipher.InitialiseCipher();
        Encrypt();
        string key = (cipher as CaesarCipher).GetKey();
        string expectedResult = GenerateExpectedText(key.ToCharArray());

        // Assert

        Assert.AreEqual(expectedResult, encryptionManager.GetEncryptedText());
    }

    [Test]
    public void SubstitutionCipherTestMirrored()
    {
        // Arrange

        SubstitutionCipher substitutionCipher = cipherHolderObject.AddComponent<SubstitutionCipher>();
        substitutionCipher.SetKeyType(SubstitutionKeyType.Atbash);
        cipher = substitutionCipher;

        AddScripts();

        // Act

        cipher.InitialiseCipher();
        Encrypt();
        string key = (cipher as SubstitutionCipher).GetKey();
        string expectedResult = GenerateExpectedText(key.ToCharArray());

        // Assert

        Assert.AreEqual(expectedResult, encryptionManager.GetEncryptedText());
    }

    [Test]
    public void SubstitutionCipherTestRandom()
    {
        // Arrange

        SubstitutionCipher substitutionCipher = cipherHolderObject.AddComponent<SubstitutionCipher>();
        substitutionCipher.SetKeyType(SubstitutionKeyType.Simple);
        substitutionCipher.InitializeShuffledAlphabet();
        cipher = substitutionCipher;

        AddScripts();

        // Act

        cipher.InitialiseCipher();
        Encrypt();
        string key = (cipher as SubstitutionCipher).GetKey();
        string expectedResult = GenerateExpectedText(key.ToCharArray());

        // Assert

        Assert.AreEqual(expectedResult, encryptionManager.GetEncryptedText());
    }

    private string GenerateExpectedText(char[] key)
    {
        string expectedText = "";

        foreach (char letter in textToEncrypt)
        {
            if (char.IsLetter(letter))
            {
                int index = letter - 'A';
                expectedText += key[index];
            }
            else
            {
                expectedText += letter;
            }
        }

        Debug.Log($"Expected text: {expectedText}");
        return expectedText;
    }
}