using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class Addr_Test : MonoBehaviour
{
    public AssetReferenceGameObject Test1;
    public Image LoadingStatus;
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        Test1.LoadAssetAsync().Completed += LoadAsset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Test1.InstantiateAsync().Completed += CreateObj;
            
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Test1.ReleaseInstance(obj);

        }
    }

    private void LoadAsset(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            LoadingStatus.color = Color.green;
        }
        else
        {
            LoadingStatus.color = Color.red;
        }
        handle.Completed -= LoadAsset;
    }

    private void CreateObj(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            obj = handle.Result;
        }
    }
}
