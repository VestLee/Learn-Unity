using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#if UNITY_EDITOR
using UnityEditor;
//#endif

public class CreateTexture2DArray : MonoBehaviour
{
    public Texture2D[] Textures;
    [ContextMenu("Make Texture2DArray RGBA32")]
    // Update is called once per frame
    void MakeTexture2DArrayAssets()
    {
        //Note that this class does not support Texture2DArray creation with a Crunch compression TextureFormat.
        Texture2DArray mTex2DArray = new Texture2DArray(Textures[0].width, Textures[0].height, Textures.Length, TextureFormat.RGBA32, false);
        for (int index = 0; index < Textures.Length; index++)
        {
            mTex2DArray.SetPixels32(Textures[index].GetPixels32(), index, 0);
        }
        mTex2DArray.Apply();
        mTex2DArray.wrapMode = TextureWrapMode.Clamp;
        mTex2DArray.filterMode = FilterMode.Bilinear;

        AssetDatabase.CreateAsset(mTex2DArray, "Assets/Texture2DArray/Assets/AlbedoArray.asset");
    }

    [ContextMenu("Make Texture2DArray With Mipmap RGBA32")]
    // Update is called once per frame
    void MakeTexture2DArrayAssetsWithMipmap()
    {
        //Note that this class does not support Texture2DArray creation with a Crunch compression TextureFormat.
        Texture2DArray mTex2DArray = new Texture2DArray(Textures[0].width, Textures[0].height, Textures.Length, TextureFormat.RGBA32, true, false);
        for (int index = 0; index < Textures.Length; index++)
        {
            for (int m = 0; m < Textures[index].mipmapCount; m++)
            {
                Graphics.CopyTexture(Textures[index], 0, m, mTex2DArray, index, m);
            }
        }
        AssetDatabase.CreateAsset(mTex2DArray, "Assets/Texture2DArray/Assets/AlbedoArrayWithMipmap.asset");
    }

    [ContextMenu("Make Texture2DArray RGBACompressed")]
    // Update is called once per frame
    void MakeTexture2DArrayAssetsCompressed()
    {
        //Note that this class does not support Texture2DArray creation with a Crunch compression TextureFormat.
        Texture2DArray mTex2DArray = new Texture2DArray(Textures[0].width, Textures[0].height, Textures.Length, Textures[0].format,true, false);
        for (int index = 0; index < Textures.Length; index++)
        {
            for (int m = 0; m < Textures[index].mipmapCount; m++)
            {
                Graphics.CopyTexture(Textures[index], 0, m, mTex2DArray, index, m);
            }
        }
        AssetDatabase.CreateAsset(mTex2DArray, "Assets/Texture2DArray/Assets/AlbedoArrayCompressed.asset");

    }
}
