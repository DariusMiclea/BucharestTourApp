using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class CameraMovement : MonoBehaviour
{
    public float speed = 0.04f;
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchPosition.x * speed* Camera.main.orthographicSize/30,
                -touchPosition.y * speed* Camera.main.orthographicSize/30, 0); 
        }
        else if(Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            Vector2 touch0Prev = touch0.position - touch0.deltaPosition;
            Vector2 touch1Prev = touch1.position - touch1.deltaPosition;
            float prevMagnitude = (touch0Prev - touch1Prev).magnitude;
            float magnitude = (touch0.position - touch1.position).magnitude;
            float dif = magnitude - prevMagnitude;
            Zoom(dif * 0.01f);
        }

    }
    void Zoom(float zoom)
    {
        Camera.main.orthographicSize = Camera.main.orthographicSize - zoom * Camera.main.orthographicSize / 30;
        //map.GetComponent<AbstractMap>().Options.locationOptions.zoom = Mathf.Clamp(map.GetComponent<AbstractMap>().Options.locationOptions.zoom + zoom, 0, 20);
        
    }
    
}
