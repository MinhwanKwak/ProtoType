using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System.Linq;
using BanpoFri;

[System.Serializable]
public class Config : BanpoFri.SingletonScriptableObject<Config>, BanpoFri.ILoader
{
    public static readonly int Slot_MAX = 3;
    public static readonly int BoostBuffIdx = 20000;
    public static readonly int CostumeBuffIdx = 30000;
    public static readonly int RouletteIdx = 10000;

    public enum FarsightedTime
    {
        Morning,
        Afternoon,
        Evening,
        Night,
    }

    public enum DragonStatus
    {
        Egg = 0,
        Hatchling,
        Adult,
    }

    public enum HeroAnimState
    {
        None,
        Attack,
        Attack2,
        Idle,
        Run,
        Win,
        Stop,
    }

    public enum DragonAnimState
    {
        None,
        Idle,
        Run,
        Attack,
    }

    public enum HeroSkillBuffIdx
    {
        Warrior = 301,
        Arture = 302,
        SpearMan = 303,
        Wizard = 304,
        Priest = 305,
    }

    public enum Language
    {
        ko,
        en,
        ja,
        th,
        de,
        fr,
    }

    public enum EnergyType
    {
        EnergyExp = 1001, //exp
        EnergyGem = 1002, //gem
    }

    public enum WardrobeProperty
    {
        Heart = 1,
        Twinkle,
        Gem,
        Wave,
        Cube,
        Star
    }

    public enum WardrobeCategory
    {
        First = 201,
        Makeup = First,
        Hair,
        Top,
        Pants,
        Body,
        Last = Body
    }

    public enum FurnitureCategory
    {
        First = 101,
        Wall = First,
        Floor,
        Carpet,
        Desk,
        Closet,
        Sofa,
        Table,
        WallAcc,
        Last = WallAcc
    }

    public enum RecordCountKeys
    {
        FacilityAuto,
        BuyInAppCount = 2,
        Cabinet = 3,
        PentHouseTicketCount = 4,
        StyleHeartCount = 5,
        SetCollectionCount = 8,
        GrowthCabinetWardrobeAd = 9,
        GrowthCabinetmanagerAd = 10,
        FirstInAppShopCurrency,

        BuyInAppCountTotal = 95,
        BuyVVIPInAppStageIdx = 96,
        BuyVIPInAppStageIdx = 97,
        BuyInAppCountByIdx = 99,

        SeasonalJoin = 200,
        EventTicketAddCount = 201,


        ExpeditionAttackCount = 202,
        ExpeditionCount = 203,
        DragonDumpInit = 1000,
        DragonHuntCount = 1001,
        DragonHuntChallengeCount = 1002,
    }

    public enum FacilityType
    {
        Enter = 1,
        Manufacture,
        Exit,
        Gacha
    }

    public enum RewardType
    {
        CurrencyGroup = 1,
        RewardGroup = 2,
        ManagerGroup = 3,
        DragonCaveGroup = 4,
        EventSpecialQuest = 1001,
    }

    public enum RewardGroup
    {
        Currency = 1,
        RandomManager = 2,
        Manager = 3,
        DragonCave = 4,
        DragonCaveRandomManager = 5,

        DropBox = 996,
        EventSeasonalMainReward = 997,
        EventBuffCat = 998,
        StageClearBox = 999,
    }

    public enum StageClearType
    {
        Choice = 101,
        NoneChoice = 102,
    }

    public enum ItemType
    {
        ADTicket = 0,
        SilverTicket = 50001,
        GoldTicket = 50002,
        DeliveryTicket = 50003,
        nocturnalTicket = 50004,
        diurnalTicket = 50005,
        Mileage = 99999,
    }

    public enum CardType
    {
        ADPack = 100001,
        SilverPack = 100002,
        GoldPack = 100003,
        DeliveryPack = 100004,
        nocturnalPack = 100005,
        diurnalPack = 100006,
        EventADPack = 1100001,
        EventSilverPack = 1100002,
        EventGoldPack = 1100003,
        EventDeliveryPack = 1100004,
        EventNocturnalPack = 1100005,
        EventDiurnalPack = 1100006,
    }

