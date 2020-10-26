using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Marker : MonoBehaviour
{
   
    public GameManager gameManager;
    public int score = 0;
    public int id = 0;
    public string nameRo = "None";
    public string descriptionTextRo = "Default description";
    public string nameEn;
    public string descriptionTextEn;
    public bool isOn = false;
    public Material matWhenOn, matWhenOff;
    ParticleSystem particles;
    
    public Texture2D image;
    
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
        StartCoroutine(LateStart(3f));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isOn = true;
        string[] attractionsId = Player.attractionsId.Split(',');
        for(int i = 0; i < attractionsId.Length; i++)
        {
            if(attractionsId[i] == (id-1).ToString())
            {
                this.GetComponentInChildren<ParticleSystem>().Stop();
                isOn = false;
            }
        }
        
    }
  
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject == gameObject)
                {
                    
                    gameManager.UiUpdate(gameManager.attractionImages[id-1], score.ToString(), descriptionTextRo, nameRo, descriptionTextEn, nameEn);
                    
                }
            }
        }
        if (isOn)
        {
            GetComponent<MeshRenderer>().material = matWhenOn;
            particles.transform.localScale = gameObject.transform.localScale;
        }
        else
        {
            GetComponent<MeshRenderer>().material = matWhenOff;
        }
        
        
        
    }

   
}
