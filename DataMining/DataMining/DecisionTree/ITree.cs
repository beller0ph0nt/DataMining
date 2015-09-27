using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public interface ITree
    {
        // загружает дерево
		void Load(string file);	// имя файла

        // сохраняет дерево
		void Save(string file);	// имя файла

        // вычисляет выходное значение
		void Calc(List<double> input);	// входной вектор

        //int CreateRoot();
        //int CreateNode(int parentId);
        //int CreateLeaf(int parentId);
    }
}