    public enum UseCashLogType
    {
        Done,
        SilverCabinet = 11,
        GoldCabinet = 21,
        GoldCabinetx10,
        Money,
        Material,
        PenthouseTicket,
        DirectPurchase,
        ManagerShopReset,
        PenthouseShopReset,
        MagicChancePackage,
        CostumeChange,
        DirectWardrobePurchase,
        Event_SilverCabinet = 100,
        Event_GoldCabinet,
        Event_Purchase_RouletteManager_Normal = 201,
        Event_Purchase_RouletteManager_Epic,
        Event_Purchase_RouletteManager_Legend,
        Event_RouleteManager_Reset,
        Event_Purchase_RouletteCoin = 301,
        Event_Purchase_Money,
        Event_Purchase_Material,
        Event_SpecialCat_Upgrade = 401,
        Event_DirectPurchase = 501,
        AdCabinet = 44,
    }

    public enum CurrencyID
    {
        StoreMoney = 9999,
        Money = 1,
        Material = 2,
        Cash = 3,
        FeverCoin = 4,
        Medal = 5,
        SilverDump = 6,
        GoldDump = 7,
        EventMoney = 101,
        EventMaterial = 102,
        EventMedal = 103,
        GachaCoin = 104,
        ClearBox = 999,
        EnergyFly = 1001,
        EnergyLightning = 1002,
        ConquestTicket = 10001,
        DragonTicket = 10002,
        PropertyTicket = 10003
    }

    public enum ManagerGrade
    {
        Noraml = 1,
        Rare = 2,
        Unique = 3,
        UnCommon = 0,
    }

    public enum ManagerType
    {
        AllManager = 0,
        Delivery = 1,
        Moon = 2,
        Sun = 3,
        EventGacha = 4
    }

    public enum ContentsOpenType
    {
        manager_equip_slot2,
        manager_equip_slot3,
        event_open,
        card_inven,
        review,
        play_start,
        manager,
        booster,
        manager_upgrade,
        shop,
        mission,
        adfly_open,
        mystery_box,
        interstitial,
        tuto_popup,
        set_collection,
        energy_event,
        seasonal_event,
        offline_reward,
        expedition_conquest,
        save_quest_reward,
        package_open,
        monster_buff,
        dragon_cave,
        dragon_hunt,
        expedition_elements,
        expedition_herostory,
        dragon_egg_box,
        attendance,
        achievements,
        none = -1,
    }

    [System.Serializable]
    public class FarsightedObject
    {
        public List<Config.FarsightedTime> time;
        public GameObject obj;
        public bool On;
    }

    [System.Serializable]
    public class RewardItemData
    {
        public int rewardType;
        public int rewardIdx;
        public System.Numerics.BigInteger rewardValue;

        public RewardItemData(int _type, int _idx, System.Numerics.BigInteger _count)
        {
            this.rewardType = _type;
            this.rewardIdx = _idx;
            this.rewardValue = _count;
        }
    }


    [System.Serializable]
    public class ColorDefine
    {
        public string key_string;
        public Color color;
    }

    [HideInInspector]
    [SerializeField]
    private List<ColorDefine> _textColorDefines = new List<ColorDefine>();
    [HideInInspector]
    [SerializeField]
    private List<ColorDefine> _eventTextColorDefines = new List<ColorDefine>();
    private Dictionary<string, Color> _textColorDefinesDic = new Dictionary<string, Color>();
    public List<ColorDefine> TextColorDefines {
        get {
            return _textColorDefines;
        }
    }
    public List<ColorDefine> EventTextColorDefines {
        get {
            return _eventTextColorDefines;
        }
    }

