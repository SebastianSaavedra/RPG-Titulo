﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOverworld : MonoBehaviour {

    private PlayerOverworld playerOvermap;
    private Item item;

    public void Setup(Item item, PlayerOverworld playerOverworld) {
        this.item = item;
        this.playerOvermap = playerOverworld;
    }

    private void Update() {
        float tractorDistance = 5f;
        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < tractorDistance) {
            // Within Tractor distance, get pulled towards player
            float tractorSpeed = Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) * 8f;
            transform.position += (playerOvermap.GetPosition() - GetPosition()).normalized * tractorSpeed * Time.deltaTime;

            float grabDistance = 2f;
            if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < grabDistance) {
                // Within Grab distance, grab and destroy

                switch (item.GetItemType()) {
                default:
                case Item.ItemType.MedicinalHerbs:
                        GameData.AddHerbs(item.GetAmount());
                        SoundManager.PlaySound(SoundManager.Sound.Coin);
                    break;
                }

                item.DestroySelf();
                Destroy(gameObject);
            }
        }
    }


    private Vector3 GetPosition() {
        return transform.position;
    }

}
