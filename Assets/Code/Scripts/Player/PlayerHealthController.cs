using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public float invincibleLength; 
    private float _invincibleCounter;

    private UIController _uIReference;
    private PlayerController _pCReference;
    private SpriteRenderer _sR;
    private LevelManager _lReference;
    private SlimePlayer _sPReference;


    // Start is called before the first frame update
    void Start()
    {
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
        _pCReference = GetComponent<PlayerController>();
        _sR = GetComponent<SpriteRenderer>();
        _lReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _sPReference = GameObject.Find("Player").GetComponent<SlimePlayer>();
        currentHealth = maxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        //Comprobamos si el contador de invencibilidad aún no está vacío
        if (_invincibleCounter > 0)
        {
            _invincibleCounter -= Time.deltaTime;

            if (_invincibleCounter <= 0)
                _sR.color = new Color(_sR.color.r, _sR.color.g, _sR.color.b, 1f);
        }
    }

    //Método para manejar el daño
    public void DealWithDamage()
    {
        if (_invincibleCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                gameObject.SetActive(false);

                _lReference.RespawnPlayer();
                
                _lReference.RespawnEnemy();
            }
            else
            {
                _invincibleCounter = invincibleLength;
                _sR.color = new Color(_sR.color.r, _sR.color.g, _sR.color.b, .5f);
                _pCReference.Knockback();
            }
            _uIReference.UpdateHealthDisplay();
        }

    }

    public void HealPlayer()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        _uIReference.UpdateHealthDisplay();
    }

    public void DoubleHealPlayer()
    {
        currentHealth = currentHealth + 2;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        _uIReference.UpdateHealthDisplay();
    }

    public void MaxHealPlayer()
    {
        currentHealth = maxHealth;
        _uIReference.UpdateHealthDisplay();
    }
}
