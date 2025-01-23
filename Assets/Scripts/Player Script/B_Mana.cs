using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BasicEntity))]
public class B_Mana : MonoBehaviour
{
    [SerializeField] private GameObject ManaBar;
    [SerializeField] private GameObject RedBar;
    private Entity entitySelf;
    private int Mana;
    private int maxMana;
    private B_SetClass classScript;
    [SerializeField] private float scaleBar = 0.1f;
    [SerializeField] private float lengthBar = 1f;
    [SerializeField] private int manaIncreaseAmount = 5;
    [SerializeField] private float increaseManaDuration = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entitySelf = GetComponent<BasicEntity>();
        classScript = GetComponent<B_SetClass>();
    }

    // Update is called once per frame
    void Update()
    {
        if (entitySelf.GetIsActive())
        {
            maxMana = entitySelf.GetMaxMana();
            displayManaBar();
            SpecialAttack();
        }
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

    public void IncreaseMana()
    {
        Mana = entitySelf.GetMana();
        if (Mana+manaIncreaseAmount>=maxMana)
        {
            Mana = maxMana;
        } else
        {
            Mana += manaIncreaseAmount;
        }
        StartCoroutine(ChangeManaCoroutine(Mana));
    }

    private void SpecialAttack()
    {
        Mana = entitySelf.GetMana();
        if (Mana>=maxMana)
        {
            StartCoroutine(ChangeManaCoroutine(0));
            if (classScript.GetSpecialAttack() is SpecialAttack specialAttack)
            {
                specialAttack.Execute(entitySelf);
            } 
        }
    }

    public void manaDrained()
    {
        Mana = entitySelf.GetMana();
        if (Mana<=(maxMana/2))
        {
            StartCoroutine(ChangeManaCoroutine(0));
        } else
        {
            StartCoroutine(ChangeManaCoroutine(Mana - (maxMana/2) ));
        }
    }

    private IEnumerator ChangeManaCoroutine(int newMana)
    {
        float t = 0;
        int actualMana = entitySelf.GetMana();
        while (t<1)
        {
            t += Time.deltaTime / increaseManaDuration;
            int mana = Mathf.RoundToInt(Mathf.Lerp(actualMana, newMana, t));
            entitySelf.SetMana(mana);
            yield return new WaitForEndOfFrame();
        }
    }
}
