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
        // тип аттрибута
        public AttributType Type { get; private set; }
        // список предикторных переменных
        public List<Cell> Values { get; set; }
        // переменная разбиения
        public SplitBase<T> SplitVar { get; protected set; }

        protected AttributeBase(AttributType type, List<T> values)
        {
            Type = type;
            //Values = values;
        }

        // разделяет аттрибут на подмножества
        public abstract List<AttributeBase<T>> Split();
    }
}
