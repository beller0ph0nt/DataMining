using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.Elements
{
    public class Cell
    {
        /// <summary>
        /// идентификатор
        /// необходим для связи столбцов таблицы
        /// </summary>
        public int id;

        /// <summary>
        /// значение
        /// double - если элемент численного типа
        /// int - если элемент категориального типа
        /// </summary>
        public object val;
    }
}