    [HideInInspector]
    [SerializeField]
    private List<ColorDefine> _imageColorDefines = new List<ColorDefine>();
    [HideInInspector]
    [SerializeField]
    private List<ColorDefine> _eventImgaeColorDefines = new List<ColorDefine>();
    private Dictionary<string, Color> _imageColorDefinesDic = new Dictionary<string, Color>();
    public List<ColorDefine> ImageColorDefines {
        get {
            return _imageColorDefines;
        }
    }
    public List<ColorDefine> EventImageColorDefines {
        get {
            return _eventImgaeColorDefines;
        }
    }

    public Material SkeletonGraphicMat;
    public Material DisableSpriteMat;
    public Material EnableSpriteMat;
    public Material ImgAddtiveMat;
    //[SerializeField]
    //private SpriteAtlas productAtlas;
    [SerializeField]
    private SpriteAtlas managerAtlas;
    //[SerializeField]
    //private SpriteAtlas toyAtlas;
    [SerializeField]
    private SpriteAtlas slotAtlas;
    [SerializeField]
    private SpriteAtlas DynamicAtlas;
    [SerializeField]
    private SpriteAtlas DynamicShopAtlas;
    [SerializeField]
    private SpriteAtlas ItemCardAtlas;
    [SerializeField]
    private SpriteAtlas CommonAtlas;
    [SerializeField]
    private SpriteAtlas facilityAtlas;
    [SerializeField]
    private SpriteAtlas furnitureIconAtlas;
    [SerializeField]
    private SpriteAtlas wardrobeIconAtlas;
    [SerializeField]
    private SpriteAtlas costumeAtlas;
    [SerializeField]
    private SpriteAtlas PentHousePreviewAtlas;
    [SerializeField]
    private SpriteAtlas CabinetBoxAtlas;
    [SerializeField]
    private SpriteAtlas competitionAtlas;
    [SerializeField]
    private SpriteAtlas SpecialCatAtlas;
    [SerializeField]
    private SpriteAtlas UiIconAtlas;
    [SerializeField]
    private SpriteAtlas UiBgAtlas;
    [SerializeField]
    private SpriteAtlas Seasonal_10001_ImgAtlas;
    [SerializeField]
    private SpriteAtlas SeasonalCommonImgAtlas;
    [SerializeField]
    private SpriteAtlas SeasonalFacilityImgAtlas;
    [SerializeField]
    private SpriteAtlas ExpeditionAreaAtlas;
    [SerializeField]
    private SpriteAtlas OneLinkAtlas;
    [SerializeField]
    private SpriteAtlas DragonHuntAtlas;
    [SerializeField]
    private SpriteAtlas DragonEggAtlas;


    public Color GetTextColor(string key)
    {
        if (_textColorDefinesDic.ContainsKey(key))
            return _textColorDefinesDic[key];

        return Color.white;
    }


    public Color GetImageColor(string key)
    {
        if (_imageColorDefinesDic.ContainsKey(key))
            return _imageColorDefinesDic[key];

        return Color.white;
    }

    public Color GetManagerGradeBGColor(ManagerGrade grade)
    {
        switch (grade)
        {
            case ManagerGrade.Noraml:
                return GetImageColor("Card_Normal_Bg");
            case ManagerGrade.Rare:
                return GetImageColor("Card_Rare_Bg");
            case ManagerGrade.Unique:
                return GetImageColor("Card_Unique_Bg");
        }

        return Color.white;
    }

    public Color GetManagerGradeFrameColor(ManagerGrade grade)
    {
        switch (grade)
        {
            case ManagerGrade.Noraml:
                return GetImageColor("Card_Normal_Frame");
            case ManagerGrade.Rare:
                return GetImageColor("Card_Rare_Frame");
            case ManagerGrade.Unique:
                return GetImageColor("Card_Unique_Frame");
        }

        return Color.white;
    }
    public Sprite GetItemcardImg(string key)
    {
        return ItemCardAtlas.GetSprite(key);
    }

