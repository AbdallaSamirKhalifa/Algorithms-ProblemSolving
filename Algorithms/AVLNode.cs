public class AVLNode<T>
{
    public T Value { get; set; }
    public AVLNode<T> Right { get; set; }
    public AVLNode<T> Left { get; set; }


    public int Height { get; set; }

    public AVLNode(T value)
    {
        Value = value;
        Height = 1;//Initially when the node is created, its height is set to 1.
        Left = null;
        Right = null;

    }
}

