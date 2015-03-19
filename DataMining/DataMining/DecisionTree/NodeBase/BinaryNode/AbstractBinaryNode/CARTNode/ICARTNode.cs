using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public interface ICARTNode<T> : IBinaryNode<T>
    {
        #region Свойства

        /// <summary>
        /// Ссылка на родителя, преобразованая к интерфейсу ICARTNode
        /// </summary>
        new ICARTNode<T> Parent { get; }

        /// <summary>
        /// Количество элементов, прошедших через узел
        /// </summary>
        long ElementsCount { get; }

        /// <summary>
        /// Правило узла
        /// </summary>
        IRule Rule { get; set; }

        /// <summary>
        /// Количество элементов каждого класса, прошедщих через узел
        /// </summary>
        IDictionary<string, long> ClassElementsCount { get; }

        #endregion
    }
}
