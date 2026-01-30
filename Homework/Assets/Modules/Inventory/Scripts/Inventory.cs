using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public int Count { get => itemPositions.Count; }

        private Item[,] storage;
        private Dictionary<Item, Vector2Int> itemPositions;

        public Inventory(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException();
            }

            Width = width;
            Height = height;

            storage = new Item[Width, Height];
            itemPositions = new Dictionary<Item, Vector2Int>();
        }

        public Inventory(
            int width,
            int height,
            params KeyValuePair<Item, Vector2Int>[] items
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
            Array.Copy(inventory.storage, storage, inventory.storage.Length);
            itemPositions = new Dictionary<Item, Vector2Int>(inventory.itemPositions);
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
            return item != null && (!IsValidItemSize(item)
                ? throw new ArgumentException()
                : IsValidItemBounds(item, startX, startY) &&
                    !Contains(item) &&
                    IsFreeSpace(startX, startY, startX + item.Size.x, startY + item.Size.y));
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

            for (int x = 0; x < item.Size.x; ++x)
            {
                for (int y = 0; y < item.Size.y; ++y)
                {
                    storage[startX + x, startY + y] = item;
                }
            }

            itemPositions.Add(item, new Vector2Int(startX, startY));

            OnAdded?.Invoke(item, new Vector2Int(startX, startY));

            return true;
        }

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(Item item)
        {
            return FindFreePosition(item, out Vector2Int freePosition) && CanAddItem(item, freePosition.x, freePosition.y);
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(Item item)
        {
            return FindFreePosition(item, out Vector2Int freePosition) && AddItem(item, freePosition.x, freePosition.y);
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
            if (sizeX <= 0 || sizeY <= 0)
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
            if (item == null)
            {
                return false;
            }
            return itemPositions.ContainsKey(item);
        }

        /// <summary>
        /// Checks if the specified position is occupied
        /// </summary>
        public bool IsOccupied(Vector2Int position)
        {
            return IsOccupied(position.x, position.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            if (item == null || !itemPositions.ContainsKey(item))
            {
                position = Vector2Int.zero;
                return false;
            }

            position = itemPositions[item];
            for (int x = 0; x < item.Size.x; ++x)
            {
                for (int y = 0; y < item.Size.y; ++y)
                {
                    storage[position.x + x, position.y + y] = null;
                }
                ;
            }
            itemPositions.Remove(item);

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
            return storage[x, y];
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
                throw new NullReferenceException();

            if (!itemPositions.ContainsKey(item))
                throw new KeyNotFoundException();

            Vector2Int[] positions = new Vector2Int[item.Size.x * item.Size.y];
            Vector2Int itemPosition = itemPositions[item];
            int count = 0;

            for (int x = 0; x < item.Size.x; ++x)
            {
                for (int y = 0; y < item.Size.y; ++y)
                {
                    positions[count] = new Vector2Int(itemPosition.x + x, itemPosition.y + y);
                    count++;
                }
            }

            return positions;
        }

        public bool TryGetPositions(Item item, out Vector2Int[] positions)
        {
            if (item == null || !itemPositions.ContainsKey(item))
            {
                positions = null;
                return false;
            }

            positions = GetPositions(item);

            return positions != null;
        }

        /// <summary>
        /// Clears all items 
        /// </summary>
        public void Clear()
        {
            if (Count == 0)
                return;

            Array.Clear(storage, 0, storage.Length);
            itemPositions.Clear();
            OnCleared?.Invoke();
        }

        /// <summary>
        /// Returns count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            int count = 0;

            foreach (Item item in itemPositions.Keys)
            {
                if (item.Name == name)
                {
                    ++count;
                }
            }

            return count;
        }

        public bool MoveItem(Item item, Vector2Int position)
        {
            if (item == null)
                throw new ArgumentNullException();
            if (!Contains(item) || !IsValidItemBounds(item, position.x, position.y))
                return false;

            for (int x = position.x; x - position.x < item.Size.x; ++x)
            {
                for (int y = position.y; y - position.y < item.Size.y; ++y)
                {
                    if (IsOccupied(x, y) && !storage[x, y].Equals(item))
                    {
                        return false;
                    }
                }
            }

            Vector2Int itemPosition = itemPositions[item];
            for (int x = 0; x < item.Size.x; ++x)
            {
                for (int y = 0; y < item.Size.y; ++y)
                {
                    storage[itemPosition.x + x, itemPosition.y + y] = null;
                }
            }
            for (int x = 0; x < item.Size.x; ++x)
            {
                for (int y = 0; y < item.Size.y; ++y)
                {
                    storage[position.x + x, position.y + y] = item;
                }
            }
            itemPositions[item] = position;

            OnMoved?.Invoke(item, position);

            return true;
        }

        /// <summary>
        /// Rearranges an inventory space with max free slots 
        /// </summary>
        public void OptimizeSpace()
        {
            if (Count == 0)
            {
                return;
            }

            Array.Clear(storage, 0, storage.Length);
            List<Item> items = new(itemPositions.Count);
            foreach (Item item in itemPositions.Keys)
            {
                items.Add(item);
            }

            SortItems(items);

            for (int y = 0; y < Height; ++y)
            {
                int l = 0;
                for (int r = 0; r < Width; ++r)
                {
                    l = r;
                    while (r < Width && !IsOccupied(r, y))
                    {
                        ++r;
                    }
                    for (; l < r; ++l)
                    {
                        if (items.Count == 0)
                        {
                            return;
                        }
                        int candidateIndex = SearchCandidateIndex(items, r - l);
                        if (candidateIndex < 0)
                            break;

                        InsertItem(items[candidateIndex], l, y);
                        l += items[candidateIndex].Size.x - 1;
                        items.RemoveAt(candidateIndex);
                    }
                }
            }
        }

        /// <summary>
        /// Iterates by all items 
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return itemPositions.Keys.GetEnumerator();
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return itemPositions.Keys.GetEnumerator();
        }

        /// <summary>
        /// Copies items to a specified matrix
        /// </summary>
        public void CopyTo(Item[,] matrix)
        {
            Array.Copy(storage, matrix, storage.Length);
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
                        table += $"{storage[x, y].Name},\t";
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

        private int SearchCandidateIndex(List<Item> items, int width)
        {
            int l = 0;
            int r = items.Count - 1;

            while (l < r)
            {
                int m = l + (r - l) / 2;
                if (items[m].Size.x <= width)
                {
                    l = m + 1;
                }
                else
                {
                    r = m;
                }
            }
            if (items[l].Size.x > width)
            {
                return l - 1;
            }
            return l;
        }

        private void InsertItem(Item item, int shiftX, int shiftY)
        {
            for (int x = 0; x < item.Size.x; ++x)
            {
                for (int y = 0; y < item.Size.y; ++y)
                {
                    storage[shiftX + x, shiftY + y] = item;
                }
            }
            itemPositions[item] = new Vector2Int(shiftX, shiftY);
        }

        private void SortItems(List<Item> items)
        {
            SortItems(items, 0, items.Count - 1);

            void SortItems(List<Item> items, int left, int right)
            {
                int l = left;
                int r = right;
                Item pivot = items[l];

                while (l <= r)
                {
                    while (Compare(pivot, items[l]) > 0)
                    {
                        ++l;
                    }
                    while (Compare(pivot, items[r]) < 0)
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

            int Compare(Item a, Item b)
            {
                if (a.Size.x != b.Size.x) return a.Size.x - b.Size.x;
                if (a.Size.y != b.Size.y) return a.Size.y - b.Size.y;
                if (a.Id != b.Id) return b.Id - a.Id;
                return 0;
            }
        }
    }
}