using System.Data;

namespace DataMining.DecisionTree {
    public interface ITree {
		void Load(string file);
		void Save(string file);
		object Calc(DataRow input);
	}
}