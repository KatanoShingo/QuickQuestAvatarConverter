using UnityEditor;
using UnityEngine;

public class QuickQuestAvatarConverter : EditorWindow
{
    [MenuItem( "Window/Quest Avatar Setup" )]
    public static void Setup()
    {
        GameObject[] allGameObject = FindObjectsOfType( typeof( GameObject ) ) as GameObject[];

        foreach( var item in allGameObject )
        {
            ChangeShader( item );
            DeleteBone( item );
        }
    }

    static void ChangeShader( GameObject go )
    {
        var renderer = go.GetComponent<Renderer>();

        if( renderer == null )
        {
            return;
        }

        var materials = renderer.materials;

        foreach( var material in materials )
        {
            material.shader = Shader.Find( "VRChat/Mobile/Toon Lit" );
        }
    }

    static void DeleteBone( GameObject go )
    {
        GameObjectUtility.RemoveMonoBehavioursWithMissingScript( go );

        var dynamicBone = go.GetType().GetMethod( "DynamicBone" );
#if dynamicBone != null

        GameObject.DestroyImmediate( go.GetComponent<DynamicBone>() );
#endif

        var dynamicBoneCollider = go.GetType().GetMethod( "DynamicBoneCollider" );
#if dynamicBoneCollider != null

        GameObject.DestroyImmediate( go.GetComponent<DynamicBoneCollider>() );
#endif 
    }
}