using System.Data;
using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public interface ITree
    {
		void Load(string file);		// загружает дерево
		void Save(string file);		// сохраняет дерево
		void Calc(DataRow input);	// вычисляет выходное значение
    }
}