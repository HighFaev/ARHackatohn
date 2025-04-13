using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Subsystems;

public class WebTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        string exampleUrl = "https://httpstat.us/200";
        string responce = await WebUtils.GetJsonAsync(exampleUrl, "");

        Debug.Log(responce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
