using UnityEditor;
using UnityEngine;
using System.Collections;

public enum BrickState
{
    Full,
    Broken,
    Empty
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bric : LDBrick
{
    bool canUseBlocks = true;

    BoxCollider2D boxCollider;
    bool canInteract = false;
    public BrickState brickState = BrickState.Full;

    public GameObject BottomBrick;

    public BrickState wantedBrickState = BrickState.Full;

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
            canInteract = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if(brickState == BrickState.Empty)
            {
                SetState(BrickState.Full);
            }
        }
    }

    private void FixedUpdate()
    {

        RaycastHit2D bottomHit = Physics2D.Raycast(BottomBrick.transform.position, -Vector2.up / 5);
        Debug.DrawRay(BottomBrick.transform.position, -Vector2.up / 5, Color.red);

        if (bottomHit.collider != null)
            if (bottomHit.collider.tag == "Player" && bottomHit.distance < .2f)
            {
                if (brickState == BrickState.Broken && canUseBlocks)
                {
                    SetState(BrickState.Empty);
                    canUseBlocks = false;
                    StartCoroutine(DelayBrick());
                }
                else if(brickState == BrickState.Full && canUseBlocks)
                {
                    SetState(BrickState.Broken);
                    canUseBlocks = false;
                    StartCoroutine(DelayBrick());
                }
            }
    }

    public void SetState(BrickState state)
    {
        brickState = state;
        Vector4 c = GetComponentInChildren<SpriteRenderer>().color;

        if(brickState == BrickState.Full)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Vector4(c.x, c.y, c.z, 1f);
            GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
        else if(brickState == BrickState.Broken)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Vector4(c.x, c.y, c.z, .5f);
            GetComponentInChildren<BoxCollider2D>().enabled = true;

        }
        else if(brickState == BrickState.Empty)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Vector4(c.x, c.y, c.z, .2f);
            GetComponentInChildren<BoxCollider2D>().enabled = false;
        }

        bFinished = brickState == wantedBrickState;
    }
    IEnumerator DelayBrick()
    {
        yield return new WaitForSeconds(0.25f);
        canUseBlocks = true;
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
            self.SetState(BrickState.Full);
        }
        if (GUILayout.Button("Broken"))
        {
            self.SetState(BrickState.Broken);
        }
        if (GUILayout.Button("Empty"))
        {
            self.SetState(BrickState.Empty);
        }
    }
}
#endif