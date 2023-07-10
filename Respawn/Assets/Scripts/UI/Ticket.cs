using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class Ticket : MonoBehaviour
{
    public TMP_Text nickName;
    public TMP_Text time;
    public Image loadingBar;

    public float min = 0f;
    public float max = 1f;


    private void Start()
    {
        transform.localScale = Vector3.one * 0.21f;
    }

    public void SetGFX(float percentage)
    {
        float x = Unity.Mathematics.math.remap(0f, 1f, min, max, percentage);

        loadingBar.transform.localPosition = new Vector3(-x *
            loadingBar.rectTransform.rect.width,
            loadingBar.transform.localPosition.y);
    }
}