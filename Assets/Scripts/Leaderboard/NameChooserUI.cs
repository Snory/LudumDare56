using System;
using TMPro;
using UnityEngine;

public class NameChooserUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.GameObject _errorTextObject;

    [SerializeField]
    private TMP_InputField _nameText;

    [SerializeField]
    private GeneralEvent _addPlayerEvent, _switchSceneEvent;

    private const string _lastUsedNameKey = "LastUsedName";

    public void Start()
    {
        var lastUsedName = GetLastUsedName();
        if (lastUsedName is not null)
        {
            Debug.Log("Last used name: " + lastUsedName);
            _nameText.text = lastUsedName;
        }
    }

    public void StoreName()
    {
        StoreLastUsedName(_nameText.text);

        if (NameExists(_nameText.text))
        {
            _switchSceneEvent.Raise();
            return;
        }

        _addPlayerEvent.Raise(new AddPlayerEventArgs(new PlayerData { Username = _nameText.text }));
    }

    public void StoreLastUsedName(string name)
    {
        PlayerPrefs.SetString(_lastUsedNameKey, name);
    }

    public string GetLastUsedName()
    {
        if(!PlayerPrefs.HasKey(_lastUsedNameKey))
        {
            Debug.Log("No last used name found");
            return null;
        }

        return PlayerPrefs.GetString(_lastUsedNameKey);
    }

    public void OnPlayerAdded(EventArgs eventArgs)
    {
        PlayerAddedEventArgs playerAddEventArgs = (PlayerAddedEventArgs)eventArgs;

        if (playerAddEventArgs.Success)
        {
            StoreName(_nameText.text);
            _errorTextObject.SetActive(false);
            _switchSceneEvent.Raise();
        }
        else
        {
            _errorTextObject.SetActive(true);
        }
    }

    public void StoreName(string name)
    {
        if (!PlayerPrefs.HasKey("PlayerNames"))
        {
            PlayerPrefs.SetString("PlayerNames", name);
            return;
        }

        // Get the existing names from PlayerPrefs
        string existingNames = PlayerPrefs.GetString("PlayerNames");

        // Append the new name to the existing names
        string updatedNames = existingNames + "," + name;

        // Store the updated names in PlayerPrefs
        PlayerPrefs.SetString("PlayerNames", updatedNames);

        PlayerPrefs.Save();
    }

    public bool NameExists(string name)
    {
        if (!PlayerPrefs.HasKey("PlayerNames"))
        {
            return false;
        }

        // Get the existing names from PlayerPrefs
        string existingNames = PlayerPrefs.GetString("PlayerNames");

        // Split the names by comma
        string[] names = existingNames.Split(',');

        return Array.Exists(names, n => n == name);
    }
}