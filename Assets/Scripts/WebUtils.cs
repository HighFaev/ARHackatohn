using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class WebUtils
{
    public static async Task<string> GetJsonAsync(string baseUrl, string queryParams)
    {
        string fullUrl = $"{baseUrl}?{queryParams}";
        using (UnityWebRequest request = UnityWebRequest.Get(fullUrl))
        {
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[WebUtils] Error fetching data: {request.error}");
                return null;
            }

            return request.downloadHandler.text;
        }
    }
}
