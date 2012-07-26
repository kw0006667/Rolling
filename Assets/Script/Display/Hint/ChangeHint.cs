using UnityEngine;
using System.Collections;

public class ChangeHint : MonoBehaviour
{
    public Texture ShowHintTexture;
    private HintDisplay hintDisplay;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            hintDisplay.hintTexture = ShowHintTexture;
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
        hintDisplay = transform.parent.GetComponent<HintDisplay>();
    }
}
