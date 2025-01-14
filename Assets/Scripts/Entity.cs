using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected bool isActive;
    [SerializeField] protected bool isEnemy;
    [SerializeField] protected bool isKing;
    [SerializeField] public enum Behavior {Neutral, Offense, Defense};
    [SerializeField] public Behavior behavior;

    [SerializeField] protected int hp;
    [SerializeField] protected int maxHP;
    [SerializeField] protected int def;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected int attack;
    [SerializeField] protected int mana;
    [SerializeField] protected int maxMana;
    [SerializeField] protected int cost;
    [SerializeField] protected float range;

    public abstract bool GetIsActive(); // R�cup�re si un personnage est actif ou pas sur la carte (ie il fait rien ou il joue)
    public abstract void SetIsActive(bool newIsActive);
    public abstract bool GetIsEnemy(); // Est ce que l'entity est un ennemi ?
    public abstract void SetIsEnemy(bool newIsEnemy);
    public abstract bool GetIsKing();
    public abstract void SetIsKing(bool newIsKing);

    public abstract Behavior GetBehavior();
    public abstract void SetBehavior(Behavior newBehavior);
   

    // PV
    public abstract int GetHP();
    public abstract void SetHP(int newHP);

    public abstract int GetMaxHP();
    public abstract void SetMaxHP(int newMaxHP);  

    // D�fense
    public abstract int GetDef();
    public abstract void SetDef(int newDef);

    // Vitesse
    public abstract float GetSpeed(); // La vitesse de d�placement
    public abstract void SetSpeed(float newSpeed);

    // Attaque
    public abstract float GetAttackSpeed();
    public abstract void SetAttackSpeed(float newAttackSpeed);
    public abstract int GetAttack();
    public abstract void SetAttack(int newAttack);

    // Mana
    public abstract int GetMana();
    public abstract void SetMana(int newMana);
    public abstract int GetMaxMana();
    public abstract void SetMaxMana(int newMaxMana);

    // Co�t
    public abstract int GetCost(); // Le co�t � payer pour placer l'entity sur l'ar�ne
    public abstract void SetCost(int newCost);

    // Port�e
    public abstract float GetRange(); 
    public abstract void SetRange(float newRange);

    // Pour les capacit�s sp�ciales : Les diff�rentes capacit�s sont des components qui h�ritent de la classe "Capacit�".
    // => Pour donner une capacit� sp�ciale � une unit� il faut lui donner le component (script) qui correspond � la capacit� sp�ciale choisie

    // Les classes "HealthManager", les classes d'attaques, etc vont d�pendre de la classe des personnages (qui h�ritent de "Entity")



}