    public Sprite GetCabinetBoxImg(string key)
    {
        return CabinetBoxAtlas.GetSprite(key);
    }

    public Sprite GetDragonHuntImg(string key)
    {
        return DragonHuntAtlas.GetSprite(key);
    }

    public Sprite GetIUiIconImg(string key)
    {
        return UiIconAtlas.GetSprite(key);
    }

    public Sprite GetUIBgImg(string key)
    {
        return UiBgAtlas.GetSprite(key);
    }

    public Sprite GetSeasonalFacilityImg(string key)
    {
        return SeasonalFacilityImgAtlas.GetSprite(key);
    }

    public Sprite GetSpecialCatImg(string key)
    {
        return SpecialCatAtlas.GetSprite(key);
    }

    public Sprite GetPentHousePreviewImg(string key)
    {
        return PentHousePreviewAtlas.GetSprite(key);
    }

    public Sprite GetCommonImg(string key)
    {
        return CommonAtlas.GetSprite(key);
    }

    public Sprite GetDragonEggImg(string key)
    {
        return DragonEggAtlas.GetSprite(key);
    }


    public Sprite GetDynamicImg(string key)
    {
        return DynamicAtlas.GetSprite(key);
    }

    public Sprite GetDynamicShop(string key)
    {
        return DynamicShopAtlas.GetSprite(key);
    }


    public Sprite GetExpeditionBg(string key)
    {
        return ExpeditionAreaAtlas.GetSprite(key);
    }

    //public Sprite GetProductImg(string key)
    //{
    //    return productAtlas.GetSprite(key);
    //}

    public Sprite GetManagerImg(string key)
    {
        return managerAtlas.GetSprite(key);
    }
    
    //public Sprite GetToyImg(string key)
    //{
    //    return toyAtlas.GetSprite(key);
    //}
    public Sprite GetSlotImg(string key)
	{
        return slotAtlas.GetSprite(key);
    }
    public Sprite GetFacilityImg(string key)
    {
        return facilityAtlas.GetSprite(key);
    }

    public Sprite GetFurnitureIconImg(string key)
    {
        return furnitureIconAtlas.GetSprite(key);
    }

    public Sprite GetWardrobeIconImg(string key)
    {
        return wardrobeIconAtlas.GetSprite(key);
    }

    public Sprite GetCostumeIconImg(string key)
    {
        return costumeAtlas.GetSprite(key);
    }

    public Sprite GetCompeitionImg(string key)
    {
        return competitionAtlas.GetSprite(key);
    }

    public Sprite GetSeasonalImg(int stage , string key)
    {
        if (stage == 10001)
            return Seasonal_10001_ImgAtlas.GetSprite(key);
        else
            return SeasonalCommonImgAtlas.GetSprite(key);

    }
    public Sprite GetOneLinkImg(string key)
    {
        return OneLinkAtlas.GetSprite(key);
    }

    public Sprite GetNotiCardPackImage(int cardidx)
    {
        switch(cardidx)
        {
            case (int)CardType.ADPack:
                return GetDynamicImg("Dynamic_Icon_Cardpack_Ads");
            case (int)CardType.SilverPack:
                return GetDynamicImg("Dynamic_Icon_Cardpack_Silver");
            case (int)CardType.GoldPack:
                return GetDynamicImg("Dynamic_Icon_Cardpack_Gold");
            case (int)CardType.DeliveryPack:
                return GetDynamicImg("Dynamic_Icon_Cardpack_Trade");
            case (int)CardType.diurnalPack:
                return GetDynamicImg("Dynamic_Icon_Cardpack_Sun");
            case (int)CardType.nocturnalPack:
                return GetDynamicImg("Dynamic_Icon_Cardpack_Moon");
        }

        return null;
    }

