using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    /// <summary>
    /// Категориальный аттрибут
    /// </summary>
    public class CategoricalAttribute : AttributeBase<int>
    {
        public int CategoriesCounter { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор аттрибута</param>
        /// <param name="values">Список предикторных переменных</param>
        /// <param name="categoriesCounter">Счетчик категорий</param>
        public CategoricalAttribute(int id, List<int> values, int categoriesCounter)
            : base(id, AttributType.Categorical, values)
        {
            SplitVar = new CategoricalSplit();
            SplitVar.Categories = categoriesCounter;
        }

        /// <summary>
        /// Разделяет аттрибут на подмножества
        /// </summary>
        /// <returns>Список разделений</returns>
        public override List<AttributeBase<int>> Split()
        {
            SplitVar.CalcBestSplit(this.Values);

            return SplitVar.Splits.ConvertAll(l => (AttributeBase<int>)new CategoricalAttribute(Id, l, CategoriesCounter));
        }
    }
}
