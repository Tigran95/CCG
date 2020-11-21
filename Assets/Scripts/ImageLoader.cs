using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ImageLoader : MonoBehaviour
{

    [SerializeField] private RawImage _rwImage;

    private void Start()
    {
        StartCoroutine(LoadFromLink("https://picsum.photos/400/600"));
    }

    public IEnumerator LoadFromLink(string url)
    {
        yield return new WaitForEndOfFrame();
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {

            ////////////// Use this if the resource links are different

            //Texture2D txtFromMyData = TemporaryImagesDataSaver.Instance.GetTextureByLink(url);
            //if (txtFromMyData != null)
            //{
            //    _rwImage.texture = txtFromMyData;
            //    yield break;
            //}
            ////////////////////////
            
            yield return uwr.SendWebRequest();
            if (!uwr.isNetworkError)
            {
                var texture = DownloadHandlerTexture.GetContent(uwr);
                TemporaryImagesDataSaver.Instance.AddTextureWithLink(url, texture);

                _rwImage.texture = texture;
                texture = null;
            }
            else
            {
                Debug.Log("Error in Responce Image" + uwr.error);
            }
        }
    }
}
