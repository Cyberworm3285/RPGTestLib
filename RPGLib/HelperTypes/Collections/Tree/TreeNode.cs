using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RPGLib.HelperTypes.Collections.Tree
{
    public class TreeNode<TContent, TId>
    {
        public TId ID { get; set; }
        public TContent Content { get; set; }
        public Dictionary<TId, TreeNode<TContent, TId>> Children { get; set; }
        public List<TId> PendingChildrenIds { get; set; }

        #region Constructors

        private TreeNode(TId id, TContent content)
        {
            ID = id;
            Content = content;

            Children = new Dictionary<TId, TreeNode<TContent, TId>>();
        }

        public TreeNode(TId id, TContent content, List<TId> pendingIds)
            :   this(id, content)
        {
            PendingChildrenIds = pendingIds;
        }

        public TreeNode(TContent content, TId id, IEnumerable<TreeNode<TContent, TId>> children)
        {
            ID = id;
            Content = content;

            Children = new Dictionary<TId, TreeNode<TContent, TId>>();
            foreach(var x in children)
            {
                Children.Add(x.ID, x);
            }
        }

        public TreeNode(TContent content, TId id, Dictionary<TId, TreeNode<TContent, TId>> children)
        {
            ID = id;
            Content = content;
            Children = children;
        }

        #endregion

        #region Methods

        public bool TryFind(Predicate<TreeNode<TContent, TId>> predicate, out TreeNode<TContent, TId> output)
        {
            if (predicate(this))
            {
                output = this;
                return true;
            }
            else if (Children.Keys.Count == 0)
            {
                output = null;
                return false;
            }
            else
            {
                foreach (var x in Children.Values)
                {
                    if (x.TryFind(predicate, out output))
                    {
                        return true;
                    }
                }
                output = null;
                return false;
            }
        }

        public void AddChild(TreeNode<TContent, TId> child)
        {
            if (PendingChildrenIds != null && PendingChildrenIds.Contains(child.ID))
            {
                PendingChildrenIds.Remove(child.ID);
            }
            Children.Add(child.ID, child);
        }

        public List<string> GetStringRepresentation(string indent, bool last)
        {
            string line = indent;
            var output = new List<string>(); 
            if (last)
            {
                line += ("╚════>");
                indent += "      ";
            }
            else
            {
                line += ("╠════>");
                indent += "║     ";
            }

            if (PendingChildrenIds.Count == 0)
                line += this.ToString();
            else
                line += $"{this.ToString()} ══> Linked Elsewhere : [{string.Join(",", PendingChildrenIds.Select(x => (x==null?"END":x.ToString())))}]";
            output.Add(line);

            foreach (var x in Children.Values)
            {
                output.AddRange(x.GetStringRepresentation(indent, x.Equals(Children.Values.Last())));
            }

            return output;
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return $"[{ID}]";
        }

        #endregion
    }
}
