using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "WordList", menuName = "ScriptableObjects/WordList")]
public class WordList : ScriptableObject
{
    [SerializeField]
    private List<string> words = new List<string>();

    public void Add(string _word)
    {
        if (words.Contains(_word, StringComparer.OrdinalIgnoreCase)) return;
        words.Add(_word);
    }

    public void Remove(string _word)
    {
        if (words.Contains(_word, StringComparer.OrdinalIgnoreCase)) words.Remove(_word);
    }

    public List<string> GetList()
    {
        return words;
    }

}
