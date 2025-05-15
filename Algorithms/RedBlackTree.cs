using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
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

        public bool Delete(int value)
        {
            Node NodeToDelete = FindNode(_Root, value);
            if(NodeToDelete is null)
                return false;
            DeleteNode(NodeToDelete);
            return true;
        }

        private void DeleteNode(Node nodeToDelete)
        {
            //Node that might require fixing the Red-Black properties
            Node nodeToFix = null;
            Node parent = null; 
            Node child = null;//child of the node to delete or its successor

            bool originalColor = nodeToDelete.IsRed;//store the original color of the node to delete.

            //case 1: the node to delete has no left child
            if(nodeToDelete.Left is null)
            {
                //the child is the right child of the node
                child = nodeToDelete.Right;
                //replace nodeToDelete with its right child.
                Transplant(nodeToDelete, child);
            }
            //case 2: The node to delete has node right child
            else if(nodeToDelete.Right is null)
            {
                child = nodeToDelete.Left;
                //replace the node wiht its left child
                Transplant(nodeToDelete, child);
            }
            //case 3: the node to delete has both left and righ child
            else
            {
                //find the inorder successor (smallest node in the right subtree)
                Node successor = Minimum(nodeToDelete.Right);
                //store the original color of the successor
                originalColor = successor.IsRed;
                //the child is the right child of the succssor
                child = successor.Right;

                //if the successor is the immediate child of the node to delete
                if(successor.Parent == nodeToDelete)
                {
                    if (child != null)
                        child.Parent = successor;//update the parent of the child
                }
                else
                {
                    //replace the successor with its right child in its original position
                    Transplant(successor, successor.Right);
                    successor.Right = nodeToDelete.Right;//connect the right child of the node to delete to the successor
                    successor.Right.Parent = successor;// update the parent of the right child 
                }

                //replace the node to delete with the successor 
                Transplant(nodeToDelete, successor);
                successor.Left = nodeToDelete.Left;//connect the left child of the node to delete to the successor
                successor.Left.Parent = successor;
                successor.IsRed = nodeToDelete.IsRed;//mainatain the original color of the node being deleted
            }

            //if the original color of the node was black, fix the red black properties
            if (!originalColor && child != null)
                FixDelete(child);
        }


        //replaces one subtree as a child of its parent with another 
        private void Transplant(Node target, Node with)
        {
            //if the target node is the root of the tree (it has no parent)
            //then the new subtree (with) becomes the new root of the tree
            if (target.Parent == null)
                _Root = with;
            //if the target node is the left child of its parent
            //then update the parent's left child to be the new subtree (with)
            else if (target.Parent.Left == target)
                target.Parent.Left = with;
            //if the target node is the right child of its parent
            //then update the parent's right child to be the new subtree (with)
            else
                target.Parent.Right = with;

            if(with!=null)
                with.Parent = target.Parent;

        }
        private void FixDelete(Node node)
        {
            Node sibling = null;
            //loop until node is the root or untill the node is red
            while(node != _Root && !node.IsRed)
            {
                //if the node is the left child of its parent
                if(node == node.Parent.Left)
                {
                    sibling = node.Parent.Right;
                    //case 1: the sibling is red, perform a rotation and recolor
                    if (sibling.IsRed)
                    {
                        sibling.IsRed = false;//recolor the sibling to black
                        node.Parent.IsRed = true;//recolor parent to red
                        LeftRotate(node.Parent);//rotate the parent to the left
                        sibling = node.Parent.Right;//update the sibling after rotation
                    }

                    //case 2.1: if both sibling's children are blac
                    if(!sibling.Left.IsRed && !sibling.Right.IsRed)
                    {
                        sibling.IsRed = true;//recolor sibling to red
                        node = node.Parent;//move up the tree to continue fixing
                    }
                    else
                    {
                        //case 2.2.2: if sibling's right child is black and left child is red
                        //(near child is red)
                        if (!sibling.Right.IsRed)
                        {
                            sibling.Left.IsRed = false;//recolor sibling's left child to black
                            sibling.IsRed = true; //recolor sibling to red
                            RightRotate(sibling);//rotate sibling to the right
                            sibling= node.Parent.Right;//update sibling after rotation
                        }

                        //case 2.2.1: sibling's right child is red (far child)
                        sibling.IsRed = node.Parent.IsRed;//recolor sibling with parent's color
                        node.Parent.IsRed = false;//recolor parent to black
                        sibling.Right.IsRed = false;//recolor sibling's right child to black
                        LeftRotate(node.Parent);//rotate parent to the left
                        node = _Root;//set node to root to break out of the loop

                    }

                }
                //if the node is the right child
                else
                {
                    sibling = node.Parent.Left;
                    //case 1: the sibling is red, perform a rotation and recolor
                    if (sibling.IsRed)
                    {
                        sibling.IsRed = false;//recolor the sibling to black
                        node.Parent.IsRed = true;//recolor parent to red
                        RightRotate(node.Parent);//rotate the parent to the right
                        sibling = node.Parent.Left;//update the sibling after rotation
                    }

                    //case 2.1: if both sibling's children are black
                    if (!sibling.Left.IsRed && !sibling.Right.IsRed)
                    {
                        sibling.IsRed = true;//recolor sibling to red
                        node = node.Parent;//move up the tree to continue fixing
                    }
                    else
                    {
                        //case 2.2.2: if sibling's left child is black and right child is red
                        //(near child is red)
                        if (!sibling.Left.IsRed)
                        {
                            sibling.Right.IsRed = false;//recolor sibling's right child to black
                            sibling.IsRed = true; //recolor sibling to red
                            LeftRotate(sibling);//rotate sibling to the left
                            sibling = node.Parent.Left;//update sibling after rotation
                        }

                        //case 2.2.1: sibling's left child is red (far child)
                        sibling.IsRed = node.Parent.IsRed;//recolor sibling with parent's color
                        node.Parent.IsRed = false;//recolor parent to black
                        sibling.Left.IsRed = false;//recolor sibling's right child to black
                        RightRotate(node.Parent);//rotate parent to the right
                        node = _Root;//set node to root to break out of the loop

                    }

                }
            }
            node.IsRed = false;//ensure the root is black before exiting
        }
        private Node Minimum(Node node)
        {

            while (node.Left != null)
                node = node.Left;
            
            return node;
        }
    }
}
