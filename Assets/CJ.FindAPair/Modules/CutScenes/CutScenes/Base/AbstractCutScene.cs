using System;
using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Modules.CutScenes.CutScenes.Base
{
    public abstract class AbstractCutScene
    {
        protected List<GameObject> ItemsPool;

        public abstract void Play();

        public abstract void Stop();

        protected virtual void InitializeItemsPool(ItemsPoolHandler itemsPoolHandler, GameObject item, 
            Transform creationTransform, int itemsCount)
        {
            ItemsPool = itemsPoolHandler.GetItemsPool(item, creationTransform, itemsCount);
        }
    }
}