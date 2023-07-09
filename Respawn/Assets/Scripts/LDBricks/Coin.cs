using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Coin : LDBrick
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetState(true);
    }

    public void SetState(bool state)
    {
        //MARIUS
        if (state != bFinished)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Props/Coin");
        }
        //MARIUS

        bFinished = state;
        Vector4 c = GetComponentInChildren<SpriteRenderer>().color;
        GetComponentInChildren<SpriteRenderer>().color = new Vector4(c.x, c.y, c.z, state ? 1f : 0.3f);
        
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Coin))]
public class CoinEditor : Editor
{
    private Coin self;

    private void OnEnable()
    {
        self = (Coin)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("ATTENTION, IL FAUT MODIFER N'IMPORTE QUEL TRUC POUR ENREGISTRER");
        if (GUILayout.Button("Enable/Disable"))
        {
            self.SetState(!self.bFinished);
        }
    }
}
#endif