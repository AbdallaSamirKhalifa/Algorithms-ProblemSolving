using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

public class BinaryTreeNode<T>
{
    public T Value { get; set; }
    public BinaryTreeNode<T> Left { get; set; }
    public BinaryTreeNode<T> Right{ get; set; }


    public BinaryTreeNode(T value)
    {
        Value = value;
        Right = null;
        Left = null;
    }

}

public class BinaryTree<T>
{
    public BinaryTreeNode<T> Root { get;set; }

    public BinaryTree()
    {
        Root = null;
    }

    /// <summary>
    /// Level-Order-Insertion
    /// </summary>
    /// <param name="value"></param>
    public void Insert(T value)
    {
        var newNode = new BinaryTreeNode<T>(value);

        if(Root is null)
        {
            Root = newNode;
            return;
        }
        Queue<BinaryTreeNode<T>> queue = new Queue<BinaryTreeNode<T>>();
        queue.Enqueue(Root);

        while(queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Left is null)
            {
                current.Left = newNode;
            
                return ;
            }
            else
                queue.Enqueue(current.Left);
           
            if(current.Right is null) 
             {
            
                current.Right = newNode;
                return;
            }
            else
                queue.Enqueue(current.Right);
        }
    }

    private void PreOrderTraversal(BinaryTreeNode<T> node)
    {
        //root-left child- right child

        if(node != null)
        {
            //it can be inserted in a queue and returns the queue if needed
            Console.Write(node.Value.ToString() + ", ");
            PreOrderTraversal(node.Left);
            PreOrderTraversal(node.Right);

        }

    }
    public void PreOrderTraversal()
    {
        PreOrderTraversal(Root);
        Console.WriteLine();
    }

    private void PostOrderTraversal(BinaryTreeNode<T> node)
    {
        //Left subtree then the right subtree then the current node

        if(node != null)
        {
            PostOrderTraversal(node.Left);
            PostOrderTraversal(node.Right);
            Console.Write(node.Value.ToString() + ", ");
        }
    }
    public void PostOrderTraversal()
    {
        PostOrderTraversal(Root);
        Console.WriteLine();
    }

    private void InOrderTraversal(BinaryTreeNode<T> node )
    {
        //lef - current - right

        if(node != null)
        {
            InOrderTraversal(node.Left);
            Console.Write(node.Value.ToString()+", ");
            InOrderTraversal(node.Right);
        }
    }

    public void InOrderTraversal()
    {
        InOrderTraversal(Root);
        Console.WriteLine();
    }

    public void LevelOrderTraversal()
    {
        if (Root is null)
            throw new NullReferenceException("The tree has no root.");

        Queue<BinaryTreeNode<T>> queue = new Queue<BinaryTreeNode<T>>();
        queue.Enqueue(Root);
        
        while (queue.Count > 0)
        {
            var curren = queue.Dequeue();
            Console.Write(curren.Value.ToString() + ", ");


            if (curren.Left != null)
                queue.Enqueue(curren.Left);
            
            if(curren.Right != null)
                queue.Enqueue(curren.Right);
            
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
}

