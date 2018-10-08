using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{

    [System.Serializable]
    public class ContainerItem
    {
        public System.Guid Id;
        public string Name;
        public int Maximum;

        public int amountTaken;

        public ContainerItem()
        {
            Id = System.Guid.NewGuid();
        }

        public int Remaining
        {
            get
            {
                return Maximum - amountTaken;
            }
        }

        public int GetAmountTaken
        {
            get
            {
                return amountTaken;
            }
        }

        public void ReduceAmountTaken(int amount)
        {
            int newAmountTaken = amountTaken - amount;

            if (newAmountTaken < 0)
            {
                amountTaken = 0;
            }
            else
            {
                amountTaken = newAmountTaken;
            }
        }

        public int Get(int value)
        {
            if ((amountTaken + value) > Maximum)
            {
                int toMuch = (amountTaken + value) - Maximum;
                amountTaken = Maximum;
                return value - toMuch;
            }

            amountTaken += value;
            return value;
        }
    }

    public List<ContainerItem> items;

    private void Awake()
    {
        items = new List<ContainerItem>();
    }

    public System.Guid Add(string name, int maximum)
    {
        items.Add(new ContainerItem
        {
            Maximum = maximum,
            Name = name
        });

        return items.Last().Id;
    }

    public int TakeFromContainer(System.Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);

        if (containerItem == null)
            return -1;

        return containerItem.Get(amount);
    }

    public int GetAmountRemaining(System.Guid id)
    {
        var containerItem = GetContainerItem(id);

        if (containerItem == null)
            return -1;

        return containerItem.Remaining;
    }

    private ContainerItem GetContainerItem(System.Guid id)
    {
        var containerItem = items.Where(x => x.Id == id).FirstOrDefault();

        return containerItem;
    }

    public void addMoreAvailableItems(System.Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);

        containerItem.ReduceAmountTaken(amount);
    }
}
