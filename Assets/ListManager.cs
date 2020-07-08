using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class ListManager : ScriptableObject
{
    [SerializeField] private bool updateList;

    public ObjectList[] objectLists;

    public ScriptableObject[] completeList;

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

        completeList = PutAllObjectsInCompleteList();
    }

    private ScriptableObject[] PutAllObjectsInCompleteList()
    {
        int arraySize = 0;
        for (int i = 0; i < objectLists.Length; i++)
        {
            arraySize += objectLists[i].objects.Length;
        }
        ScriptableObject[] objects = new ScriptableObject[arraySize];

        int objectCounter = 0;
        for (int i = 0; i < objectLists.Length; i++)
        {
            for (int j = 0; j < objectLists[i].objects.Length; j++)
            {
                objects[objectCounter] = objectLists[i].objects[j];
                objectCounter++;
            }
        }

        return objects;
    }

    public ScriptableObject[] GetCompleteList()
    {
        return completeList;
    }
}

[System.Serializable]
public class ObjectList
{
    public string name;
    public ScriptableObject[] objects;
}