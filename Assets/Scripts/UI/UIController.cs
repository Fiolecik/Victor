using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Windows/Popus)")]
    [SerializeField] UIInventory playerInventory;
    [SerializeField] UIItemDescription description;
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText, coinText;

    public GameObject deathScreen;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;

    public string newGameScene, mainMenuScene;

    public GameObject pausedMenu, mapDisplay, bigMapText, equipement;

    public Image currentGun;
    public Text gunText;

    public Slider bossHealthBar;
    public Canvas mainCanvas;

	private void Awake()
	{
        instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;

        currentGun.sprite = PlayerController.instance.gameObject.GetComponent<WeaponController>().getGun().gunUI;
        gunText.text = PlayerController.instance.gameObject.GetComponent<WeaponController>().getGun().weaponName;
        //currentGun.sprite = PlayerController.instance.avalaibleGuns[PlayerController.instance.currentGun].gunUI;
        //gunText.text = PlayerController.instance.avalaibleGuns[PlayerController.instance.currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOutBlack)
		{
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
			{
                fadeOutBlack = false;
			}
		}

        if(fadeToBlack)
		{
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
	{
        fadeToBlack = true;
        fadeOutBlack = false;
	}

    public void NewGame()
	{
        Time.timeScale = 1f;

        SceneManager.LoadScene(newGameScene);

        Destroy(PlayerController.instance.gameObject);
    }

    public void ReturnToMainMenu()
	{
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);

        Destroy(PlayerController.instance.gameObject);
	}

    public void Resume()
	{
        LevelManager.instance.PauseUnpause();
	}

    #region Controling_Inventory

    public void showDescription(Item i)
    {
        description.gameObject.SetActive(true);
        description.showDescription(i);
    }

    public void hideDescription()
    {
        description.hideDescription();
    }

    public void openInventory(Inventory inv)
    {
        playerInventory.gameObject.SetActive(true);
        playerInventory.open(inv);
    }

    public void switchInventory(Inventory inv)
    {
        playerInventory.gameObject.SetActive(!playerInventory.gameObject.activeSelf);

        if (playerInventory.gameObject.activeSelf)
        {
            playerInventory.open(inv);
            PlayerController.instance.canMove = false;
        }
        else
        {
            closeInventory();
            PlayerController.instance.canMove = true;
        }
            
    }

    public void closeInventory()
    {
        playerInventory.gameObject.SetActive(false);
        description.hideDescription();
    }
    #endregion
}
