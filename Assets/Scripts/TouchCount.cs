using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpdateTimeOnTouch : MonoBehaviour
{
    // Назначьте в инспекторе ссылку на элемент Text, где будет выводиться время
    public TextMeshProUGUI timeText;
    public GameObject prefab;
    public Camera MainCamera;
    public float maxDistance = 1000f;

    void Update()
    {
        // Обработка касания на мобильном устройстве
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            CreatePrefab();
            UpdateTime();
        }
        
        if (Input.touchCount > 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
           DeleteObj();
        }
    }

    // Метод обновления текста текущим временем
    private void UpdateTime()
    {
        // Формат времени можно изменить по необходимости
        timeText.text = DateTime.Now.ToString("HH:mm:ss");
    }
    private void CreatePrefab()
    {
        // Формат времени можно изменить по необходимости
        Vector3 spawnPosition = MainCamera.transform.position;
        Quaternion spawnRotation = MainCamera.transform.rotation;

        Instantiate(prefab, spawnPosition, spawnRotation);
    }

    private void DeleteObj()
    {
        Ray ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Если луч пересекается с каким-либо объектом
            timeText.text = "МЫ УНИЧТОЖИЛИИИИ " + hit.collider.name;
            Destroy(hit.collider.gameObject);
        }
    }
}
