using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class RedBlackTree
    {
        public class Node
        {
            public int Value { get; set; }
            public Node Parent, Left, Right;

            public bool IsRed = true;

            public Node(int value)
            {
                Value = value;
            }
        }

        private Node _Root;

        public void Insert(int value)
        {
            Node newNode = new Node(value);
            if(_Root is null)
            {
                _Root = newNode;
                _Root .IsRed = false;
                return;
            }

            Node current = _Root;
            Node parent = null;
            while(current != null)
            {
                parent = current;
                if(value <  current.Value) 
                    current = current.Left;
                else
                    current = current.Right;
            }

            newNode.Parent = parent;

            if(newNode.Value < parent.Value)
                parent.Left  = newNode;
            else 
                parent.Right = newNode;
        
            FixInsert(newNode);
        }

        //method to fix red-black tree properties after insertion.
        private void FixInsert(Node node)
        {
            Node parent = null, grandparent = null, uncle = null;

            // Fixing the node as long as the node is not the root and both the node and its parent are red
            while ( node != _Root && node.IsRed && node.Parent.IsRed)
            {
                parent = node.Parent;
                grandparent = parent.Parent;

                if(parent == grandparent.Left)
                {
                    uncle = grandparent.Right;
                    //case 1: the uncle is red (recoloring)
                    if(uncle != null && uncle.IsRed)
                    {
                        grandparent.IsRed = true;//Recolor the grandparent to red 
                        parent.IsRed = false;//recolor the parent to black
                       uncle.IsRed = false;
                        node = grandparent;//move up the tree to continue fixing 
                    }
                    else
                    {
                        //case 2: Node is the right child of its parent (triangle case)
                        if (node == parent.Right)
                        {
                            //LeftRotate for the parent (in the opposite direction of its child) to transform the triangle case into the line case
                            LeftRotate(parent);
                            node = parent;
                            parent = node.Parent;
                        }

                        //case 3: node is now the left child of its parent (line case)
                        //perform right (in the opposite direction of its grandchild)
                        //rotation on the grandparent to maintain the properties
                        RightRotate(grandparent);

                        //swap the colors of parent and grandparent 
                        (parent.IsRed, grandparent.IsRed) = (grandparent.IsRed, parent.IsRed);
                        
                        node=parent;
                    }
                }
                else // parent is the right child
                {
                    uncle = grandparent.Left;
                    //case 1: the undcle is red (recoloring)
                    if(uncle != null && uncle.IsRed)
                    {
                        grandparent.IsRed = true;
                        parent.IsRed = false;
                        uncle.IsRed = false;
                        node = grandparent;
                    }
                    else
                    {
                        //case 2: node is the left child (triangle case)
                        if(node == parent.Left)
                        {

                            //Right rotate (in the opposite direction of it child) the parent node to transform the triangle into line
                            RightRotate(parent);
                            node= parent;
                            parent = node.Parent;
                        }

                        //node is now the right child of its parent (line case)
                        //perform left (in the opposite direction of its grand child) rotation on grandparent to balance the tre
                        LeftRotate(grandparent);

                        //swap the colors of parent and grandparent 
                        (parent.IsRed, grandparent.IsRed) = (grandparent.IsRed, parent.IsRed);
                        node = parent;
                    }
                }
            }
            _Root.IsRed = false;
        }


        private void LeftRotate(Node parent)
        {
            //right child becomes the new root of the subtree
            Node OriginalChild = parent.Right;
            //parent grand left child becomes its right child
            //moving the left subtree of the originalchild to the right subtree of its parent
            parent.Right = OriginalChild.Left;

            //incase the origincal child had a left child it bomes the new right child
            //of its new right child(ex parent)
            if(parent.Right != null)
                parent.Right.Parent = parent;
            
            OriginalChild.Parent = parent.Parent;//connect the new root with the grand parent


            if (parent.Parent == null)
                _Root = OriginalChild;//the right child becomes new root of the tree
            else if(parent == parent.Parent.Left)
                parent.Parent.Left = OriginalChild;//set right child to left child of parent(ex GParent)
            else
                parent.Parent.Right = OriginalChild;//set right child to left child of parent(ex GParent)
            
            OriginalChild.Left = parent;//original parent becomes the left child of its right child

            parent.Parent = OriginalChild;//update parent of the original parent
        }

        private void RightRotate(Node parent)
        {
            Node OriginalChild = parent.Left;//left child becomes the new root of the subtree

            //moving the right subtree of the originalchild to the left subtree of its parent
            parent.Left = OriginalChild.Right;

            //incase the origincal child had a right child it bomes the new left child
            //of its new right child(ex parent)
            if (parent.Left != null)
                parent.Left.Parent = parent;

            //connect new root with the grand parent
            OriginalChild.Parent = parent.Parent;

            if(parent.Parent == null)
                _Root = OriginalChild;//the original child becomes the new root of the tree
            else if(parent == parent.Parent.Right)
                parent.Parent.Right = OriginalChild;//set original child(left child) child to
                                                    //right child of parent(ex GParent) 
            else
                parent.Parent.Left =OriginalChild;//set original child(left child)
                                                  //to left child of parent(ex GParent)

            OriginalChild.Right = parent;//original parent becomes the right child of its left child

            parent.Parent = OriginalChild;//update parent of the original parent

                                                 }

        public void PrintTree()
        {
            PrintHelper(_Root, "", true);
        }
        private void PrintHelper(Node node, string indent, bool last)
        {
            if(node != null)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("R----");
                    indent += "   ";
                }
                else
                {
                    Console.Write("L----");
                    indent += "|  ";
                }
                var color = node.IsRed ? "RED" : "BLK";
                Console.WriteLine(node.Value + "(" + color + ")");
                PrintHelper(node.Left, indent, false);
                PrintHelper(node.Right, indent, true);
            }

        }

        private Node FindNode(Node SearchInNode, int value)
        {
            if (SearchInNode == null || SearchInNode.Value == value)
                return SearchInNode;

            if(SearchInNode.Value >  value)
                return FindNode(SearchInNode.Left, value);
            else
                return FindNode(SearchInNode.Right, value);
        }

        public Node Find(int value)
        {
            return FindNode(_Root, value);
        }
    }
}
