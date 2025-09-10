using System;
using System.Collections;
using System.Collections.Generic;

namespace MunicipalServiceApp.DataStructures
{
    /// <summary>
    /// Custom implementation of a dynamic list data structure
    /// Built specifically for the municipal services application
    /// </summary>
    /// <typeparam name="T">The type of elements in the list</typeparam>
    public class CustomList<T> : IEnumerable<T>
    {
        private T[] items;
        private int count;
        private const int DefaultCapacity = 4;

        public int Count => count;
        public int Capacity => items.Length;

        public CustomList()
        {
            items = new T[DefaultCapacity];
            count = 0;
        }

        public CustomList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));
            
            items = new T[capacity];
            count = 0;
        }

        /// <summary>
        /// Indexer to access elements by index
        /// </summary>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return items[index];
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                items[index] = value;
            }
        }

        /// <summary>
        /// Adds an item to the end of the list
        /// </summary>
        public void Add(T item)
        {
            if (count == items.Length)
            {
                Resize();
            }
            items[count] = item;
            count++;
        }

        /// <summary>
        /// Inserts an item at the specified index
        /// </summary>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (count == items.Length)
            {
                Resize();
            }

            // Shift elements to the right
            for (int i = count; i > index; i--)
            {
                items[i] = items[i - 1];
            }

            items[index] = item;
            count++;
        }

        /// <summary>
        /// Removes the first occurrence of the specified item
        /// </summary>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the item at the specified index
        /// </summary>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // Shift elements to the left
            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            count--;
            items[count] = default(T); // Clear the reference
        }

        /// <summary>
        /// Finds the index of the first occurrence of the specified item
        /// </summary>
        public int IndexOf(T item)
        {
            for (int i = 0; i < count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], item))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Checks if the list contains the specified item
        /// </summary>
        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        /// <summary>
        /// Clears all items from the list
        /// </summary>
        public void Clear()
        {
            Array.Clear(items, 0, count);
            count = 0;
        }

        /// <summary>
        /// Converts the list to an array
        /// </summary>
        public T[] ToArray()
        {
            T[] result = new T[count];
            Array.Copy(items, result, count);
            return result;
        }

        /// <summary>
        /// Resizes the internal array when capacity is exceeded
        /// </summary>
        private void Resize()
        {
            int newCapacity = items.Length == 0 ? DefaultCapacity : items.Length * 2;
            T[] newItems = new T[newCapacity];
            Array.Copy(items, newItems, count);
            items = newItems;
        }

        /// <summary>
        /// Returns an enumerator for the list
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
