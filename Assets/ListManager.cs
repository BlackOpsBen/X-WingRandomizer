using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class ListManager : ScriptableObject
{
    [SerializeField] private bool updateList;

    public ObjectList[] objectLists;

    private void OnValidate()
    {
        updateList = false;

        string pathPrefix = "Assets/Resources/";

        string[] objectDirectories = Directory.GetDirectories(pathPrefix + this.name);

        for (int i = 0; i < objectDirectories.Length; i++)
        {
            objectDirectories[i] = objectDirectories[i].Replace(pathPrefix, string.Empty);
            objectDirectories[i] = objectDirectories[i].Replace("\\", "/");
        }


        objectLists = new ObjectList[objectDirectories.Length];

        for (int i = 0; i < objectLists.Length; i++)
        {
            objectLists[i] = new ObjectList();
            objectLists[i].name = objectDirectories[i].Replace(this.name + "/", string.Empty);

            objectLists[i].objects = Resources.LoadAll<Ship>(objectDirectories[i]);
        }
    }
}

[System.Serializable]
public class ObjectList
{
    public string name;
    public ScriptableObject[] objects;
}