    public Sprite GetCurrencyImg(CurrencyID id)
    {
        switch(id)
        {
            case CurrencyID.Money:
                return GetDynamicImg("Dynamic_Icon_Btn_GameMoney");
            case CurrencyID.Cash:
                return GetDynamicImg("Dynamic_Icon_Btn_Gem");
            case CurrencyID.Material:
                return GetDynamicImg("Dynamic_Icon_Btn_Material");
            case CurrencyID.GachaCoin:
                return GetDynamicImg("Dynamic_Icon_Seasonal_RouletteCoin");
            case CurrencyID.EventMoney:
                return GetDynamicImg("Dynamic_Icon_Seasonal_Money");
            case CurrencyID.EventMaterial:
                return GetDynamicImg("Dynamic_Icon_Seasonal_Material");
            case CurrencyID.Medal:
                return GetDynamicImg("Dynamic_Icon_Medal");
            case CurrencyID.EventMedal:
                return GetDynamicImg("Dynamic_Icon_Seasonal_Ticket");
            case CurrencyID.SilverDump:
                return GetDragonHuntImg("DragonHunt_Icon_Dragon_Food_2");
            case CurrencyID.GoldDump:
                return GetDragonHuntImg("DragonHunt_Icon_Dragon_Food_1");
            case CurrencyID.EnergyLightning:
                return GetDynamicImg("Dynamic_Icon_Btn_Energy_2");
            case CurrencyID.EnergyFly:
                return GetDynamicImg("Dynamic_Icon_Btn_Energy_1");
            case CurrencyID.ConquestTicket:
                return GetDynamicImg("Dynamic_Icon_ExpeditionTicket_Conquest");
            case CurrencyID.DragonTicket:
                return GetDynamicImg("Dynamic_Icon_ExpeditionTicket_Dragon");
            case CurrencyID.PropertyTicket:
                return GetDynamicImg("Dynamic_Icon_ExpeditionTicket_Elements");

        }
        return null;
    }

    public void Load()
    {
        _textColorDefinesDic.Clear();
        foreach(var cd in _textColorDefines)
        {
            _textColorDefinesDic.Add(cd.key_string, cd.color);
        }
        foreach(var cd in _eventTextColorDefines)
        {
            _textColorDefinesDic.Add(cd.key_string, cd.color);
        }
        _imageColorDefinesDic.Clear();
        foreach(var cd in _imageColorDefines)
        {
            _imageColorDefinesDic.Add(cd.key_string, cd.color);
        }
        foreach(var cd in _eventImgaeColorDefines)
        {
            _imageColorDefinesDic.Add(cd.key_string, cd.color);
        }
    }

    public string GetManagerTypeConvert(int _key)
    {
        string returnvalue = "";
        switch (_key)
        {

            case (int)Config.ManagerType.Delivery:
                returnvalue = "Dynamic_Icon_Type_Sun";
                break;
            case (int)Config.ManagerType.Moon:
                returnvalue = "Dynamic_Icon_Type_Moon";
                break;
            case (int)Config.ManagerType.Sun:
                returnvalue = "Dynamic_Icon_Type_Trade";
                break;
        }

        return returnvalue;
    }

    public Sprite GetManagerBottomImg(ManagerGrade grade)
    {
        switch(grade)
        {
            case ManagerGrade.Noraml:
                return GetDynamicImg("Dynamic_Bg_ManagerPlace_Normal");
            case ManagerGrade.Rare:
                return GetDynamicImg("Dynamic_Bg_ManagerPlace_Rare");
            case ManagerGrade.Unique:
                return GetDynamicImg("Dynamic_Bg_ManagerPlace_Unique");
        }

        return null;
    }

    public Sprite GetManagerFrameImg(ManagerGrade grade)
    {
        switch(grade)
        {
            case ManagerGrade.Noraml:
                return GetItemcardImg("ItemCard_Frame_Card_Normal_Back");
            case ManagerGrade.Rare:
                return GetItemcardImg("ItemCard_Frame_Card_Rare_Back");
            case ManagerGrade.Unique:
                return GetItemcardImg("ItemCard_Frame_Card_Unique_Back");
        }

        return null;
    }
}
