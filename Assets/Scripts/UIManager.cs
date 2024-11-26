using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;
    }

    private bool isReloadingUI;
    public bool GetIsReloadingUI() { return isReloadingUI; }

    [SerializeField] private TMP_Text currentMagazineAmmoText;
    [SerializeField] private TMP_Text totalAmmoText;
    [SerializeField] private Image reloadUI;
    [SerializeField] private float fillAmountOffset;
    [SerializeField] private float alphaOffset;
    [SerializeField] private float startAlpha;

    [SerializeField] private Image healthUI;
    [SerializeField] private Image shielddUI;

    [SerializeField] private Image takeDamageEffectUI;
    [SerializeField] private float fadeAlpha;
    [SerializeField] private float fadeTime;

    // Win UI
    [SerializeField] private Image winUI;

    // Lose UI
    [SerializeField] private Image loseUI;
    [SerializeField] private float fadeAlphaLoseUI;
    [SerializeField] private float fadeTimeLoseUI;

    [SerializeField] private GameObject TurnBackToMenuSceneUI;

    [SerializeField] private List<Image> weaponUIList;

    public void SetCurrentWeaponUI(int SetCurrentWeaponID)
    {
        foreach (var wp in weaponUIList)
        {
            if (weaponUIList.IndexOf(wp) == SetCurrentWeaponID)
            {
                wp.color = new Color(0f, 0f, 0f, 1f);
            }

            else wp.color = new Color(0f, 0f, 0f, 0.5f);
        }
    }

    public void SetHealthUI(float HP)
    {
        healthUI.fillAmount = HP / 100;
        // Debug.Log(HP / 100);
    }

    public void SetShieldUI(float shield)
    {
        shielddUI.fillAmount = shield / 500;
    }

    public void UpdateBulletsHud(int currentMagazineAmmo, int totalAmmo)
    {
        SetCurrentMagazineAmmoText(currentMagazineAmmo);
        SetTotalAmmoText(totalAmmo);
    }

    public void SetCurrentMagazineAmmoText(int currentMagazineAmmo)
    {
        currentMagazineAmmoText.text = currentMagazineAmmo.ToString();
    }
    public void SetTotalAmmoText(int totalAmmo)
    {
        totalAmmoText.text = totalAmmo.ToString();
    }

    public void StartReloadUI(int currentMagazineAmmo, int totalAmmo)
    {
        this.currentMagazineAmmo = currentMagazineAmmo;
        this.totalAmmo = totalAmmo;

        isReloadingUI = true;
        reloadUI.gameObject.SetActive(true);
    }

    public void PlayDamageAnimation()
    {
        StartCoroutine(FadeIn(takeDamageEffectUI.color, fadeTime, fadeAlpha));
        StartCoroutine(FadeOut(takeDamageEffectUI.color, fadeTime, fadeAlpha));
    }

    public void PlayeWinAnimation()
    {
        winUI.gameObject.SetActive(true);
        StartCoroutine(TurnBackToMenuScene());
    }

    public void PlayeLoseAnimation()
    {
        loseUI.gameObject.SetActive(true);
        StartCoroutine(TurnBackToMenuScene());

    }

    IEnumerator TurnBackToMenuScene()
    {
        yield return new WaitForSeconds(3);

        TurnBackToMenuSceneUI.SetActive(true);

        yield return new WaitForSeconds(3);

        TurnBackToMenuSceneUI.SetActive(false);
        winUI.gameObject.SetActive(false);
        loseUI.gameObject.SetActive(false);

        SceneManager.LoadSceneAsync("Loading Scene");
        GameSceneManager.Instance.SetSceneToLoad("Game Menu Scene");
    }

    IEnumerator FadeIn(Color color, float fadeTime, float fadeAlpha)
    {
        float counter = 0f;
        while (counter < fadeTime)
        {
            counter += Time.deltaTime;
            color.a = Mathf.Lerp(0, fadeAlpha, counter / fadeTime);
            takeDamageEffectUI.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOut(Color color, float fadeTime, float fadeAlpha)
    {
        float counter = 0f;
        while (counter < fadeTime)
        {
            counter += Time.deltaTime;
            color.a = Mathf.Lerp(fadeAlpha, 0, counter / fadeTime);
            takeDamageEffectUI.color = color;
            yield return null;
        }
    }

    private int currentMagazineAmmo = 0;
    private int totalAmmo = 0;

    private void Update()
    {

        if (isReloadingUI == true)
        {
            if (reloadUI.fillAmount < 1f)
                reloadUI.fillAmount += fillAmountOffset;
            else
            {
                if (reloadUI.color.a > 0f)
                    reloadUI.color = new Color(1f, 1f, 1f, reloadUI.color.a - alphaOffset);
                else
                {
                    reloadUI.gameObject.SetActive(false);
                    reloadUI.color = new Color(1f, 1f, 1f, startAlpha);
                    reloadUI.fillAmount = 0;

                    isReloadingUI = false;
                    SetCurrentMagazineAmmoText(currentMagazineAmmo);
                    SetTotalAmmoText(totalAmmo);

                    //Debug.Log("Reloading done!!!");
                }
            }
        }
    }
}