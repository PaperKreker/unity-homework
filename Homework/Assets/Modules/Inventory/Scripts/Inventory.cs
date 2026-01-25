using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Modules.Inventories
{
    public class Inventory : IEnumerable<Item>
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        public readonly int Width;
        public readonly int Height;
        public int Count { get => count; private set => count = value; }

        private int count;
        private StorageItem[,] storage;

        public Inventory(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException();
            }

            Width = width;
            Height = height;

            storage = new StorageItem[Width, Height];
        }

        public Inventory(
            int width,
            int height,
            params KeyValuePair<Item, Vector2Int>[] items
        ) : this (width, height)
        {
            if (items == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var (item, position) in items)
            {
                AddItem(item, position);
            }
        }

        public Inventory(
            int width,
            int height,
            params Item[] items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentNullException();
            }

            foreach (Item item in items)
            {
                AddItem(item);
            }
        }

        public Inventory(
            int width,
            int height,
            IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var (item, position) in items)
            {
                AddItem(item, position);
            }
        }

        public Inventory(
            int width,
            int height,
            IEnumerable<Item> items
        ) : this(width, height)
        {
            if (items == null)
            {
                throw new ArgumentNullException();
            }

            foreach (Item item in items)
            {
                AddItem(item);
            }
        }

        /// <summary>
        /// Creates new inventory 
        /// </summary>
        public Inventory(Inventory inventory) : this(inventory.Width, inventory.Height)
        {
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    storage[x, y] = inventory.storage[x, y];
                }
            }
        }

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(Item item, Vector2Int position)
        {
            return CanAddItem(item, position.x, position.y);
        }

        public bool CanAddItem(Item item, int startX, int startY)
        {
            if (item == null)
            {
                return false;
            }
            if (!IsValidItemSize(item))
            {
                throw new ArgumentException();
            }
            if (!IsValidItemBounds(item, startX, startY) || Contains(item) || !IsFreeSpace(startX, startY, startX + item.Size.x, startY + item.Size.y))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Adds an item on a specified position
        /// </summary>
        public bool AddItem(Item item, Vector2Int position)
        {
            return AddItem(item, position.x, position.y);
        }

        public bool AddItem(Item item, int startX, int startY)
        {
            if (!CanAddItem(item, startX, startY))
            {
                return false;
            }

            StorageItem storageItem = new StorageItem(item, startX, startY);
            IterateItemPositions(item, startX, startY, (position) =>
            {
                storage[position.x, position.y] = storageItem;
            });
            count++;

            OnAdded?.Invoke(item, new Vector2Int(startX, startY));

            return true;
        }

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(Item item)
        {
            FindFreePosition(item, out Vector2Int freePosition);
            return CanAddItem(item, freePosition.x, freePosition.y);
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(Item item)
        {
            FindFreePosition(item, out Vector2Int freePosition);
            return AddItem(item, freePosition.x, freePosition.y);
        }

        /// <summary>
        /// Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(Item item, out Vector2Int position)
        {
            if (item == null)
            {
                position = Vector2Int.zero;
                return false;
            }
            return FindFreePosition(item.Size.x, item.Size.y, out position);
        }

        public bool FindFreePosition(Vector2Int size, out Vector2Int position)
        {
            return FindFreePosition(size.x, size.y, out position);
        }

        public bool FindFreePosition(int sizeX, int sizeY, out Vector2Int position)
        {
            if (sizeX <= 0 ||  sizeY <= 0)
            {
                throw new ArgumentException();
            }

            for (int y = 0; y <= Height - sizeY; ++y)
            {
                for (int x = 0; x <= Width - sizeX; ++x)
                {
                    if (IsFreeSpace(x, y, x + sizeX, y + sizeY))
                    {
                        position = new Vector2Int(x, y);
                        return true;
                    }
                }
            }

            position = Vector2Int.zero;
            return false;
        }

        private bool IsFreeSpace(int startX, int startY, int endX, int endY)
        {
            for (int x = startX; x < endX; ++x)
            {
                for (int y = startY; y < endY; ++y)
                {
                    if (IsOccupied(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the specified element exists
        /// </summary>
        public bool Contains(Item item)
        {
            if (count == 0)
            {
                return false;
            }
            return GetStorageItem(item) != null;
        }

        /// <summary>
        /// Checks if the specified position is occupied
        /// </summary>
        public bool IsOccupied(Vector2Int position)
        {
            return IsOccupied(position.x, position.y);
        }

        public bool IsOccupied(int x, int y)
        {
            return storage[x, y] != null;
        }

        /// <summary>
        /// Checks if the specified position is free
        /// </summary>
        public bool IsFree(Vector2Int position)
        {
            return !IsOccupied(position.x, position.y);
        }

        public bool IsFree(int x, int y)
        {
            return !IsOccupied(x, y);
        }

        /// <summary>
        /// Removes specified item
        /// </summary>
        public bool RemoveItem(Item item)
        {
            return RemoveItem(item, out Vector2Int _);
        }

        public bool RemoveItem(Item item, out Vector2Int position)
        {
            StorageItem storageItem = GetStorageItem(item);
            if (storageItem == null)
            {
                position = Vector2Int.zero;
                return false;
            }

            IterateItemPositions(
                item,
                storageItem.StartX,
                storageItem.StartY,
                (position) =>
                {
                    storage[position.x, position.y] = null;
                });
            count--;
            position = new Vector2Int(storageItem.StartX, storageItem.StartY);

            OnRemoved?.Invoke(item, position);

            return true;
        }

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(Vector2Int position)
        {
            return GetItem(position.x, position.y);
        }

        public Item GetItem(int x, int y)
        {
            return storage[x, y]?.Content;
        }

        public bool TryGetItem(Vector2Int position, out Item item)
        {
            return TryGetItem(position.x, position.y, out item);
        }

        public bool TryGetItem(int x, int y, out Item item)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                item = null;
                return false;
            }

            item = GetItem(x, y);
            return IsOccupied(x, y);
        }

        /// <summary>
        /// Returns positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(Item item)
        {
            if (item == null)
            {
                throw new NullReferenceException();
            }

            StorageItem storageItem = GetStorageItem(item);
            if (storageItem == null)
            {
                throw new KeyNotFoundException();
            }

            Vector2Int[] positions = new Vector2Int[item.Size.x * item.Size.y];
            int count = 0;

            IterateItemPositions(
                item, 
                storageItem.StartX, 
                storageItem.StartY, 
                (position) =>
                {
                    positions[count] = new Vector2Int(position.x, position.y);
                    count++;
            });

            return positions;
        }

        public bool TryGetPositions(Item item, out Vector2Int[] positions)
        {
            try
            {
                positions = GetPositions(item);
            }
            catch
            {
                positions = null;
                return false;
            }
            return positions != null;
        }

        /// <summary>
        /// Clears all items 
        /// </summary>
        public void Clear()
        {
            if (count == 0)
            {
                return;
            }

            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    storage[x, y] = null;
                }
            }
            count = 0;
            OnCleared?.Invoke();
        }

        /// <summary>
        /// Returns count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            float count = 0.0f;

            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (IsFree(x, y))
                    {
                        continue;
                    }

                    Item item = storage[x, y].Content;
                    if (item.Name == name)
                    {
                        count += 1.0f / item.Size.x / item.Size.y;
                    }
                }
            }

            return Mathf.RoundToInt(count);
        }

        public bool MoveItem(Item item, Vector2Int position)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            if (!Contains(item) || !IsValidItemBounds(item, position.x, position.y))
            {
                return false;
            }
            for (int x = position.x; x - position.x < item.Size.x; ++x)
            {
                for (int y = position.y; y - position.y < item.Size.y; ++y)
                {
                    if (IsOccupied(x, y) && storage[x, y].Content != item)
                    {
                        return false;
                    }
                }
            }

            StorageItem storageItem = GetStorageItem(item);
            StorageItem newStorageItem = new StorageItem(item, position.x, position.y);
            Vector2Int itemPosition = new Vector2Int(storageItem.StartX, storageItem.StartY);
            IterateItemPositions(
                item,
                itemPosition.x,
                itemPosition.y,
                (position) =>
                {
                    storage[position.x, position.y] = null;
                });
            IterateItemPositions(
                item,
                position.x,
                position.y,
                (position) =>
                {
                    storage[position.x, position.y] = newStorageItem;
                });

            OnMoved?.Invoke(item, position);

            return true;
        }

        /// <summary>
        /// Rearranges an inventory space with max free slots 
        /// </summary>
        public void OptimizeSpace()
        {
            new StorageOptimizer(this).Optimize();
        }

        /// <summary>
        /// Iterates by all items 
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            Item[] items = GetItemsArray();
            
            return items.GetEnumerator();

        }

        public IEnumerator<Item> GetEnumerator()
        {
            Item[] items = GetItemsArray();

            return (IEnumerator<Item>)items.GetEnumerator();
        }

        /// <summary>
        /// Copies items to a specified matrix
        /// </summary>
        public void CopyTo(Item[,] matrix)
        {
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    matrix[x, y] = storage[x, y]?.Content;
                }
            }
        }

        /// <summary>
        /// Returns an inventory matrix in string format
        /// </summary>
        public override string ToString()
        {
            string table = "";
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (storage[x, y] != null)
                    {
                        table += $"{storage[x, y].Content.Name},\t";
                    }
                    else
                    {
                        table += $",\t";
                    }
                }
                table += "\n";
            }

            return table;
        }

        /// <summary>
        /// Returns storage item if it has specific item
        /// </summary>
        private StorageItem GetStorageItem(Item item)
        {
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (IsOccupied(x, y) && storage[x, y].Content == item)
                    {
                        return storage[x, y];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Iterates throw items positions in storage
        /// </summary>
        private void IterateItemPositions(Item item, int startX, int startY, Action<(int x, int y)> OnIteration)
        {
            for (int x = 0; x < item.Size.x; ++x)
            {
                for (int y = 0; y < item.Size.y; ++y)
                {
                    OnIteration.Invoke((startX + x, startY + y));
                }
            }
        }

        private bool IsValidItemBounds(Item item, int startX, int startY)
        {
            return startX >= 0 && startY >= 0 && startX + item.Size.x <= Width && startY + item.Size.y <= Height;
        }

        private bool IsValidItemSize(Item item)
        {
            return item != null &&
                    item.Size.x > 0 &&
                    item.Size.y > 0;
        }

        private Item[] GetItemsArray()
        {
            Item[] items = new Item[count];
            int itemsCountInArray = 0;

            foreach (StorageItem storageItem in storage)
            {
                if (storageItem == null)
                {
                    continue;
                }

                Item item = storageItem.Content;
                bool isInArray = false;

                for (int i = 0; i < itemsCountInArray; ++i)
                {
                    if (items[i] == item)
                    {
                        isInArray = true;
                        break;
                    }
                }
                if (!isInArray)
                {
                    items[itemsCountInArray] = item;
                    ++itemsCountInArray;
                }
            }

            return items;
        }

        private class StorageItem
        {
            public readonly int StartX;
            public readonly int StartY;
            public readonly Item Content;

            public StorageItem(Item item, int startX, int startY)
            {
                Content = item;
                StartX = startX;
                StartY = startY;
            }
        }

        private class StorageOptimizer
        {
            private StorageItem[,] optimizedStorage;
            private Space[] freeSpaces;
            private Inventory inventory;
            private int freeSpaceCount;

            public StorageOptimizer(Inventory inventory)
            {
                this.inventory = inventory;
            }

            public void Optimize()
            {
                optimizedStorage = new StorageItem[inventory.Width, inventory.Height];
                Item[] items = inventory.GetItemsArray();
                if (items.Length == 0)
                {
                    return;
                }

                SortItems(items, (Item a, Item b) =>
                {
                    int areaA = a.Size.x * a.Size.y;
                    int areaB = b.Size.x * b.Size.y;
                    if (areaA < areaB) return 1;
                    if (areaA > areaB) return -1;
                    if (a.Id > b.Id)   return 1;
                    if (a.Id < b.Id)   return -1;
                    return 0;
                });

                freeSpaces = new Space[inventory.Width * inventory.Height];
                freeSpaces[0] = new Space(0, 0, inventory.Width, inventory.Height);
                freeSpaceCount = 1;

                for (int i = 0; i < items.Length; ++i)
                {
                    Insert(items[i]);
                }

                inventory.storage = optimizedStorage;
            }

            private void Insert(Item item)
            {
                int freeSpaceIndex = FindBestSpaceIndex(item);
                if (freeSpaceIndex == -1)
                {
                    return;
                }

                Space freeSpace = freeSpaces[freeSpaceIndex];
                StorageItem storageItem = new StorageItem(item, freeSpace.x, freeSpace.y);
                inventory.IterateItemPositions(
                item,
                freeSpace.x,
                freeSpace.y,
                (position) =>
                {
                    optimizedStorage[position.x, position.y] = storageItem;
                });

                SplitSpace(item, freeSpace.x, freeSpace.y);
            }

            private int FindBestSpaceIndex(Item item)
            {
                int bestX = int.MaxValue;
                int bestY = int.MaxValue;
                int bestSpaceIndex = -1;

                for (int i = 0; i < freeSpaceCount; ++i)
                {
                    Space freeSpace = freeSpaces[i];
                    if (item.Size.x <= freeSpace.width &&
                        item.Size.y <= freeSpace.height)
                    {
                        if (freeSpace.y < bestY || freeSpace.y == bestY && freeSpace.x < bestX)
                        {
                            bestY = freeSpace.y;
                            bestX = freeSpace.x;
                            bestSpaceIndex = i;
                        }
                    }
                }

                return bestSpaceIndex;
            }

            private void SplitSpace(Item item, int x, int y)
            {
                int count = freeSpaceCount;
                for (int i = 0; i < count; ++i) {
                    Space space = freeSpaces[i];
                    if (!IsIntersecting(new Space(x, y, item.Size.x, item.Size.y), space))
                    {
                        continue;
                    }

                    AddFreeSpace(new Space(space.x, space.y, x - space.x, space.height));
                    AddFreeSpace(new Space(x + item.Size.x, space.y, space.x + space.width - (x + item.Size.x), space.height));
                    AddFreeSpace(new Space(space.x, space.y, space.width, y - space.y));
                    AddFreeSpace(new Space(space.x, y + item.Size.y, space.width, space.y + space.height - (y + item.Size.y)));
                    RemoveFreeAt(i);
                }

                PruneFreeSpaces();
            }

            private void AddFreeSpace(Space space)
            {
                if (space.width <= 0 || 
                    space.height <= 0 || 
                    space.x + space.width > inventory.Width || 
                    space.y + space.height > inventory.Height)
                {
                    return;
                }
                freeSpaces[freeSpaceCount] = space;
                freeSpaceCount++;
            }

            private void RemoveFreeAt(int index)
            {
                if (freeSpaceCount == 0)
                {
                    return;
                }

                freeSpaceCount--;
                freeSpaces[index] = freeSpaces[freeSpaceCount];
                freeSpaces[freeSpaceCount] = null;
            }

            private void PruneFreeSpaces()
            {
                for (int i = 0; i < freeSpaceCount; i++)
                {
                    for (int j = i + 1; j < freeSpaceCount; j++)
                    {
                        if (IsContainedIn(freeSpaces[i], freeSpaces[j]))
                        {
                            RemoveFreeAt(i);
                            i--;
                            break;
                        }
                        if (IsContainedIn(freeSpaces[j], freeSpaces[i]))
                        {
                            RemoveFreeAt(j);
                            j--;
                        }
                    }
                }
            }

            private bool IsContainedIn(Space a, Space b)
            {
                return  a.x >= b.x &&
                        a.y >= b.y &&
                        a.x + a.width <= b.x + b.width &&
                        a.y + a.height <= b.y + b.height;
            }

            private bool IsIntersecting(Space a, Space b)
            {
                return !(a.x + a.height <= b.x ||
                        a.x >= b.x + b.width ||
                        a.y + a.height <= b.y ||
                        a.y >= b.y + b.height);
            }

            private void SortItems(Item[] items, Func<Item, Item, int> comparer)
            {
                SortItems(items, 0, items.Length - 1);

                void SortItems(Item[] items, int left, int right)
                {
                    int l = left;
                    int r = right;
                    Item pivot = items[l];

                    while (l <= r)
                    {
                        while (comparer.Invoke(pivot, items[l]) > 0)
                        {
                            ++l;
                        }
                        while (comparer.Invoke(pivot, items[r]) < 0)
                        {
                            --r;
                        }

                        if (l <= r)
                        {
                            Item temp = items[l];
                            items[l] = items[r];
                            items[r] = temp;
                            ++l;
                            --r;
                        }
                    }

                    if (left < r)
                    {
                        SortItems(items, left, r);
                    }
                    if (l < right)
                    {
                        SortItems(items, l, right);
                    }
                }
            }

            private class Space
            {
                public int x;
                public int y;
                public int width;
                public int height;

                public Space(int x, int y, int width, int height)
                {
                    this.x = x;
                    this.y = y;
                    this.width = width;
                    this.height = height;
                }
            }
        }
    }
}