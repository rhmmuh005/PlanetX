using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    // container class for inventory system
    [System.Serializable]
    public class ContainerItem
    {
        public System.Guid Id;
        public string Name;
        public int Maximum;

        public int amountTaken;

        // creates a new container item
        public ContainerItem()
        {
            Id = System.Guid.NewGuid();
        }

        // amount left in the container
        public int Remaining
        {
            get
            {
                return Maximum - amountTaken;
            }
        }

        // return amount taken
        public int GetAmountTaken
        {
            get
            {
                return amountTaken;
            }
        }

        // reduce the amount taken from the container
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

    // add a new container item with elements
    public System.Guid Add(string name, int maximum)
    {
        items.Add(new ContainerItem
        {
            Maximum = maximum,
            Name = name
        });

        return items.Last().Id;
    }

    // take elements from container
    public int TakeFromContainer(System.Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);

        if (containerItem == null)
            return -1;

        return containerItem.Get(amount);
    }

    // return amount remaining
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

    // add more available items
    public void addMoreAvailableItems(System.Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);

        containerItem.ReduceAmountTaken(amount);
    }
}
