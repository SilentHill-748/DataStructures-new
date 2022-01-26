using System.Collections;

namespace DataStructures
{
    public class BinarySearchTree<T>
        where T : IComparable
    {
        private Node<T>? _root;


        public BinarySearchTree() { }

        public BinarySearchTree(T item)
        {
            _root = new Node<T>(item);
        }

        public BinarySearchTree(IEnumerable<T> items)
        {
            foreach (T item in items)
                Add(item);
        }


        public void Add(T item)
        {
            _root = AddNode(_root, item);
        }

        private Node<T> AddNode(Node<T>? node, T item)
        {
            if (node is null)
                return new Node<T>(item);

            if (node.CompareTo(item) < 0)
                node.LeftSubnode = AddNode(node.LeftSubnode, item);
            else
                node.RightSubnode = AddNode(node.RightSubnode, item);

            return node;
        }

        public List<T> Preorder()
        {
            if (_root is null)
                return new List<T>();

            return Preorder(_root);
        }

        public List<T> Inorder()
        {
            if (_root is null)
                return new List<T>();

            return Inorder(_root);
        }

        public List<T> Postorder()
        {
            if (_root is null)
                return new List<T>();

            return Postorder(_root);
        }

        private List<T> Preorder(Node<T>? node)
        {
            List<T> result = new();
            if (node is not null)
            {
                result.Add(node.Value);

                if (node.LeftSubnode is not null)
                    result.AddRange(Preorder(node.LeftSubnode));

                if (node.RightSubnode is not null)
                    result.AddRange(Preorder(node.RightSubnode));
            }

            return result;
        }

        public Node<T> PostorderSearch(T item)
        {
            return FindNode(_root, item);
        }

        private Node<T> FindNode(Node<T>? node, T item)
        {
            if (node is null)
                throw new ArgumentException("Binary tree is not contains specified node!");

            if (node.Value.Equals(item)) return node;

            if (node.CompareTo(item) < 0)
                return FindNode(node.LeftSubnode, item);

            return FindNode(node.RightSubnode, item);
        }

        private List<T> Inorder(Node<T> node)
        {
            List<T> result = new();

            if (node.LeftSubnode is not null)
                result.AddRange(Inorder(node.LeftSubnode));

            result.Add(node.Value);

            if (node.RightSubnode is not null)
                result.AddRange(Inorder(node.RightSubnode));

            return result;
        }

        private List<T> Postorder(Node<T> node)
        {
            List<T> result = new();

            if (node.LeftSubnode is not null)
                result.AddRange(Postorder(node.LeftSubnode));

            if (node.RightSubnode is not null)
                result.AddRange(Postorder(node.RightSubnode));

            result.Add(node.Value);

            return result;
        }
    }

    public class Node<T> : IComparable
        where T : IComparable
    {
        public T Value { get; set; }

        public Node<T>? LeftSubnode { get; set; }
        public Node<T>? RightSubnode { get; set; }


        public Node(T item)
        {
            Value = item;
        }

        public int CompareTo(object? obj)
        {
            if (obj is not null && obj is T item)
            {
                return item.CompareTo(Value);
            }
            else
                throw new ArgumentException("Obj is not node!");
        }
    }
}
