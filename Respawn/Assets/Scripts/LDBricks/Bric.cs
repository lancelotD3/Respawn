using UnityEditor;
using UnityEngine;

public enum BricState
{
    Full,
    Broken,
    Empty
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bric : LDBrick
{
    BoxCollider2D boxCollider;
    bool canInteract = false;
    public BricState bricState = BricState.Full;

    private void Awake()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if(bricState == BricState.Empty)
            {
                SetState(BricState.Full);
            }
        }
    }

    public void SetState(BricState state)
    {
        bricState = state;
        Vector4 c = GetComponentInChildren<SpriteRenderer>().color;

        if(bricState == BricState.Full)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Vector4(c.x, c.y, c.z, 1f);
            GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
        else if(bricState == BricState.Broken)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Vector4(c.x, c.y, c.z, .5f);
            GetComponentInChildren<BoxCollider2D>().enabled = true;

        }
        else if(bricState == BricState.Empty)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Vector4(c.x, c.y, c.z, .2f);
            GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Bric))]
public class BricEditor : Editor
{
    private Bric self;

    private void OnEnable()
    {
        self = (Bric)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("ATTENTION, IL FAUT MODIFER N'IMPORTE QUEL TRUC POUR ENREGISTRER");
        if (GUILayout.Button("Full"))
        {
            self.SetState(BricState.Full);
        }
        if (GUILayout.Button("Broken"))
        {
            self.SetState(BricState.Broken);
        }
        if (GUILayout.Button("Empty"))
        {
            self.SetState(BricState.Empty);
        }
    }
}
#endif