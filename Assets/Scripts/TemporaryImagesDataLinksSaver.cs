using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class TemporaryImagesDataLinksSaver : MonoBehaviour
{
    public static TemporaryImagesDataLinksSaver Instance;
    private string _pathToDirectory;
    private string _pathToFile;
    private bool _isDataReadyForReading;

   [SerializeField] private HashSet<string> _imageLinks = new HashSet<string>();

    private void Awake()
    {
        _pathToDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar + "ImagesLinksForStream";
        _pathToFile = _pathToDirectory + Path.DirectorySeparatorChar + "ImageLinks.txt";
        if (!Directory.Exists(_pathToDirectory))
        {
            Directory.CreateDirectory(_pathToDirectory);
            Debug.Log("CREATE LINKS DIRCTORY");

            File.CreateText(_pathToFile);
            
        }
        else
        {
            if (!File.Exists(_pathToFile))
            {
                File.CreateText(_pathToFile);
            }
            File.Open(_pathToFile,FileMode.Open,FileAccess.ReadWrite);
            Debug.Log("LINKS DIRCTORY EXISTS"+ _pathToFile);
            if(new FileInfo(_pathToFile).Length == 0)
            {
                Debug.Log("LINKS FILE IS EMPTY");
            }
            else
            {
                _isDataReadyForReading = true;
            }
        }
        Instance = this;
        
    }


    public bool ContainsKey(string key)
    {
        
        if (_imageLinks.Contains(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddLink(string key)
    {
        if (!_imageLinks.Contains(key))
        {
            _imageLinks.Add(key);
            
            AddLineToFile(key);
        }
    }
    private void AddLineToFile(string key)
    {
        using (FileStream fStream = new FileStream(_pathToFile, FileMode.Append))
        {
            using (StreamWriter stream=new StreamWriter(fStream))
            {
                stream.WriteLine(key);
            }
        }
    }
    private void Start()
    {
        if (_isDataReadyForReading)
        {
            var collection = File.ReadAllLines(_pathToFile).ToList();

            foreach (var item in collection)
            {
                _imageLinks.Add(item);
            }
            Debug.Log("ImageLinksCount= " + _imageLinks.Count);
        }
    }
}
