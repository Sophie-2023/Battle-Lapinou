using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BasicEntity))]
public class B_Death : MonoBehaviour
{
    private bool alive = true;
    private bool isDying = false;
    private Entity entitySelf;
    [SerializeField] private float deathDuration = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entitySelf = GetComponent<BasicEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        alive = (entitySelf.GetHP() > 0);
        if ((!alive)&&(!isDying)) {
            StartCoroutine(deathCoroutine());
        }
    }

    public bool isAlive()
    {
        return alive;
    }

    private IEnumerator deathCoroutine()
    {
        isDying = true;
        float t = 0;
        Color Initialcolor = this.GetComponent<MeshRenderer>().material.color;
        while (t<1)
        {
            t += Time.deltaTime/deathDuration;
            Color newColor = Initialcolor;
            newColor.a = Mathf.Lerp(0.0f, 1.0f, t);
            this.GetComponent<MeshRenderer>().material.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
