using UnityEngine;

[RequireComponent(typeof(BasicEntity))]
public class B_Health : MonoBehaviour
{
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private GameObject RedBar;
    private Entity entitySelf;
    private int HP;
    private int maxHP;
    [SerializeField] private float scaleBar = 0.1f;
    [SerializeField] private float lengthBar = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entitySelf = GetComponent<BasicEntity>();
        HP = entitySelf.GetHP();
        maxHP = entitySelf.GetMaxHP();
    }

    // Update is called once per frame
    void Update()
    {
        HP = entitySelf.GetHP();
        float scaleBarHP = ((HP * 1f) / (maxHP * 1f)) * scaleBar;
        float lengthBarHP = ((HP*1f) / (maxHP*1f)) * lengthBar;
        float zBarHP = (lengthBar - lengthBarHP) / 2f;

        HealthBar.transform.localPosition = new Vector3(HealthBar.transform.localPosition.x, HealthBar.transform.localPosition.y, zBarHP);
        HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, HealthBar.transform.localScale.y, scaleBarHP);

        float scaleRedBar = (((maxHP - HP) * 1f) / (maxHP * 1f)) * scaleBar;
        float lengthRedBar = lengthBar - lengthBarHP;
        float zRedBar = -(lengthBar - lengthRedBar) / 2f;

        RedBar.transform.localPosition = new Vector3(RedBar.transform.localPosition.x, RedBar.transform.localPosition.y, zRedBar);
        RedBar.transform.localScale = new Vector3(RedBar.transform.localScale.x, RedBar.transform.localScale.y, scaleRedBar);
    }
}
