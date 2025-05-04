using System;
using System.Collections.Generic;
using System.Xml.Schema;

public class BinarySearchTree<T> where T : IComparable<T>

{
    public BinaryTreeNode<T> Root { get; private set; }

    public BinarySearchTree()
    {
        Root = null;
    }

    public void Insert(T value)
    {
        Root = Insert(Root, value);
    }

    private BinaryTreeNode<T> Insert(BinaryTreeNode<T> node, T value)
    {
        if (node is null)
            return new BinaryTreeNode<T>(value);
        else if (value.CompareTo(node.Value) < 0)
           node.Left= Insert(node.Left, value);
        else if (value.CompareTo(node.Value) > 0)
            node.Right = Insert(node.Right, value);

            return node;
    }

    public void InOrderTraversal()
    {
        InOrderTraversal(Root);
        Console.WriteLine();
    }
    private void InOrderTraversal(BinaryTreeNode<T> node)
    {
        if(node != null)
        {
            InOrderTraversal(node.Left);
            Console.Write(node.Value.ToString() + " ");
            InOrderTraversal(node.Right);
        }
    }

    public void PreOrderTraversal()
    {
        PreOrderTraversal(Root);
        Console.WriteLine();
    }
    
    private void PreOrderTraversal(BinaryTreeNode<T> node)
    {
        if (node!=null)
        {
            Console.Write(node.Value.ToString() + " ");
            PreOrderTraversal(node.Left);
            PreOrderTraversal(node.Right);
        }
    }

    public void PostOrderTraversal()
    {
        PostOrderTraversal(Root);
        Console.WriteLine() ;
    }
    
    public void PostOrderTraversal(BinaryTreeNode<T> node)
    {
        if (node != null)
        {
            PostOrderTraversal(node.Left);
            PostOrderTraversal(node.Right);
            Console.Write(node.Value.ToString() + " ");
        }
    }

    public void PrintTree(BinaryTreeNode<T> node, int spaces =0)
    {
        int count = 10;//Distance between levels to adjust the visual representation.

        if (node is null)
            return;

        spaces += count;

        PrintTree(node.Right, spaces);//Print the right first, then root, then left

        Console.WriteLine();
        for (int i = count; i < spaces; i++)
            Console.Write(" ");

        Console.WriteLine(node.Value.ToString());//prints the curren node after spaces
        PrintTree(node.Left, spaces);//recur on the left child

    }

    public bool Contains(T item)
    {
        return Search(item) != null;
    }
    public BinaryTreeNode<T> Search(T value)
    {
        if (Root is null || Root.Value.Equals(value))
            return Root;

        return Search(Root, value);
    }
    private BinaryTreeNode<T> Search(BinaryTreeNode<T> node,T Value)
    {
        if (node is null || node.Value.Equals(Value))
            return node;

        if (Value.CompareTo(node.Value) < 0)
           return Search(node.Left, Value);
        else 
            return Search(node.Right, Value);
        

    }

}




