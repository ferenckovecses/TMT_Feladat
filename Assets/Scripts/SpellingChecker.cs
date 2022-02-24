using SpellingCorrector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class SpellingChecker : MonoBehaviour
{
    [Header("Data Model")]
    public WordList characters;
    public WordList places;
    public WordList items;
    public WordList misc;
    public WordList action;

    [Header("Object references")]
    public TMP_Text wordPrefab;   
    public TMP_Text result;
    public TMP_InputField sentenceField;
    public GameObject characterField;
    public TMP_InputField characterInput;
    public GameObject locationField;
    public TMP_InputField locationInput;
    public GameObject itemField;
    public TMP_InputField itemInput;
    public GameObject actionField;
    public TMP_InputField actionInput;

    private Spelling spelling;
    
    // Start is called before the first frame update
    void Start()
    {
        SpellingInit();

        DisplayWords(characters.GetList(), characterField);
        DisplayWords(places.GetList(), locationField);
        DisplayWords(items.GetList(), itemField);
        DisplayWords(action.GetList(), actionField);

    }

    void SpellingInit()
    {
        if (characters == null || places == null || items == null || misc == null || action == null) { Debug.Log("Not all wordlist set!"); return; }

        List<string> temp = characters.GetList().Union(places.GetList()).Union(items.GetList()).Union(misc.GetList()).Union(action.GetList()).ToList();
        spelling = new Spelling(temp);
    }

    void DisplayWords(List<string> words, GameObject parent)
    {
        foreach (string word in words)
        {
            TMP_Text textObject = Instantiate(wordPrefab);
            textObject.text = word;
            textObject.transform.SetParent(parent.transform);
        }
    }

    public void AddToCharacters()
    {
        if (characterInput.text.Length == 0) return;
        characters.Add(characterInput.text);
        TMP_Text textObject = Instantiate(wordPrefab);
        textObject.text = characterInput.text;
        textObject.transform.SetParent(characterField.transform);
        characterInput.text = "";
        SpellingInit();
    }

    public void AddToLocations()
    {
        if (locationInput.text.Length == 0) return;
        characters.Add(locationInput.text);
        TMP_Text textObject = Instantiate(wordPrefab);
        textObject.text = locationInput.text;
        textObject.transform.SetParent(locationField.transform);
        locationInput.text = "";
        SpellingInit();
    }

    public void AddToItems()
    {
        if (itemInput.text.Length == 0) return;
        characters.Add(itemInput.text);
        TMP_Text textObject = Instantiate(wordPrefab);
        textObject.text = itemInput.text;
        textObject.transform.SetParent(itemField.transform);
        itemInput.text = "";
        SpellingInit();
    }

    public void AddToActions()
    {
        if (actionInput.text.Length == 0) return;
        characters.Add(actionInput.text);
        TMP_Text textObject = Instantiate(wordPrefab);
        textObject.text = actionInput.text;
        textObject.transform.SetParent(actionField.transform);
        actionInput.text = "";
        SpellingInit();
    }

    public void Check()
    {
        if (sentenceField.text.Length == 0) return;
        string input = sentenceField.text;
        sentenceField.text = "";
        List<string> words = GetWords(input);
        string sentence = "Result: ";
        foreach(string word in words)
        {
            sentence = sentence + " " + spelling.Correct(word);
        }
        sentence = sentence + ".";

        result.text = sentence;
    }

    static List<string> GetWords(string input)
    {
        MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

        var words = from m in matches.Cast<Match>()
                    where !string.IsNullOrEmpty(m.Value)
                    select TrimSuffix(m.Value);

        return words.ToList();
    }

    static string TrimSuffix(string word)
    {
        int apostropheLocation = word.IndexOf('\'');
        if (apostropheLocation != -1)
        {
            word = word.Substring(0, apostropheLocation);
        }

        return word;
    }
}
