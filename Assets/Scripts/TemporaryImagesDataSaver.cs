using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System;
using System.Text;

public class TemporaryImagesDataSaver : MonoBehaviour
{
    public static TemporaryImagesDataSaver Instance;
    private string _pathToDirectory;
    private void Awake()
    {
        _pathToDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar + "ImagesForStream";

        if(!Directory.Exists(_pathToDirectory))
        {
            Directory.CreateDirectory(_pathToDirectory);
            Debug.Log("CREATE DIRCTORY");
        }
        else
        {
            Debug.Log("DIRCTORY EXISTS");
        }

        Instance = this;
    }

    public async void AddTextureWithLink(string key, Texture2D value)
    {

        string hashedKey = HashSHA1(key);

        string filePath = _pathToDirectory + Path.DirectorySeparatorChar + hashedKey + ".png";

        if (!TemporaryImagesDataLinksSaver.Instance.ContainsKey(hashedKey))
        {
            Debug.Log("ADD");


            byte[] pngBytesData = value.EncodeToPNG();


            try
            {
                using (FileStream sourceStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize: 512, useAsync: true))
                {
                    await Task.Run(() =>
                    {
                        sourceStream.WriteAsync(pngBytesData, 0, pngBytesData.Length);
                        TemporaryImagesDataLinksSaver.Instance.AddLink(hashedKey);
                    });
                }
            }
            catch (Exception)
            {
                Debug.LogError("Cant Add Image to storage2");
            }
        }
    }

    public  Texture2D GetTextureByLink(string key)
    {
        string hashedKey = HashSHA1(key);
        string filePath = _pathToDirectory + Path.DirectorySeparatorChar + hashedKey + ".png";

        if (!TemporaryImagesDataLinksSaver.Instance.ContainsKey(hashedKey))
        {
            return null;
        }
        else
        {
            try
            {
                using (FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None, bufferSize: 512, useAsync: true))
                {
                    try
                    {
                        using (MemoryStream mStream = new MemoryStream())
                        {
                            fStream.CopyTo(mStream);
                            Debug.Log("LOAD");
                            byte[] pngBytesData = mStream.ToArray();
                            Texture2D tex = new Texture2D(2, 2, TextureFormat.RGB24, false);
                            tex.LoadImage(pngBytesData);
                            tex.Apply();
                            return tex;
                        }
                    }
                    catch (Exception)
                    {
                        Debug.LogError("Cant Get Image to storage1");
                    }
                }
            }
            catch (Exception)
            {
                Debug.LogError("Cant Get Image to storage2");
            }
            return null;
        }
    }
    public string HashSHA1(string input)
    {
        using (SHA1Managed sha1 = new SHA1Managed())
        {
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                // can be "x2" if you want lowercase
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
