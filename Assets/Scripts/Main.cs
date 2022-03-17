using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class Main : MonoBehaviour
{

    public Trivial trivial;
    void Start()
    {
        
        //StartCoroutine(GetRequest("https://servizos.meteogalicia.gal/mgrss/predicion/jsonCPrazo.action?dia=0&request_locale=gl"));

        StartCoroutine(GetRequest("https://opentdb.com/api.php?amount=5&category=15"));

        
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;               
            }
            trivial = JsonUtility.FromJson<Trivial>(webRequest.downloadHandler.text);

            Debug.Log(trivial.results[Random.Range(0, 5)].question);
            Debug.Log(trivial.results[3].incorrect_answers[2]);
            Debug.Log(trivial.results[4].difficulty);
        }
    }   


   

    
    

}
