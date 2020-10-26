using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class RegisterUser : MonoBehaviour
{

    public TMP_InputField userInfo;
    public TMP_InputField userPassword;
    public string username;
    public string password;
    public TextMeshProUGUI messageFromServer;


    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://dariusmiclea.com/BucharestTourApp/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                messageFromServer.text = www.error;
                messageFromServer.color = new Color32(255, 20, 20, 255);
                userPassword.text = "";
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                messageFromServer.text = www.downloadHandler.text;
                if (www.downloadHandler.text.Contains("User created successfully!"))
                {
                    messageFromServer.color = new Color32(20, 255, 20, 255);
                }
                else
                {
                    messageFromServer.color = new Color32(255, 20, 20, 255);
                    userPassword.text = "";
                }
            }
        }
    }

    public void RegisterButtonClicked()
    {
        if (userInfo.text.Length < 4)
        {
            messageFromServer.text = "Username too short!";
            messageFromServer.color = new Color32(255, 20, 20, 255);
        }
        else if (userPassword.text.Length < 6)
        {
            messageFromServer.text = "Password too short!";
            messageFromServer.color = new Color32(255, 20, 20, 255);

        }
        else
        {
            username = userInfo.text;
            password = userPassword.text;
            StartCoroutine(Register());
        }
        
     }
}
