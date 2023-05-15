# Getting Started with Addressables

[[Tutorial Link]](https://learn.unity.com/project/getting-started-with-addressables?uv=2021.3)

## Notes
* Window > Asset Management > Addressables > Groups
* Groups Window > Tools > Inspect System Settings (AddressableAssetSettings File)
* Load Asset then instantiate manually:
```csharp 
private void Start()
{
    objectOperation = Addressables.LoadAssetAsync<GameObject>(objectToLoad);
    objectOperation.Completed += ObjectLoadDone;
}

private void ObjectLoadDone(AsyncOperationHandle<GameObject> obj)
{
    if (obj.Status != AsyncOperationStatus.Succeeded)
    {
        return;
    }

    var loadedObject = obj.Result;
    instantiatedObject = Instantiate(loadedObject);
}

private void OnDestroy()
{
    if (objectOperation.IsValid())
    {
        Addressables.Release(objectOperation);
    }

    Destroy(instantiatedObject);
}
```
* Load and Instantiate in one step:
```csharp
accessoryObjectOperation = accessoryObjectToLoad.InstantiateAsync(instantiatedObject.transform);
accessoryObjectOperation.Completed += op =>
{
    if (op.Status != AsyncOperationStatus.Succeeded)
    {
        return;
    }

    Debug.Log("Successfully loaded and instantiated accessory object.");
};

if (accessoryObjectOperation.IsValid())
{
    Addressables.ReleaseInstance(accessoryObjectOperation);
}
```
* `AssetReferenceSprite`