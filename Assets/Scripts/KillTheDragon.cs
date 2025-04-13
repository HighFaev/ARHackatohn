// Removed unnecessary using directive
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class KillTheDragon : MonoBehaviour
{

    private readonly float maxTime = 15f;
    private float time = 15f;
    private float maxSizeOfTimer;
    // Removed unused field axePrefab
    private GameObject runePrefab;

    private float timeTillEnd = 25f;
    // Removed unused field forceMagnitude

    private bool end = false;
    void Start()
    {
        time = 15f;
        maxSizeOfTimer = GameObject.Find("Timer").transform.localScale.x;
        runePrefab = Resources.Load<GameObject>($"Prefabs/Rune");
        runePrefab.name = "Rune";
    }

    void FixedUpdate()
    {
        #if UNITY_EDITOR || UNITY_WEBGL
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("SHOOOT");
            ShootTheAxe();
        }
        #else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ShootTheAxe();
        }
        #endif
        if (!end && GameObject.Find("Dragon").transform.childCount == 0)
        {
            end = true;
            GameObject camera = Camera.main.gameObject;
            TextMeshPro textComponent = runePrefab.TryGetComponent<TextMeshPro>(out var cachedTextComponent) ? cachedTextComponent : null;
            if (textComponent != null)
            {
                textComponent.text = "Zwycięstwo, wojowniku! TY WYGRAŁEŚ!";
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component is missing on the runePrefab.");
            }
            Instantiate(runePrefab, camera.transform.position, Quaternion.identity);
            Destroy(GameObject.Find("Timer"));
            //Win
        }
        if(!end && time <= 0f)
        {
            end = true;
            GameObject camera = Camera.main.gameObject;
            if (runePrefab.GetComponent<TextMeshPro>() != null)
            {
                runePrefab.GetComponent<TextMeshPro>().text = "Odpocznij, wojowniku... A gdy siły wrócą — spróbuj raz jeszcze.";
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component is missing on the runePrefab.");
            }
            Instantiate(runePrefab, camera.transform.position, Quaternion.identity);
            Destroy(GameObject.Find("Dragon"));
            Destroy(GameObject.Find("Timer"));
            //Loose
        }


        time -= Time.fixedDeltaTime;
        timeTillEnd -= Time.fixedDeltaTime;

        if(timeTillEnd <= 0)
        {
            Destroy(GameObject.Find("Dragon"));
            Destroy(GameObject.Find("Rune"));
            Destroy(this);
        }
        if(!end)
        {
            Vector3 timer_scale = GameObject.Find("Timer").transform.localScale;
            timer_scale.x = maxSizeOfTimer - (maxSizeOfTimer / maxTime * (15f- time));
            GameObject.Find("Timer").transform.localScale = timer_scale;
        }
    }

    void ShootTheAxe()
    {
        GameObject camera = Camera.main.gameObject;
        var ray = new Ray(camera.transform.position, camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 500f))
        {
            // Если луч пересекается с каким-либо объектом
            Debug.Log("МЫ УНИЧТОЖИЛИИИИ " + hit.collider.name);
            Destroy(hit.collider.gameObject);
        }
    }
}
