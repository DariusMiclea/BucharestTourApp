using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    public TMP_InputField userInfo;
    public TMP_InputField userPassword;
    public TextMeshProUGUI messageFromServer;
    public string username;
    public string password;

    IEnumerator UserLogin()
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://dariusmiclea.com/BucharestTourApp/Login.php", form))
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
                string[] message = www.downloadHandler.text.Split('|');
                messageFromServer.text = message[0];
                if (message[0] == "Login Success!")
                {
                    messageFromServer.color = new Color32(20, 255, 20, 255);
                    Player.username = username;
                    Player.score = int.Parse(message[1]);
                    Player.attractionsId = message[2];
                    SceneManager.LoadScene("GameScene");
                }
                else
                {
                    messageFromServer.color = new Color32(255, 20, 20, 255);
                    userPassword.text = "";
                }
                
            }
        }
    }


    public void LoginButtonClicked()
    {
        if(userInfo.text.Length < 4)
        {
            messageFromServer.text = "Username too short!";
            messageFromServer.color = new Color32(255, 20, 20, 255);
        }
        else if(userPassword.text.Length < 6)
        {
            messageFromServer.text = "Password too short!";
            messageFromServer.color = new Color32(255, 20, 20, 255);

        }
        else
        {
            username = userInfo.text;
            password = userPassword.text;
            StartCoroutine(UserLogin());
        }
        
     }
}
