using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    /// <summary>
    /// Типы аттрибутов
    /// </summary>
    public enum AttributType
    {
        Numerical,      // Числовой
        Categorical     // Категориальный
    }

    /// <summary>
    /// Базовый класс аттрибутов
    /// </summary>
    public abstract class AttributeBase<T>
    {
        #region Свойства

        /// <summary>
        /// Идентификатор аттрибута
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Тип аттрибута
        /// </summary>
        public AttributType Type { get; private set; }

        /// <summary>
        /// Список значений аттрибута
        /// </summary>
        public List<T> Values { get; private set; }

        #endregion

        protected AttributeBase(int id, AttributType type, List<T> values)
        {
            Id = id;
            Type = type;
            Values = values;
        }

        #region Методы

        /// <summary>
        /// Метод, разделяющий аттрибут на подмножества
        /// </summary>
        /// <returns>Список разделений</returns>
        public abstract List<AttributeBase<T>> Split();

        #endregion
    }
}
