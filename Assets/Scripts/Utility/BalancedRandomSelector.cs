using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility
{
    public class BalancedRandomSelector<T>
    {
        private List<T> items;
        private int lastIdx;
        private int randomIdx;
        
        public BalancedRandomSelector()
        {
            items = new List<T>();
            lastIdx = -1;
            randomIdx = -1;
        }
        
        public BalancedRandomSelector(IEnumerable<T> initItems)
        {
            items = initItems.ToList();
            lastIdx = items.Count - 1;
            randomIdx = -1;
        }

        public BalancedRandomSelector(List<T> initList)
        {
            items = initList;
            lastIdx = items.Count - 1;
            randomIdx = -1;
        }
        
        public void AddItem(T item)
        {
            items.Add(item);
            Swap(++lastIdx, items.Count - 1);
        }

        public T GetRandomItem()
        {
            randomIdx = Random.Range(0, lastIdx);
            T returnItem = items[randomIdx];

            return returnItem;
        }

        public void RemoveRandomItem()
        {
            if (randomIdx == -1)
            {
                return;
            }
            
            Swap(randomIdx, lastIdx);
            if (--lastIdx < 0)
            {
                lastIdx = items.Count - 1;
            }

            randomIdx = -1;
        }

        public void Clear()
        {
            items.Clear();
            lastIdx = 0;
            randomIdx = -1;
        }
        
        private void Swap(int idxA, int idxB)
        {
            (items[idxA], items[idxB]) = (items[idxB], items[idxA]);
        }
    }
}
