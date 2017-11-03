using System;
using System.Collections.Generic;
using System.Text;

namespace RPGLib.HelperTypes.Collections.Tree
{
    public class Tree<TContent, TId> // : ICollection<TContent>
    {
        public TreeNode<TContent, TId> Root { get; set; }

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

                foreach (var z in curr.PendingChildrenIds
                ) // ..und alle anderen treeparts die von diesem verlinkt werden ..
                {
                    var linked = TreeParts.Find(i => i.ID.Equals(z));
                    if (linked != null && !linked.Content.Equals(x))
                    {
                        curr.AddChild(linked); //..werden dem aktuellen unterstellt ..
                        TreeParts.Remove(linked); // ..sowie aus der treepart liste entfernt
                    }
                }
            }

            Root = new TreeNode<TContent, TId>(rootId, rootContent, new List<TId>());
            foreach (var x in TreeParts)
            {
                Root.AddChild(x);
            }
        }

        public List<string> GetStringRepresentation()
        {
            return Root.GetStringRepresentation("", true);
        }
    }
}
