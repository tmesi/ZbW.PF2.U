using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security;

namespace MB13.SinglyLinkedListExample {
  public class SinglyLinkedList<T> : IEnumerable<T> {

        public class Node {
            public T Data { get; set; }
            public Node Link { get; set; }

            public Node() { }

        }

        public Node start;
        public Node end;

        public int Count { get; set; }

        public SinglyLinkedList() {
            start = null;
            end = null;
        }

        public void Add(T data) {
            var node = new Node() { Data = data, Link = null};

            if (start == null) {
                start = node;
                end = node;
            } else {
                end.Link = node;
                end = node;
            }

            Count++;
        }

        public bool Contains(T data) {
            return Find(data) != null;
        }

        public T? Find(T data) {
            var currentNode = start;

            while (currentNode != null) {
                if (currentNode.Data.Equals(data)) {
                    return currentNode.Data;
                }

                currentNode = currentNode.Link;
            }

            return default;
        }

        public bool Remove(T data) {
            if (start.Data.Equals(data)) {
                start = start.Link;
                Count--;

                return true;
            }

            var currentNode = start.Link;
            var lastNode = start;

            while (currentNode != null) {
                lastNode = currentNode;
                currentNode = currentNode.Link;

                if (currentNode.Data.Equals(data)) {
                    lastNode.Link = currentNode.Link;
                    Count--;

                    return true;
                }
            }

            return false;
        }

        private Node FindByIndex(int index) {

            if (index <= 0 || index >= Count) {
                throw new IndexOutOfRangeException();
            }

            var current = start;
            var counter = 0;

            while (current != null) {
                if(counter == index) { 
                    return current; 
                }

                current = current.Link;
                counter++;
            }

            return null;

        }

        public void Clear()
        {
            start = null;
            end = null;
            Count = 0;
        }

        public T this[int index] {
            get => FindByIndex(index).Data;

            set {
                var node = FindByIndex(index);
                if (node != null) {
                    node.Data = value;
                }
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            var node = this.start;

            while (node != null) {
                yield return node.Data;
                node = node.Link;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
  }
}
