using System;
using MunicipalServiceApp.Models;

namespace MunicipalServiceApp.DataStructures
{
    /// <summary>
    /// Custom queue implementation for managing issues in priority order
    /// Uses a circular array for efficient queue operations
    /// </summary>
    public class IssueQueue
    {
        private Issue[] items;
        private int front;
        private int rear;
        private int count;
        private readonly int capacity;

        public int Count => count;
        public bool IsEmpty => count == 0;
        public bool IsFull => count == capacity;

        public IssueQueue(int capacity = 50)
        {
            this.capacity = capacity;
            items = new Issue[capacity];
            front = 0;
            rear = -1;
            count = 0;
        }

        /// <summary>
        /// Adds an issue to the rear of the queue
        /// </summary>
        public void Enqueue(Issue issue)
        {
            if (IsFull)
                throw new InvalidOperationException("Queue is full");

            rear = (rear + 1) % capacity;
            items[rear] = issue;
            count++;
        }

        /// <summary>
        /// Removes and returns the issue at the front of the queue
        /// </summary>
        public Issue Dequeue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Queue is empty");

            Issue issue = items[front];
            items[front] = null; // Clear reference
            front = (front + 1) % capacity;
            count--;
            return issue;
        }

        /// <summary>
        /// Returns the issue at the front of the queue without removing it
        /// </summary>
        public Issue Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Queue is empty");

            return items[front];
        }

        /// <summary>
        /// Converts the queue to an array for display purposes
        /// </summary>
        public Issue[] ToArray()
        {
            Issue[] result = new Issue[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = items[(front + i) % capacity];
            }
            return result;
        }

        /// <summary>
        /// Clears all issues from the queue
        /// </summary>
        public void Clear()
        {
            Array.Clear(items, 0, capacity);
            front = 0;
            rear = -1;
            count = 0;
        }
    }
}
