using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public static string username = "";
    public static int score = 0;
    public static string attractionsId = " ";
    int lastScore = 0;
    string lastAttractionsId = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lastScore != score)
        {
            StartCoroutine(UpdateScore());
            lastScore = score;
        }

        if(lastAttractionsId != attractionsId)
        {
            StartCoroutine(UpdateAttractionId());
            lastAttractionsId = attractionsId;
        }
        if(attractionsId == "0")
        {
            attractionsId = "";
        }
        
        
    }

    IEnumerator UpdateScore()
    {
        WWWForm form = new WWWForm();
        form.AddField("userInfo", username);
        form.AddField("score", score);
        using (UnityWebRequest www = UnityWebRequest.Post("http://dariusmiclea.com/BucharestTourApp/UpdateScore.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    IEnumerator UpdateAttractionId()
    {
        WWWForm form = new WWWForm();
        form.AddField("userInfo", username);
        form.AddField("attraction_id", attractionsId);
        using (UnityWebRequest www = UnityWebRequest.Post("http://dariusmiclea.com/BucharestTourApp/UpdateAttraction.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
  
   
}
