using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public interface ITree
    {
        #region Методы

        // загружает дерево
        // file - Имя файла
        void Load(string file);

        // сохраняет дерево
        // file - имя файла
        void Save(string file);

        // вычисляет выходное значение
        // input - входной вектор
        void Calc(List<double> input);	// !!! По иде в функцию должен передоваться список АТРИБУТОВ а не даблов, а возвращаться выходное значение

        //int CreateRoot();
        //int CreateNode(int parentId);
        //int CreateLeaf(int parentId);

        #endregion
    }
}
