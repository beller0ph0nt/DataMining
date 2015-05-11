using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    /// <summary>
    /// Числовой аттрибут
    /// </summary>
    public class NumericalAttribute : AttributeBase<double>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор аттрибута</param>
        /// <param name="values">Список предикторных переменных</param>
        public NumericalAttribute(int id, List<double> values)
            : base(id, AttributType.Numerical, values)
        {
            SplitVar = new NumericalSplit();
        }

        /// <summary>
        /// Разделяет аттрибут на подмножества
        /// </summary>
        /// <returns>Список разделений</returns>
        public override List<AttributeBase<double>> Split()
        {
            SplitVar.CalcBestSplit(this.Values);

            return SplitVar.Splits.ConvertAll(l => (AttributeBase<double>)new NumericalAttribute(Id, l));
        }
    }
}
