using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;





public class AVLTree<T> where T: IComparable<T>
{
    public AVLNode<T> Root {  get;private set; }


   
    public void Insert(T value)
    {
        Root = Insert(Root, value);
    }
    private AVLNode<T> Insert(AVLNode<T> node, T value)
    {
        if (node == null)
            return new AVLNode<T>(value);

        if(value.CompareTo(node.Value) < 0)
            node.Left = Insert(node.Left, value);
        else if (value.CompareTo(node.Value) > 0)
            node.Right = Insert(node.Right, value);
        else return node; //Duplicate values are not allowed

        UpdataHeight(node);

        return Balance(node);
    }

    public void Delete(T value)
    {
        Root = DeleteNode(Root, value);
    }
   
    private AVLNode<T> DeleteNode(AVLNode<T> node, T value)
    {
        if (node is null)
            return node;

        //Step 1: Perform standard BST delete
        if (value.CompareTo(node.Value) < 0)
            node.Left = DeleteNode(node.Left, value);
        else if (value.CompareTo(node.Value) > 0)
            node.Right = DeleteNode(node.Right, value);
        else
        {
            /*
             * if the node to be deleted has one or no child 
             * simply remove the node and return the non-null child (if any)
             * 
             */

            //Node with only one child or node child
            if (node.Left is null)
                return node.Right;

            else if (node.Right is null)
                return node.Left;


            /*
             * if the node to be delete have two children
             * find the smallest node in the right subtree (inorder successor) then 
             * copy its value to the node to be deleted, then recursivly delete the inorder successor.
             */

            //Node with two children: get the inorder successor (smallest in the right subtree)
            AVLNode<T> temp = MinValNode(node.Right);

            //copy the inorder successor's data to this node
            node.Value = temp.Value;

            //delete the inorder successor
            node.Right = DeleteNode(node.Right, temp.Value);

        }

        UpdataHeight(node);
        return Balance(node);
    }
    
    private AVLNode<T> MinValNode(AVLNode<T> node)
    {
        /*
        the minimum value is always located in the lefmost node.
        this because for any given node in a BST
        all values in the left subtree are less than the value of the node
        and all values in the right subtrere are greater.
         */


        AVLNode<T> current = node;
        while (current.Left != null)
        {
            current = current.Left;
        }
        return current;

    }
    private void UpdataHeight(AVLNode<T> node)
    {
        //this will add 1 to the max hight and update the node height
        node.Height =1 + Math.Max(Height(node.Left), Height(node.Right));
    }

    public bool Exists(int value)
    {
        return Search(value) != null;
    }
    public AVLNode<T> Search(int value)
    {
        return Search(Root, value);
    }
    private AVLNode<T> Search(AVLNode<T> node, int value)
    {
        if (node is null || value.Equals( node.Value))
            return node;

        if(value.CompareTo(node.Value) < 0)
            return  Search(node.Left, value);
        else 
            return Search(node.Right, value);
    }
    private int Height(AVLNode<T> node)
    {
        //this will get the height of the ndoe, incase the node is null it will return 0
        return node != null ? node.Height : 0;
    }

    private int GetBalanceFactor(AVLNode<T> node)
    {
        return node != null ? Height(node.Left) - Height(node.Right) : 0;
    }

    private AVLNode<T> RightRotate(AVLNode<T> originalRoot)
    {
        /*
         * the left child of the node becomes the new root of the subtree.
         * the original root becomes the new right child of the new root.
         * if the new root already had a right child, it becomes the left child of the new right child(the original root).
         */

        //the lef child of the node becomes the new root of the subtree
        AVLNode<T> NewRoot = originalRoot.Left;
        
        //save original right child temporarly
        AVLNode<T> OriginalRightChild = NewRoot.Right;

        //the roiginal root node becomes the right child of the new root
        NewRoot.Right = originalRoot;
        
        //the original right child becomes the new right child of the original root
        originalRoot.Left = OriginalRightChild;


        //after the rotation, the height of the nodes may not longer be correct.
        UpdataHeight(originalRoot);
        UpdataHeight(NewRoot);

        //finally return the NewRoot, which is now the root of this subtree after rotation.
        return NewRoot;
    }

    private AVLNode<T> LeftRotate(AVLNode<T> originalRoot)
    {
        /*
         * the right child of the node becomes the new root of the subtree.
         * the original root becomes the new left child of the new root.
         * if the new root already had a left child, it becomes the right child of the new right child(the original root).
         */

        //the right child of the node becomes the new root of the subtree
        AVLNode<T> NewRoot = originalRoot.Right;

        //save original left child temporarly
        AVLNode<T> OriginalLeftChild = NewRoot.Left;

        //the roiginal root node becomes the left child of the new root
        NewRoot.Left = originalRoot;

        //the original left child becomes the new right child of the original root
        originalRoot.Right = OriginalLeftChild;


        //after the rotation, the height of the nodes may not longer be correct.
        UpdataHeight(originalRoot);
        UpdataHeight(NewRoot);

        //finally return the NewRoot, which is now the root of this subtree after rotation.
        return NewRoot;
    }

    private AVLNode<T> Balance (AVLNode<T> node)
    {

        //when the node is too left heave we perform right rotation, other wise we perform left rotation

        int balanceFactor = GetBalanceFactor(node);

        //RR - Right Rotation case : Parent BF = -2, Child BF = -1 or 0
        if(balanceFactor < -1 && GetBalanceFactor(node.Right) <= 0 )
           return LeftRotate(node);
        
        
        //LL - Left Rotation case : Parent BF = 2, Child BF = 1 or 0
        if(balanceFactor > 1 && GetBalanceFactor(node.Left) >= 0)
            return RightRotate(node);

        //LR - Left Rotation case : Parent BF = 2, Child BF = -1 which means its too heave on the right side
        if(balanceFactor > 1 && GetBalanceFactor (node.Left) < 0)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }
        
        //LR - Left Rotation case : Parent BF = 2, Child BF = 1 which means its too heavy on the left side 
        else if(balanceFactor < -1 && GetBalanceFactor (node.Right) > 0)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    public void PrintTree()
    {
        PrintTree(Root, "", true);
    }
    private void PrintTree(AVLNode<T> node, string indent, bool last)
    
    {
        if(node != null)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("R----");
                indent += "     ";
            }
            else
            {
                Console.Write("L----");
                indent += "|    ";
            }
            Console.WriteLine(node.Value);
            PrintTree(node.Left, indent, false);
            PrintTree(node.Right, indent, true);
        }

    }


}

