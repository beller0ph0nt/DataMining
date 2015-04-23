using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree
{
    /// <summary>
    /// Интерфейс абстрактной фабрики узлов
    /// </summary>
    /// <typeparam name="T">Тип данных, хранимый в узле</typeparam>
    public interface INodeFactory<T>
    {
        INodeBase<T> GetRoot();
        INodeBase<T> GetNode(INodeBase<T> parent);
        INodeBase<T> GetLeaf(INodeBase<T> parent);
    }
}
