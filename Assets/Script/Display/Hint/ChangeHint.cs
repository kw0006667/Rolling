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
            this.hintDisplay.hintTexture = this.ShowHintTexture;
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
        this.hintDisplay = this.transform.parent.GetComponent<HintDisplay>();
    }
}
