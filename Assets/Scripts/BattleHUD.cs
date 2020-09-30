using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.name;
        levelText.text = "LV: "+ unit.unitLevel;
        hpSlider.maxValue = (float) unit.maxHP;
        hpSlider.value = (float) unit.currentHP;

    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
    public void UpdateHP(Unit unit)
    {
        StartCoroutine(UpdateHPRoutine(unit));
    }

    public IEnumerator UpdateHPRoutine(Unit unit)
    {
        float beforeAttackHP = hpSlider.value;
        float currentHP = unit.currentHP;
        float difference = beforeAttackHP - currentHP;
    

        for( float i = beforeAttackHP; i>currentHP; i-= difference/20)
        {
            SetHP((int) i);
            yield return new WaitForSeconds(0.05f);
        }


    }


}
