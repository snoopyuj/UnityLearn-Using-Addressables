using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[RequireComponent(typeof(SpriteRenderer))]
public class AddressableSpriteLoader : MonoBehaviour
{
    public AssetReferenceSprite newSprite;

    private SpriteRenderer spriteRenderer;
    private AsyncOperationHandle<Sprite> spriteOperation;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteOperation = newSprite.LoadAssetAsync();
        spriteOperation.Completed += SpriteLoaded;
    }

    private void OnDestroy()
    {
        if (spriteOperation.IsValid())
        {
            Addressables.Release(spriteOperation);
            Debug.Log("Successfully released sprite load operation.");
        }
    }

    private void SpriteLoaded(AsyncOperationHandle<Sprite> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                spriteRenderer.sprite = obj.Result;
                break;

            case AsyncOperationStatus.Failed:
                Debug.LogError("Sprite load failed.");
                break;

            default:
                break;
        }
    }
}