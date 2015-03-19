using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public interface ITree
    {
        #region Методы

        /// <summary>
        /// Метод, загружающий дерево
        /// </summary>
        /// <param name="file">Имя файла</param>
        void Load(string file);

        /// <summary>
        /// Метод, сохраняющий дерево
        /// </summary>
        /// <param name="file">Имя файла</param>
        void Save(string file);

        /// <summary>
        /// Метод, вычисляющий выходное значение
        /// </summary>
        /// <param name="input">Входной вектор</param>
        void Calc(List<double> input);              // !!! По иде в функцию должен передоваться список АТРИБУТОВ а не даблов, а возвращаться выходное значение

        int CreateRoot();
        int CreateNode(int parentId);
        int CreateLeaf(int parentId);

        #endregion
    }
}
