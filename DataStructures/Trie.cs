namespace DataStructures
{
    public class Trie<T>
    {
        private TrieNode<T> _root;


        public Trie()
        {
            _root = new TrieNode<T>('\0', default, "");
        }


        public void Add(string key, T item)
        {
            AddNode(key, item, _root);
        }

        public void Remove(string key)
        {
            RemoveNode(key, _root);
        }

        public bool TrySearch(string key, out T? value)
        {
            return SearchNode(key, _root, out value);
        }


        private void AddNode(string key, T data, TrieNode<T> node)
        {
            if (string.IsNullOrEmpty(key))
            {
                if (!node.IsWord)
                {
                    node.Data = data;
                    node.IsWord = true;
                }
            }
            else
            {
                char symbol = key[0];
                TrieNode<T>? subnode = node.TryFind(symbol);

                if (subnode is not null)
                {
                    AddNode(key[1..], data, subnode);
                }
                else
                {
                    TrieNode<T> newNode = new(symbol, data, node.Prefix + key[0]);
                    node.Subnodes.Add(symbol, newNode);
                    AddNode(key[1..], data, newNode);
                }    
            }
        }

        private void RemoveNode(string key, TrieNode<T> node)
        {
            if (string.IsNullOrEmpty(key))
            {
                if (node.IsWord)
                {
                    node.IsWord = false;
                    node.Data = default;
                }
            }
            else
                RemoveNode(key[1..], node.Subnodes[key[0]]);
        }

        private bool SearchNode(string key, TrieNode<T> node, out T? value)
        {
            bool result = false;
            value = default;

            if (string.IsNullOrEmpty(key))
            {
                value = node.Data;
                return true;
            }
            else
            {
                TrieNode<T>? subNode = node.TryFind(key[0]);

                if (subNode is not null)
                    result = SearchNode(key[1..], subNode, out value);
            }
            
            return result;
        }
    }

    public class TrieNode<T>
    {
        public char Symbol { get; set; }
        public T? Data { get; set; }
        public bool IsWord { get; set; }
        public string Prefix { get; set; }

        public Dictionary<char, TrieNode<T>> Subnodes { get; set; }


        public TrieNode(char symbol, T? data, string prefix)
        {
            Symbol = symbol;
            Data = data;
            Subnodes = new Dictionary<char, TrieNode<T>>();
            Prefix = prefix;
        }


        public TrieNode<T>? TryFind(char symbol)
        {
            if (Subnodes.TryGetValue(symbol, out TrieNode<T>? value))
                return value;

            return null;
            //throw new ArgumentException("Node with specified symbol key is not found!", nameof(symbol));
        }

        public override string ToString()
        {
            if (Data is null)
                return string.Empty;
            
            return "Data: " + (Data.ToString() ?? string.Empty) + $" Symbol: {Symbol} Prefix: {Prefix}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is TrieNode<T> node)
            {
                if (Data is null)
                    return false;

                return Data.Equals(node);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
