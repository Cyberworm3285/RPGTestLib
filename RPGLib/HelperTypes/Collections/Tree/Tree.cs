using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace RPGLib.HelperTypes.Collections.Tree
{
    public class Tree<TContent, TId> : IEnumerable<TContent>, IEnumerable
    {
        public TreeNode<TContent, TId> Root { get; set; }
        public int Count { get; private set; }

        #region Constructors

        public Tree()
        {
        }

        public Tree(IEnumerable<TContent> content, Func<TContent, TId> idSelector,
            Func<TContent, List<TId>> childrenIdSelector, TContent rootContent = default(TContent),
            TId rootId = default(TId))
        {
            List<TreeNode<TContent, TId>> TreeParts = new List<TreeNode<TContent, TId>>();

            foreach (var x in content)
            {
                TId id = idSelector(x);
                TreeNode<TContent, TId> present = null;

                foreach (var y in TreeParts) //sucht treeparts die auf das momentane element verlinken wollen
                {
                    if (y.TryFind(t => t.PendingChildrenIds.Contains(id), out present))
                    {
                        break;
                    }
                }

                var curr = new TreeNode<TContent, TId>(id, x, childrenIdSelector(x));
                if (present != null) //wenn ein element vorhanden ist...
                {
                    present.AddChild(curr); // ..wird das aktuelle element diesem untergeordnet .. 
                }
                else
                {
                    TreeParts.Add(curr); //..oder selbst eingefügt ..
                }

                List<TId> pendings = new List<TId>();
                foreach (var z in curr.PendingChildrenIds) // ..und alle anderen treeparts die von diesem verlinkt werden ..
                {
                    var linked = TreeParts.Find(i => i.ID.Equals(z));
                    if (linked != null && !linked.Content.Equals(x))
                    {
                        curr.AddChild(linked, removePending: false); //..werden dem aktuellen unterstellt (pending darf nicht entfernt werden, da sonst die foreach-schleife stirbt)..
                        pendings.Add(z);
                        TreeParts.Remove(linked); // ..sowie aus der treepart liste entfernt
                    }
                }
                pendings.ForEach(pen => curr.PendingChildrenIds.Remove(pen)); //(jetz dürfen die pendings gelöscht werden)
            }

            Root = new TreeNode<TContent, TId>(rootId, rootContent, new List<TId>());
            foreach (var x in TreeParts)
            {
                Root.AddChild(x);
            }
            SetIndices();
        }

        #endregion

        #region Methods

        public bool TryFind(Predicate<TreeNode<TContent, TId>> predicate, out TreeNode<TContent, TId> output)
        {
            return Root.TryFind(predicate, out output);
        }

        public List<string> GetStringRepresentation()
        {
            return Root.GetStringRepresentation("", true);
        }

        public IEnumerable<TContent> Traverse()
        {
            foreach (var x in Root.Traverse())
            {
                yield return x;
            }
        }

        private void SetIndices()
        {
            IndexCounter counter = new IndexCounter(-1);
            Root.SetIndices(counter);
            Count = counter.CurrentIndex;
        }

        public TContent AtIndex(int index)
        {
            return Root.AtIndex(index);
        }

        #endregion

        #region Interface Implementations

        public IEnumerator<TContent> GetEnumerator() => new TreeEnumerator<TContent, TId>(this);

        IEnumerator IEnumerable.GetEnumerator() => new TreeEnumerator<TContent, TId>(this);

        #endregion

        #region Indexer

        public TContent this[Predicate<TContent> predicate]
        {
            get
            {
                TreeNode<TContent, TId> temp = null;
                if (Root.TryFind(x => predicate(x.Content), out temp))
                    return temp.Content;
                else
                    throw new ArgumentException("id not found");
            }
        }

        #endregion
    }
}
