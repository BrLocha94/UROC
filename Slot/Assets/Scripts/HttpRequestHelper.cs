using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequestHelper
{
    static string baseUrl = "https://api-test.uroc.com/api/";

    static string AuthorizationToken;
    
    public string _activeResponse { get; private set; }
    
    public HttpRequestHelper() 
    {
        AuthorizationToken = "";
        _activeResponse = "";
    }

    public IEnumerator GetRequest(string route, Dictionary<string, string> queryParams, Action onRequestSucceded, Action onRequestFailed)
    {
        string endpoint = baseUrl + route;

        string queryString = queryParams != null ? "?" + string.Join("&", queryParams.Select(kv => $"{kv.Key}={UnityWebRequest.EscapeURL(kv.Value)}")) : "";

        UnityWebRequest uwr = UnityWebRequest.Get(endpoint + queryString);
        if (!String.IsNullOrEmpty(AuthorizationToken))
        {
            uwr.SetRequestHeader("Bearer", AuthorizationToken);
        }
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            onRequestFailed?.Invoke();
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            _activeResponse = uwr.downloadHandler.text;
            if (!String.IsNullOrEmpty(uwr.GetResponseHeader("Authorization")))
            {
                AuthorizationToken = uwr.GetResponseHeader("Authorization");
            }
            onRequestSucceded?.Invoke();
        }
    }
}
