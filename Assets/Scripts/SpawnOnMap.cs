using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
using System.Collections;

public class SpawnOnMap : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    [Geocode]
    string[] _locationStrings;
    Vector2d[] _locations;

    [SerializeField]
    float _spawnScale = 100f;

    [SerializeField]
    GameObject _markerPrefab;

    List<GameObject> _spawnedObjects;
    int[] idArray;
    int[] scoreArray;
    string[] descriptionRoArray;
    string[] descriptionEnArray;
    string[] nameRoArray;
    string[] nameEnArray;
    Texture2D[] images;

    void Start()
    {
        StartCoroutine(LateStart(1f));

    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Init();
    }

    private void Update()
    {
        if (_spawnedObjects != null)
        {
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                var location = _locations[i];
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }

    }
    public void Init()
    {
        _locations = new Vector2d[_locationStrings.Length];
        _spawnedObjects = new List<GameObject>();
        for (int i = 0; i < _locationStrings.Length; i++)
        {
            var locationString = _locationStrings[i];
            _locations[i] = Conversions.StringToLatLon(locationString);
            var instance = Instantiate(_markerPrefab, new Vector3(1000, -1000, 1000), Quaternion.identity);
            instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            instance.GetComponent<Marker>().score = scoreArray[i];
            instance.GetComponent<Marker>().id = idArray[i];
            instance.GetComponent<Marker>().nameEn = nameEnArray[i];
            instance.GetComponent<Marker>().nameRo = nameRoArray[i];
            instance.GetComponent<Marker>().descriptionTextEn = descriptionEnArray[i];
            instance.GetComponent<Marker>().descriptionTextRo = descriptionRoArray[i];
            //instance.GetComponent<Marker>().image = images[i];


            _spawnedObjects.Add(instance);
        }
    }
    public void SetLocationStrings(string[] locationStrings)
    {
        _locationStrings = locationStrings;
    }
    public void SetMarkerPrefab(GameObject markerPrefab)
    {
        _markerPrefab = markerPrefab;
    }
    public void SetSpawnScale(float spawnScale)
    {
        _spawnScale = spawnScale;
    }
    public void SetValues(int[] ids, int[] scores, string[] namesRo, string[] descRo, string[] namesEn, string[] descEn)
    {
        idArray = ids;
        scoreArray = scores;
        nameRoArray = namesRo;
        descriptionRoArray = descRo;
        nameEnArray = namesEn;
        descriptionEnArray = descEn;
    }
    public void SetImages(Texture2D[] images)
    {
        this.images = images;
    }
    public List<GameObject> GetSpawnedObjects()
    {
        if (_spawnedObjects != null)
        {
            return _spawnedObjects;
        }
        else return null;

    }
}