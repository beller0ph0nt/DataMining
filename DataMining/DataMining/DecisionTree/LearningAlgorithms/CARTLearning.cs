using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.LearningAlgorithm
{
    public class CARTLearning : ILearningAlgorithm
    {
		private CARTTree<Split> _tree;	// обучаемое дерево
        private ISplitQualityAlgorithm _splitQuality;

		public CARTLearning(CARTTree<Split> tree)
        {
            _tree = tree;
            _splitQuality = new GiniSplit();
        }

		public void Training(DataTable table)
		{
			//int id = _tree.CreateRoot();	// Создать корень у дерева
			//Split s = new Split(table);		// Получить лучшее разбиение

			// Создать у текущего узла два листа
			// В левый лист поместить [0] разбиение
			// В правый лист поместить [1] разбиение

			// Цикл по всем листам
			// Проверяем родителя листа. Выходим из цикла, когда у всех родителей


			//-----------------------------------------
			// Вначале должна быть инициаллизация дерева. Должен появиться корень и 2 листа
			// Далее начинается цикл по всем листам в дереве.
			// Вычисляем лучшее разбиение для текущего листа.
			// Если в разбиении получилось только одно множество, то такое разбиение пропускаем.
			// Если в разбиении 2 подмножества, то родительский лист надо заменить на узел и к нему привязать 2-а листа и заполнить их разбиениями.
		}
    }
}
