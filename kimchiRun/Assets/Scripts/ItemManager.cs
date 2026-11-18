using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    [SerializeField] public Transform ItemLair;
    [SerializeField] Sprite[] Items;
    [SerializeField] Sprite GoldenItem;
    [SerializeField] ItemScript ItemPrefab;
    [SerializeField] List<ItemScript> ItemPool;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        OnPoolInit();
    }

    private void OnPoolInit()
    {
        ItemPool = new List<ItemScript>();

        for (int i = 0; i < 256; i++)
        {
            ItemScript io = Instantiate<ItemScript>(ItemPrefab, ItemLair);
            io.gameObject.SetActive(false);
            ItemPool.Add(io);
        }
    }

    private ItemNormal TossItem()
    { 
        int toss = Random.Range(0, System.Enum.GetNames(typeof(ItemNormal)).Length);
        return (ItemNormal)toss;
    }

    public ItemScript GetItem(bool golden=false)
    {

        Sprite TargetSprite;
        foreach (ItemScript e in ItemPool)
        {
            if (e.gameObject.activeSelf == false)
            {
                e.gameObject.SetActive(true);

                if (golden == false)
                {
                    ItemNormal toss = TossItem();
                    TargetSprite = Items[(int)toss];
                    e.itemType = (ItemType)((int)toss);
                    e.SetSprite = TargetSprite;
                    e.transform.SetParent(null);
                    return e;
                }
                else
                {
                    TargetSprite = GoldenItem;
                    e.itemType = ItemType.GoldenItem;
                    e.SetSprite = TargetSprite;
                    e.transform.SetParent(null);
                    return e;
                }
            }
        }

        ItemScript io = Instantiate<ItemScript>(ItemPrefab, ItemLair);
        io.gameObject.SetActive(false);
        ItemPool.Add(io);

        if (golden == false)
        {
            ItemNormal toss = TossItem();
            TargetSprite = Items[(int)toss];
            io.itemType = (ItemType)((int)toss);
            io.SetSprite = TargetSprite;
            io.transform.SetParent(null);
            return io;
        }
        else
        {
            TargetSprite = GoldenItem;
            io.itemType = ItemType.GoldenItem;
            io.SetSprite = TargetSprite;
            io.transform.SetParent(null);
            return io;
        }


    }








}