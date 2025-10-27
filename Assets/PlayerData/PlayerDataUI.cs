using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataUI : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider manaBar;
    [SerializeField] private float _lerpDuration = 0.3f;

    private Coroutine _hpCoroutine;
    private Coroutine _manaCoroutine;

    void Start()
    {
        playerData.OnMaxHpChanged += MaxHpUpdate;
        playerData.OnMaxManaChanged += MaxManaUpdate;
        playerData.OnHpChanged += HpUpdate;
        playerData.OnManaChanged += ManaUpdate;
    }


    public void MaxHpUpdate(float maxHp)
    {
        hpBar.maxValue = maxHp;
    }
    public void MaxManaUpdate(float maxMana)
    {
        manaBar.maxValue = maxMana;
    }
    public void HpUpdate(float hp)
    {
        if (_hpCoroutine != null) StopCoroutine(_hpCoroutine);
        _hpCoroutine = StartCoroutine(SmoothUpdateValue(hpBar, hp));
    }
    public void ManaUpdate(float mana)
    {
        if (_manaCoroutine != null) StopCoroutine(_manaCoroutine);
        _hpCoroutine = StartCoroutine(SmoothUpdateValue(manaBar, mana));
    }

    private IEnumerator SmoothUpdateValue(Slider slider, float targetValue)
    {
        float startValue = slider.value;
        float elapsedTime = 0f;

        while (elapsedTime < _lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _lerpDuration);

            slider.value = Mathf.Lerp(startValue, targetValue, t);
            yield return null;
        }

        slider.value = targetValue;
    }

    void OnDestroy()
    {
        playerData.OnMaxHpChanged -= MaxHpUpdate;
        playerData.OnMaxManaChanged -= MaxManaUpdate;
        playerData.OnHpChanged -= HpUpdate;
        playerData.OnManaChanged -= ManaUpdate;
    }


}
