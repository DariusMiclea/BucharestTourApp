using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using Mapbox.Unity.Map;
using System.IO;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    
    
    public TextAsset jsonFile;
    public AbstractMap map;
    public GameObject player;
    public Texture2D[] attractionImages, imagesFromWeb;
    public Texture2D imageFromWeb;
    public List<GameObject> markers;
    public GameObject UiManager;
    string[] locationStrings;
    int[] idArray;
    int[] scoreArray;
    string[] descriptionRoArray;
    string[] descriptionEnArray;
    string[] nameRoArray;
    string[] nameEnArray;
    public Sprite[] pictures;
    public int playerScore;
    public string[] attractionId;
    


    [Serializable]
    public struct Data
    {
        public int id;
        public string nameEn;
        public string nameRo;
        public string lat;
        public string lon;
        public string descriptionEn;
        public string descriptionRo;
        public int score;
        
    }
    [Serializable]
    public class Attraction
    {
        public Data[] data;
    }
    public Attraction attractions;

    void Start()
    {
        
        attractions = JsonUtility.FromJson<Attraction>(jsonFile.text);
        
        int i = 0;
        attractionImages = new Texture2D[attractions.data.Length];
        imagesFromWeb = new Texture2D[attractions.data.Length];
        locationStrings = new string[attractions.data.Length];
        idArray = new int[attractions.data.Length];
        scoreArray = new int[attractions.data.Length];
        nameRoArray = new string[attractions.data.Length];
        descriptionRoArray = new string[attractions.data.Length];
        nameEnArray = new string[attractions.data.Length];
        descriptionEnArray = new string[attractions.data.Length];
        pictures = new Sprite[attractions.data.Length];

        foreach (Data attraction in attractions.data)
        {
            locationStrings[i] = attraction.lat + "," + attraction.lon;
            idArray[i] = attraction.id;
            nameRoArray[i] = attraction.nameRo;
            nameEnArray[i] = attraction.nameEn;
            scoreArray[i] = attraction.score;
            descriptionRoArray[i] = attraction.descriptionRo;
            descriptionEnArray[i] = attraction.descriptionEn;
            StartCoroutine(LoadPicture(attraction.id));
            

            i++;
        }

        map.gameObject.GetComponent<SpawnOnMap>().SetLocationStrings(locationStrings);
        map.gameObject.GetComponent<SpawnOnMap>().SetValues(idArray, scoreArray, nameRoArray, descriptionRoArray, nameEnArray, descriptionEnArray);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (map.gameObject.GetComponent<SpawnOnMap>().GetSpawnedObjects() != null)
        {
            AttractionVisited(map.gameObject.GetComponent<SpawnOnMap>().GetSpawnedObjects());
        }
    }

    public void UiUpdate(Texture2D image, string points, string descriptionRo, string nameRo, string descriptionEn, string nameEn)
    {
        if(UiManager.GetComponent<UiManager>().lang == "Romana")
        {
            UiManager.GetComponent<UiManager>().UiUpdate(image, points, descriptionRo, nameRo);
        }
        else
        {
            UiManager.GetComponent<UiManager>().UiUpdate(image, points, descriptionEn, nameEn);
        }
        
    }
    
    
    IEnumerator LoadPicture(int id)
{
        
        using (UnityWebRequest pictureRequest = new UnityWebRequest("http://dariusmiclea.com/BucharestTourApp/Pictures/" + id.ToString() + ".jpg", UnityWebRequest.kHttpVerbGET))
        {

            pictureRequest.downloadHandler = new DownloadHandlerTexture();
            yield return pictureRequest.SendWebRequest();
            attractionImages[id-1] = DownloadHandlerTexture.GetContent(pictureRequest);
        }
        

 }
    public void AttractionVisited(List<GameObject> markersList)
    {
        for(int i = 0; i < markersList.Count; i++)
        {
            if (markersList[i].GetComponent<Marker>().isOn && Vector3.Distance(markersList[i].transform.position,
                player.transform.position) <= 5)
            {
                Player.score += markersList[i].GetComponent<Marker>().score;
                Player.attractionsId = Player.attractionsId + "," + i.ToString();
                markersList[i].GetComponent<Marker>().isOn = false;
                markersList[i].GetComponentInChildren<ParticleSystem>().Stop();

                Debug.Log(markersList[i].GetComponent<Marker>().nameRo + "was unlocked!");
            }
            
        }
    }
}
