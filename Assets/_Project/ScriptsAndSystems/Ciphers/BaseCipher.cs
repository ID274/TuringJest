using System.Linq;
using UnityEngine;

public abstract class BaseCipher : MonoBehaviour
{
    [SerializeField] private string cipherName;
    protected string key = "";

    protected char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public abstract char Encrypt(char letter);

    public string ReturnName()
    {
        return cipherName;
    }

    protected bool CheckIfAlphabetLetter(char letter)
    {
        if (!alphabet.Contains(letter))
        {
            Debug.Log($"Processed letter {letter} is not in the alphabet");
            return false;
        }
        else
        {
            return true;
        }
    }

    public void InitialiseCipher()
    {
        SetKey();
    }

    protected abstract void SetKey();

    public string GetKey() 
    {
        return key;
    }
}