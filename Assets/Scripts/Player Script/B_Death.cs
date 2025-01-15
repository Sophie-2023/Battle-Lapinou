using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BasicEntity))]
public class B_Death : MonoBehaviour
{
    private bool alive = true;
    private bool isDying = false;
    private Entity entitySelf;
    [SerializeField] private float deathDuration = 1;
    [SerializeField] private MeshRenderer HealthBarRenderer;
    [SerializeField] private MeshRenderer ManaBarRenderer;
    [SerializeField] private MeshRenderer ResteManaBarRenderer;
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
        MeshRenderer renderer = this.GetComponent<MeshRenderer>();

        Color InitialcolorHPBar = HealthBarRenderer.material.color;
        Color InitialcolorManaBar = ManaBarRenderer.material.color;
        Color InitialcolorResteManaBar = ResteManaBarRenderer.material.color;
        while (t<1)
        {
            t += Time.deltaTime/deathDuration;
            Color newColor = new Color(Initialcolor.r, Initialcolor.g, Initialcolor.b, Mathf.Lerp(1, 0, t));
            renderer.material.color = newColor;
            Color newColorHPBar = new Color(InitialcolorHPBar.r, InitialcolorHPBar.g, InitialcolorHPBar.b, Mathf.Lerp(1, 0, t));
            HealthBarRenderer.material.color = newColorHPBar;
            Color newColorManaBar = new Color(InitialcolorManaBar.r, InitialcolorManaBar.g, InitialcolorManaBar.b, Mathf.Lerp(1, 0, t));
            ManaBarRenderer.material.color = newColorManaBar;
            Color newColorResteManaBar = new Color(InitialcolorResteManaBar.r, InitialcolorResteManaBar.g, InitialcolorResteManaBar.b, Mathf.Lerp(1, 0, t));
            ResteManaBarRenderer.material.color = newColorResteManaBar;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
