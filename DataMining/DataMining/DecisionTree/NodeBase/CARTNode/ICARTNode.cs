using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public interface ICARTNode<T> : IBinaryNode<T>
    {
        // ссылка на родителя, преобразованая к интерфейсу ICARTNode
        new ICARTNode<T> Parent { get; }

        // кол-во элементов, прошедших через узел
        long ElementsCount { get; }

        // кол-во элементов каждого класса, прошедщих через узел
        IDictionary<string, long> ClassElementsCount { get; }
    }
}
