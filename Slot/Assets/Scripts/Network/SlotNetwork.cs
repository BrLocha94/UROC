using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class SlotNetwork : MonoSingleton<SlotNetwork>
{
    public Action onRequestSucceded;
    public Action onRequestFailed;

    private HttpRequestHelper httpRequestHelper = new HttpRequestHelper();

    private string initializeRoute = "SlotMachine/Initialise";
    private string spinRoute = "SlotMachine/Spin";
    private string confirmRoute = "SlotMachine/Confirm";

    private const string BRAND = "UROC";
    private const string GAME_TOKEN = "3x3_10FreeSpins";
    private const string PLAYER_TOKEN = "12345-abcde";

    public SlotMachineSessionResponse sessionResponse { get; private set; }
    public SlotMachineSpinResponse spinResponse { get; private set; }

    private float retryTimer = 5f;

    private int maxRetry = 3;
    private int retryCounter = 0;

    private float defaultBetAmount = 1f;
    private int defaultNumberOfLines = 5;

    protected override void ExecuteOnAwake()
    {
        base.ExecuteOnAwake();
        DontDestroyOnLoad(gameObject);
    }

    public void StartRequestInitialize()
    {
        retryCounter = 0;

        Dictionary<string, string> queryParams = new Dictionary<string, string>
        {
            { "Brand", BRAND },
            { "GameToken", GAME_TOKEN },
            { "PlayerToken", PLAYER_TOKEN }
        };

        StartCoroutine(httpRequestHelper.GetRequest(
            initializeRoute,
            queryParams, 
            OnInitializeSucceded, OnInitializeFailed));
    }

    private void OnInitializeSucceded()
    {
        Debug.Log("Initialize succeded");
        sessionResponse = JsonConvert.DeserializeObject<SlotMachineSessionResponse>(httpRequestHelper._activeResponse);
        onRequestSucceded?.Invoke();
    }

    private void OnInitializeFailed()
    {
        if (retryCounter < maxRetry)
        {
            Debug.Log($"INITIALIZE FAILED, WILL RETRY IN {retryTimer} SECCONDS");
            retryCounter++;
            Invoke(nameof(StartRequestInitialize), retryTimer);
            return;
        }

        Debug.Log($"INITIALIZE MAX RETRY EXCEDED, REQUEST FAILED");
        onRequestFailed?.Invoke();
    }

    public void StartRequestSpin()
    {
        retryCounter = 0;

        Dictionary<string, string> queryParams = new Dictionary<string, string>
        {
            { "gameId", sessionResponse.GameOverview.GameId.ToString() },
            { "betAmount", defaultBetAmount.ToString() },
            { "numberOfLines", defaultNumberOfLines.ToString() }
        };

        // SPIN MUST HAVE AN MAX RETRY COUNT
        StartCoroutine(httpRequestHelper.GetRequest(
            spinRoute,
            queryParams,
            OnSpinSucceded, OnSpinFailed));
    }

    private void OnSpinSucceded()
    {
        Debug.Log("Spin succeded");
        spinResponse = JsonConvert.DeserializeObject<SlotMachineSpinResponse>(httpRequestHelper._activeResponse);
        onRequestSucceded?.Invoke();
    }

    private void OnSpinFailed()
    {
        if (retryCounter < maxRetry)
        {
            Debug.Log($"SPIN FAILED, WILL RETRY IN {retryTimer} SECCONDS");
            retryCounter++;
            Invoke(nameof(StartRequestSpin), retryTimer);
            return;
        }

        Debug.Log($"SPIN MAX RETRY EXCEDED, REQUEST FAILED");
        onRequestFailed?.Invoke();
    }

    public void StartRequestConfirm()
    {
        retryCounter = 0;

        Dictionary<string, string> queryParams = new Dictionary<string, string>
        {
            { "gameId", sessionResponse.GameOverview.GameId.ToString() },
            { "uid", spinResponse.SpinResult.uid }
        };

        // Must me confirmed at server before continue
        StartCoroutine(httpRequestHelper.GetRequest(
            confirmRoute,
            queryParams,
            OnConfirmSucceded, OnConfirmFailed));
    }

    private void OnConfirmSucceded()
    {
        Debug.Log("Confirm succeded");
        spinResponse = JsonConvert.DeserializeObject<SlotMachineSpinResponse>(httpRequestHelper._activeResponse);
        onRequestSucceded?.Invoke();
    }

    private void OnConfirmFailed()
    {
        if (retryCounter < maxRetry)
        {
            Debug.Log($"CONFIRM FAILED, WILL RETRY IN {retryTimer} SECCONDS");
            retryCounter++;
            Invoke(nameof(StartRequestSpin), retryTimer);
            return;
        }

        Debug.Log($"CONFIRM MAX RETRY EXCEDED, REQUEST FAILED");
        onRequestFailed?.Invoke();
    }
}
