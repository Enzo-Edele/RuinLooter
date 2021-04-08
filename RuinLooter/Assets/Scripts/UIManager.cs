using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    HealthBarController HealthBar;
    [SerializeField]
    GameObject HP;
    [SerializeField]
    Sprite HPEgalUn;
    [SerializeField]
    Sprite HPEgalDeux;
    [SerializeField]
    Sprite HPEgalTrois;
    [SerializeField]
    Sprite HPEgalQuatre;
    [SerializeField]
    Sprite HPEgalCinq;
    [SerializeField]
    Sprite HPEgalSix;

    [SerializeField]
    TMP_Text Coin;
    [SerializeField]
    GameObject laBanque;

    ProgressionArtefactController artefactProgress;
    [SerializeField]
    GameObject artefactFragments;
    [SerializeField]
    Sprite artefactStep0;
    [SerializeField]
    Sprite artefactStep1;
    [SerializeField]
    Sprite artefactStep2;
    [SerializeField]
    Sprite artefactStep3;

    ItemSlotController slot;
    [SerializeField]
    GameObject Slot;
    [SerializeField]
    Sprite empty;
    [SerializeField]
    Sprite cape;
    [SerializeField]
    Sprite potion;
    [SerializeField]
    Sprite bandage;
    [SerializeField]
    Sprite pearl;
    [SerializeField]
    Sprite shield;

    [SerializeField]
    TMP_Text Level;
    [SerializeField]
    GameObject fullPopup;
    float timerPopup = 5.0f;
    float timePopup;
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject endMenu;
    [SerializeField]
    GameObject menuScreen;
    [SerializeField]
    GameObject loadScreen;
    [SerializeField]
    GameObject deathScreen;
    [SerializeField]
    GameObject victoryScreen;
    [SerializeField]
    GameObject artAnimArmature;
    [SerializeField]
    GameObject artAnimGear;
    [SerializeField]
    GameObject artAnimCadran;
    [SerializeField]
    GameObject artAnimAiguilles;
    [SerializeField]
    GameObject animArtefact;
    bool endLevel = false;
    bool premierePhase = false;
    float speedAnimEndLevel = 100.0f;

    [SerializeField]
    GameObject minimapPrefab;
    GameObject minimap;
    Vector2 positionMinimap = new Vector2(1580, 900);

    public readonly string tuto = "Rapporter 3 morceaux d'artefact à la machine.";
    public readonly string nextLevel = "Vous avez rassembler les artefact";

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is NULL");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        this.Deactivate();
    }
    private void Update()
    {
        if(timePopup >= 0)
        {
            timePopup -= Time.deltaTime;
        }
        else
        {
            if(fullPopup != null)
            {
                fullPopup.SetActive(false);
            }
        }
        if (endLevel)
        {
            this.EndLevelAnimation();
        }
    }
    public void UpdateAll(int health, int coin, int artefact, string item)
    {
        this.UpdateHealth(health);
        this.UpdateCoin(coin);
        this.UpdateArtefact(artefact);
        this.UpdateSlot(item);
        this.UpdateLevel();
    }
    public void Deactivate()
    {
        if (HP != null)
        {
            HP.SetActive(false);
        }
        if(laBanque != null)
        {
            laBanque.SetActive(false);
        }
        if(artefactFragments != null)
        {
            artefactFragments.SetActive(false);
        }
        if(Slot != null)
        {
            Slot.SetActive(false);
        }
        Level.text = "";
    }
    public void UpdateHealth(int health)
    {
        HP.SetActive(true);
        HealthBar = FindObjectOfType(typeof(HealthBarController)) as HealthBarController;
        if(health == 5)
        {
            HealthBar.actualSprite = HPEgalSix;
        }
        else if (health == 4)
        {
            HealthBar.actualSprite = HPEgalCinq;
        }
        else if (health == 3)
        {
            HealthBar.actualSprite = HPEgalQuatre;
        }
        else if (health == 2)
        {
            HealthBar.actualSprite = HPEgalTrois;
        }
        else if (health == 1)
        {
            HealthBar.actualSprite = HPEgalDeux;
        }
        else if (health == 0)
        {
            HealthBar.actualSprite = HPEgalUn;
        }
    }
    public void UpdateCoin(int coin)
    {
        laBanque.SetActive(true);
        Coin.text = " : " + coin;
    }
    public void UpdateArtefact(int artefact)
    {
        artefactFragments.SetActive(true);
        artefactProgress = FindObjectOfType(typeof(ProgressionArtefactController)) as ProgressionArtefactController;
        if (artefact == 0)
        {
            artefactProgress.actualSprite = artefactStep0;
        }
        else if (artefact == 1)
        {
            artefactProgress.actualSprite = artefactStep1;
        }
        else if (artefact == 2)
        {
            artefactProgress.actualSprite = artefactStep2;
        }
        else if (artefact == 3)
        {
            artefactProgress.actualSprite = artefactStep3;
        }
    }
    public void UpdateSlot(string item)
    {
        GameManager.Instance.UpdateItem(item);
        Slot.SetActive(true);
        slot = FindObjectOfType(typeof(ItemSlotController)) as ItemSlotController;
        if(item == "Empty")
        {
            slot.actualSprite = empty;
        }
        else if (item == "Cape")
        {
            slot.actualSprite = cape;
        }
        else if (item == "Potion")
        {
            slot.actualSprite = potion;
        }
        else if (item == "Bandage")
        {
            slot.actualSprite = bandage;
        }
        else if (item == "Pearl")
        {
            slot.actualSprite = pearl;
        }
        else if (item == "Shield")
        {
            slot.actualSprite = shield;
        }
    }
    public void UpdateLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 17)
        {
            Level.text = "Level : Tuto";
        }
        else
        {
            Level.text = "Level : " + (SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    public void Full()
    {
        fullPopup.SetActive(true);
        timePopup = timerPopup;
    }
    public void DeathUI()
    {
        deathScreen.SetActive(true);
        endMenu.SetActive(true);
    }
    public void VictoryUI()
    {
        victoryScreen.SetActive(true);
        endMenu.SetActive(true);
    }
    public void ActiveMenuScreen()
    {
        menuScreen.SetActive(true);
    }
    public void DeactivateMenuScreen()
    {
        if (menuScreen != null)
        {
            menuScreen.SetActive(false);
        }
    }
    public void ActiveLoad()
    {
        loadScreen.SetActive(true);
    }
    public void DeactivateLoad()
    {
        if (loadScreen != null)
        {
            loadScreen.SetActive(false);
        }
    }
    public void ActiveMainMenu()
    {
        mainMenu.SetActive(true);
    }
    public void DeactivateMainMenu()
    {
        if (mainMenu != null)
        {
            mainMenu.SetActive(false);
        }
    }
    public void PauseUIOn()
    {
        pauseMenu.SetActive(true);
    }
    public void PauseUIOff()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        if (endMenu != null)
        {
            endMenu.SetActive(false);
        }
        if(victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }
        if(deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
    }
    public void ResumeButton()
    {
        InputManager.Instance.UnpauseState();
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void MainMenuButton()
    {
        this.PauseUIOff();
        InputManager.Instance.ReturnMenu();
        this.Deactivate();
        this.ActiveMenuScreen();
        this.ActiveMainMenu();
        this.DegenerateMinimap();
    }
    public void OptionButton()
    {
        Debug.Log("Option");
        this.DeactivateMainMenu();
        this.PauseUIOff();
    }
    public void StartButton()
    {
        InputManager.Instance.UnpauseState();
        InputManager.Instance.StartGame();
        this.DeactivateMenuScreen();
        this.DeactivateMainMenu();
    }
    public void TutoButton()
    {
        InputManager.Instance.UnpauseState();
        SceneManager.LoadScene("tuto");
        this.DeactivateMenuScreen();
        this.DeactivateMainMenu();
    }
    public void LaunchEndLevelAnimation()
    {
        endLevel = true;
        premierePhase = true;
        InputManager.Instance.pause = true;
        this.DegenerateMinimap();
    }
    public void EndLevelAnimation()
    {
        animArtefact.SetActive(true);
        Vector2 anim1 = artAnimArmature.transform.position;
        Vector2 anim2 = artAnimGear.transform.position;
        Vector2 anim3 = artAnimCadran.transform.position;
        Vector2 anim4 = artAnimAiguilles.transform.position;
        if (premierePhase)
        {
            if (anim1.x < 1285)
            {
                anim1.x += Time.deltaTime * speedAnimEndLevel * 2;
            }
            if (anim2.x < 1085)
            {
                anim2.x += Time.deltaTime * speedAnimEndLevel;
            }
            if (anim3.x > 835)
            {
                anim3.x -= Time.deltaTime * speedAnimEndLevel;
            }
            if (anim4.x > 635)
            {
                anim4.x -= Time.deltaTime * speedAnimEndLevel * 2;
            }
            if(anim1.y < 700)
            {
                anim1.y += Time.deltaTime * speedAnimEndLevel * 3;
                anim2.y += Time.deltaTime * speedAnimEndLevel * 3;
                anim3.y += Time.deltaTime * speedAnimEndLevel * 3;
                anim4.y += Time.deltaTime * speedAnimEndLevel * 3;
            }
        }
        if (!premierePhase)
        {
            if (anim1.x > 960)
            {
                anim1.x -= Time.deltaTime * speedAnimEndLevel * 2;
            }
            if (anim2.x > 968)
            {
                anim2.x -= Time.deltaTime * speedAnimEndLevel;
            }
            if (anim3.x < 982)
            {
                anim3.x += Time.deltaTime * speedAnimEndLevel;
            }
            if (anim4.x < 989)
            {
                anim4.x += Time.deltaTime * speedAnimEndLevel * 2;
            }
        }
        artAnimArmature.transform.position = anim1;
        artAnimGear.transform.position = anim2;
        artAnimCadran.transform.position = anim3;
        artAnimAiguilles.transform.position = anim4;
        if (anim1.x > 1285 && anim2.x > 1085 && anim3.x < 835 && anim4.x < 635)
        {
            premierePhase = false;
        }
        if (anim1.x < 960 && anim2.x < 968 && anim3.x > 982 && anim4.x > 989)
        {
            InputManager.Instance.pause = false;
            endLevel = false;
            Vector2 posInitial = new Vector2(960, 190);
            anim1 = posInitial;
            anim2 = posInitial;
            anim3 = posInitial;
            anim4 = posInitial;
            anim2.x = anim1.x + 8;
            anim3.x = anim1.x + 22;
            anim4.x = anim1.x + 28;
            this.DeactivateLoad();
            animArtefact.SetActive(false);
        }
        artAnimArmature.transform.position = anim1;
        artAnimGear.transform.position = anim2;
        artAnimCadran.transform.position = anim3;
        artAnimAiguilles.transform.position = anim4;
    }
    public void GenerateMinimap()
    {
        if (minimap == null)
        {
            minimap = Instantiate(minimapPrefab, positionMinimap, Quaternion.identity,GameObject.FindGameObjectWithTag("canvas").transform);
            RoomTemplates scriptMap = FindObjectOfType(typeof(RoomTemplates)) as RoomTemplates;
            scriptMap.Invoke("Minimap", 0.025f * 32);
        }
    }
    public void DegenerateMinimap()
    {
        if (minimap != null)
        {
            Destroy(minimap);
        }
    }
}
