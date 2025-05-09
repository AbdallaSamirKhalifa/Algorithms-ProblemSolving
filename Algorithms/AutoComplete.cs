using System;
using System.Collections.Generic;


namespace Algorithms
{
    public class AVLAutoComplete 
    {
        private  AVLTree<string> Tree;
        public AVLAutoComplete(AVLTree<string> tree)
        {
            Tree= tree;
        }
        public  IEnumerable<string> AutoComplete(string preFix)
        {
            List<string> result = new List<string>();
            AutoComplete(Tree.Root, preFix, result);
            return result;
        }

        private  void AutoComplete(AVLNode<string> node, string preFix, List<string> result)
        {
            if (node != null)
            {
                if(node.Value.StartsWith(preFix,StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(node.Value);
                    AutoComplete(node.Left, preFix, result);
                        AutoComplete(node.Right, preFix, result);


                }
                else
                {
                    int comparison = string.Compare(preFix, node.Value, StringComparison.OrdinalIgnoreCase);
                    if (comparison > 0)
                        AutoComplete(node.Right, preFix, result);
                    else
                        AutoComplete(node.Left, preFix, result);
                }
            }
            
            
        }
    }
}
