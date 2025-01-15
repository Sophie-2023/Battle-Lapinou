using UnityEngine;

[RequireComponent(typeof(BasicEntity))]
public class B_Mana : MonoBehaviour
{
    [SerializeField] private GameObject ManaBar;
    [SerializeField] private GameObject RedBar;
    private Entity entitySelf;
    private int Mana;
    private int maxMana;
    [SerializeField] private float scaleBar = 0.1f;
    [SerializeField] private float lengthBar = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entitySelf = GetComponent<BasicEntity>();
        Mana = entitySelf.GetMana();
        maxMana = entitySelf.GetMaxMana();
    }

    // Update is called once per frame
    void Update()
    {
        displayManaBar();
    }

    private void displayManaBar()
    {
        Mana = entitySelf.GetMana();
        float scaleBarMana = ((Mana * 1f) / (maxMana * 1f)) * scaleBar;
        float lengthBarMana = ((Mana * 1f) / (maxMana * 1f)) * lengthBar;
        float zBarMana = (lengthBar - lengthBarMana) / 2f;

        ManaBar.transform.localPosition = new Vector3(ManaBar.transform.localPosition.x, ManaBar.transform.localPosition.y, zBarMana);
        ManaBar.transform.localScale = new Vector3(ManaBar.transform.localScale.x, ManaBar.transform.localScale.y, scaleBarMana);

        float scaleRedBar = (((maxMana - Mana) * 1f) / (maxMana * 1f)) * scaleBar;
        float lengthRedBar = lengthBar - lengthBarMana;
        float zRedBar = -(lengthBar - lengthRedBar) / 2f;

        RedBar.transform.localPosition = new Vector3(RedBar.transform.localPosition.x, RedBar.transform.localPosition.y, zRedBar);
        RedBar.transform.localScale = new Vector3(RedBar.transform.localScale.x, RedBar.transform.localScale.y, scaleRedBar);
    }
}
