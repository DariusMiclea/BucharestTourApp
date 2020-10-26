using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public Image image;
    public DescriptionPage descPage;
    public TextMeshProUGUI points, description, name, username, language, userScore;
    public GameObject settings;
    public Button settingsButton, locationButton;
    public string lang;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        descPage.gameObject.SetActive(false);
        settings.SetActive(false);
        settingsButton.onClick.AddListener(settingsButtonClicked);
        locationButton.onClick.AddListener(locationButtonClicked);
        username.text = Player.username;
        
    }

    // Update is called once per frame
    void Update()
    {
        userScore.text = Player.score.ToString();
    }
    public void UiUpdate(Texture2D image, string points, string description, string name)
    {
        this.image.sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2());
        this.points.text = points;
        this.description.text = description;
        this.name.text = name;

        descPage.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(false);
        locationButton.gameObject.SetActive(false);
    }
    public void CloseButtonDescPage()
    {
        descPage.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(true);
        locationButton.gameObject.SetActive(true);

    }
    public void CloseButtonSettings()
    {
        settings.SetActive(false);
        settingsButton.gameObject.SetActive(true);
        locationButton.gameObject.SetActive(true);
    }
    public void LanguageToEn()
    {
        lang = "English";
        language.text = "Current Language: \n" + lang;
    }
    public void LanguageToRo()
    {
        lang = "Romana";
        language.text = "Limba curenta: \n" + lang;
    }
    public void settingsButtonClicked()
    {
        settings.SetActive(true);
        settingsButton.gameObject.SetActive(false);
        locationButton.gameObject.SetActive(false);
    }
    public void locationButtonClicked()
    {
        //Camera.main.transform.position = player.transform.position;
        Camera.main.transform.position = new Vector3(player.transform.position.x, 50, player.transform.position.z);
        Camera.main.orthographicSize = 30;
    }
    public void logout()
    {
        SceneManager.LoadScene("AutthScene");
    }

    
}
