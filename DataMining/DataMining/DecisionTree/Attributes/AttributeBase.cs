using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;
using DataMining.DecisionTree.Elements;

namespace DataMining.DecisionTree.Attributes
{
    // типы аттрибутов
    public enum AttributType
    {
        Answer,         // ответный
        Numerical,      // числовой
        Categorical     // категориальный
    }
		
    // базовый класс аттрибутов
    public abstract class AttributeBase<T>
    {
        #region Свойства

        // тип аттрибута
        public AttributType Type { get; private set; }

        // список предикторных переменных
        //public List<T> Values { get; private set; }
        public List<Cell> Values { get; set; }

        /// <summary>
        /// Переменная разбиения
        /// </summary>
        public SplitBase<T> SplitVar { get; protected set; }

        #endregion

        protected AttributeBase(AttributType type, List<T> values)
        {
            Type = type;
            //Values = values;
        }

        #region Методы

        /// <summary>
        /// Разделяет аттрибут на подмножества
        /// </summary>
        /// <returns>Список разделений</returns>
        public abstract List<AttributeBase<T>> Split();

        #endregion
    }
}
