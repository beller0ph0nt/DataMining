using System.Data;

namespace DataMining.DecisionTree {
    public interface ITree {
		void Load(string file);
		void Save(string file);
		void Calc(DataRow input);
	}
}