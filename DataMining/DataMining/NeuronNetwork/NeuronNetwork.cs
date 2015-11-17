using System;
using System.Collections.Generic;

namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Класс нейросети
    /// </summary>
    public class NeuronNetwork : INeuronNetwork
    {
        private List<ILayer> _layers;   // Список слоев
        private int _layerCount;        // Счетчик слоев
        private int _neuronCount;       // Счетчик нейронов
        private int _synapseCount;      // Счетчик связей

        #region Свойства

        /// <summary>
        /// Список слоев
        /// </summary>
        public List<ILayer> Layers { get { return _layers; } }

        /// <summary>
        /// Счетчик слоев
        /// </summary>
        public int LayerCount { get { return _layerCount; } }

        /// <summary>
        /// Счетчик нейронов
        /// </summary>
        public int NeuronCount { get { return _neuronCount; } }

        /// <summary>
        /// Счетчик связей
        /// </summary>
        public int SynapseCount { get { return _synapseCount; } }

        #endregion

        /// <summary>
        /// Конструктор нейросети
        /// </summary>
        /// <param name="activationFunction">Активационая функция нейронов</param>
        /// <param name="layersStructure">Структура слоев нейросети</param>
        public NeuronNetwork(IActivationFunction activationFunction, params uint[] layersStructure)
        {
            try
            {
                if (layersStructure.Length < 2)
                    throw new Exception("В сети должно быть минимум 2 слоя (входной и выходной)");

                _layerCount = 0;
                _neuronCount = 0;
                _synapseCount = 0;
                _layers = new List<ILayer>();

                int i, j;
                ILayer newLayer;
                INeuron newNeuron;
                ISynapse newSynapse;
                Random grn = new Random();

                // Создаем слой S нейронов
                newLayer = new Layer(++_layerCount);
                for (i = 0; i < layersStructure[0]; i++)
                {
                    newNeuron = new NeuronS(++_neuronCount, activationFunction.Clone());
                    newNeuron.Threshold = grn.NextDouble();
                    newLayer.Add(newNeuron);
                }
                _layers.Add(newLayer);

                // Создаем слои A нейронов
                for (i = 1; i < layersStructure.Length - 1; i++)
                {
                    newLayer = new Layer(++_layerCount);
                    for (j = 0; j < layersStructure[i]; j++)
                    {
                        newNeuron = new NeuronA(++_neuronCount, activationFunction.Clone());
                        newNeuron.Threshold = grn.NextDouble();
                        newLayer.Add(newNeuron);
                    }
                    _layers.Add(newLayer);
                }

                // Создаем слой R нейронов
                newLayer = new Layer(++_layerCount);
                for (i = 0; i < layersStructure[layersStructure.GetUpperBound(0)]; i++)
                {
                    newNeuron = new NeuronR(++_neuronCount, activationFunction.Clone());
                    newNeuron.Threshold = grn.NextDouble();
                    newLayer.Add(newNeuron);
                }
                _layers.Add(newLayer);

                // Создаем синапсы между нейронами
                for (i = 0; i < _layers.Count - 1; i++)
                {
                    foreach (Neuron leftNeuron in _layers[i].Neurons)
                    {
                        for (j = i + 1; j < _layers.Count; j++)
                        {
                            foreach (Neuron rightNeuron in _layers[j].Neurons)
                            {
                                newSynapse = new Synapse(++_synapseCount, leftNeuron, rightNeuron, 0);
                                newSynapse.Weight = grn.NextDouble();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Конструктор нейросети
        /// </summary>
        /// <param name="weightMatrix">Матрица весов</param>
        /// <param name="inputNeurons">Массив индексов входных нейронов</param>
        /// <param name="outputNeurons">Массив индексов выходных нейронов</param>
        /// <param name="f"></param>
        public NeuronNetwork(double[][] weightMatrix, int[] inputNeurons, int[] outputNeurons, IActivationFunction f)
        {
            int[] inputs = new int[inputNeurons.Length];
            int[] outputs = new int[outputNeurons.Length];
            Dictionary<int, Neuron> neurons = new Dictionary<int, Neuron>();        // Все нейроны сети
            Dictionary<int, Synapse> synapses = new Dictionary<int, Synapse>();     // Все синапсы сети

            inputNeurons.CopyTo(inputs, 0);
            outputNeurons.CopyTo(outputs, 0);

            // Создаем нейроны
            for (int i = 0; i < weightMatrix.Length; i++)
                neurons.Add(neurons.Count, new NeuronA(neurons.Count, f.Clone()));

            // Создаем синапсы
            for (int i = 0; i < weightMatrix.Length; i++)
                for (int j = 0; j < weightMatrix[i].Length; j++)
                    if (weightMatrix[i][j] != 0)
                        synapses.Add(synapses.Count, new Synapse(synapses.Count, neurons[i], neurons[j], weightMatrix[i][j]));
        }

        #region Методы

        /// <summary>
        /// Метод, загружающий сеть
        /// </summary>
        /// <param name="file">Имя файла</param>
        public void LoadNetwork(string file)
        {
        }

        /// <summary>
        /// Метод, сохраняющий сеть
        /// </summary>
        /// <param name="file">Имя файла</param>
        public void SaveNetwork(string file)
        {
        }

        /// <summary>
        /// Метод, вычисляющий выходной вектор нейросети
        /// </summary>
        /// <param name="input">Входной вектор</param>
        public void Calc(List<double> input)
        {
            try
            {
                if (input.Count != _layers[0].Neurons.Count)
                    throw new Exception("Некоректная размерность входного вектора");

                // Заполняем сенсоры
                for (int i = 0; i < _layers[0].Neurons.Count; i++)
                    _layers[0].Neurons[i].Axon = input[i];

                // Последовательно вычисляем состояния всех нейронов
                for (int i = 1; i < _layers.Count; i++)
                    _layers[i].Calc();
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}
