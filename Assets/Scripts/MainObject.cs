using System;
using System.Collections;
using System.Collections.Generic;
using Backtrace.Unity;
using Backtrace.Unity.Model;
using UnityEngine;

public class MainObject : MonoBehaviour
{
    // Backtrace client instance
    private BacktraceClient _backtraceClient;
    void Start()
    {
        Debug.Log("BTCLD: Starting main object");

        var serverUrl = "https://submit.backtrace.io/konst-ryab/04aecb6b3da05e8d83f2a27f9b5f41352ac792428c5e5f6a94ff58d43ee14e46/json";
        var gameObjectName = "Backtrace";
        var databasePath = "${Application.persistentDataPath}/sample/backtrace/path";
        var attributes = new Dictionary<string, string>() { { "my-super-cool-attribute-name", "attribute-value" } };

        // use game object to initialize Backtrace integration
        _backtraceClient = GameObject.Find(gameObjectName).GetComponent<BacktraceClient>();
        //Read from manager BacktraceClient instance
        var database = GameObject.Find(gameObjectName).GetComponent<BacktraceDatabase>();

        // or initialize Backtrace integration directly in your source code
        Debug.Log("BTCLD: Initialize BacktraceClient");
        _backtraceClient = BacktraceClient.Initialize(
                url: serverUrl,
                databasePath: databasePath,
                gameObjectName: gameObjectName,
                attributes: attributes);
    }
    // Update is called once per frame
    void Update()
    {
        try
        {
            // throw an exception here
        }
        catch (Exception exception)
        {
            var report = new BacktraceReport(
               exception: exception,
               attributes: new Dictionary<string, string>() { { "key", "value" } },
               attachmentPaths: new List<string>() { @"file_path_1", @"file_path_2" }
           );
            _backtraceClient.Send(report);
        }
    }
}
