using UnityEngine;
using System.Collections;

public class WeightsMechanism : MonoBehaviour {

    public float Mass = 1;
    public float DispearTime = 5.0f;

    private float totalTime;
    

	// Use this for initialization
	void Start () 
    {
        this.totalTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!this.renderer.enabled)
        {
            this.totalTime += Time.deltaTime;
            if (this.totalTime >= this.DispearTime)
            {
                this.renderer.enabled = true;
                this.totalTime = 0.0f;
            }
        }

	}

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && this.renderer.enabled)
        {
            switch (GameDefinition.GetIsWeight(m_parent.rigidbody.mass))
            {
                case GameDefinition.Weight.light:
                    m_parent.rigidbody.mass += this.Mass;
                    break;
                case GameDefinition.Weight.heavy:
                    m_parent.rigidbody.mass -= this.Mass;
                    break;
                default:
                    break;
            }
            this.renderer.enabled = false;
        }
    }
}
