
using ReactiveUI;
using System.Collections.Generic;
using System.Windows.Input;

namespace openipc.configurator.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static

        private string _testText;

        public string TestText
        {
            get { return _testText; }
            set { _testText = this.RaiseAndSetIfChanged(ref _testText, value); }
        }

        private List<string> _fiveEightFrequencyList = new List<string>
        {
            "5180 MHz [36]",
            "5200 MHz [40]",
            "5220 MHz [44]",
            "5240 MHz [48]",
            "5260 MHz [52]",
            "5280 MHz [56]",
            "5300 MHz [60]",
            "5320 MHz [64]",
            "5500 MHz [100]",
            "5520 MHz [104]",
            "5540 MHz [108]",
            "5560 MHz [112]",
            "5580 MHz [116]",
            "5600 MHz [120]",
            "5620 MHz [124]",
            "5640 MHz [128]",
            "5660 MHz [132]",
            "5680 MHz [136]",
            "5700 MHz [140]",
            "5720 MHz [144]",
            "5745 MHz [149]",
            "5765 MHz [153]",
            "5785 MHz [157]",
            "5805 MHz [161]",
            "5825 MHz [165]",
            "5845 MHz [169]",
            "5865 MHz [173]",
            "5885 MHz [177]"
        };

        public List<string> FiveEightFrequencyList
        {
            get { return _fiveEightFrequencyList; }
            set { _fiveEightFrequencyList = this.RaiseAndSetIfChanged(ref _fiveEightFrequencyList, value); }
        }
        private List<string> _fiveEightTxPower = new List<string>
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "58",
            "60",
            "63"
        };

        public List<string> FiveEightTxPowerList
        {
            get { return _fiveEightTxPower; }
            set { _fiveEightTxPower = this.RaiseAndSetIfChanged(ref _fiveEightTxPower, value); }
        }

        private List<string> _twoFourFrequency = new List<string>
        {
            "2412 MHz [1]",
            "2417 MHz [2]",
            "2422 MHz [3]",
            "2427 MHz [4]",
            "2432 MHz [5]",
            "2437 MHz [6]",
            "2442 MHz [7]",
            "2447 MHz [8]",
            "2452 MHz [9]",
            "2457 MHz [10]",
            "2462 MHz [11]",
            "2467 MHz [12]",
            "2472 MHz [13]",
            "2484 MHz [14]"
        };

        public List<string> TwoFourFrequencyList
        {
            get { return _twoFourFrequency; }
            set { _twoFourFrequency = this.RaiseAndSetIfChanged(ref _twoFourFrequency, value); }
        }
        private List<string> _twoFourTxPower = new List<string>
        {
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "58"
        };

        public List<string> TwoFourTxPowerList
        {
            get { return _twoFourTxPower; }
            set { _twoFourTxPower = this.RaiseAndSetIfChanged(ref _twoFourTxPower, value); }
        }
        private List<string> _mscIndex = new List<string>
        {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"
        };

        public List<string> MscIndexList
        {
            get { return _mscIndex; }
            set { _mscIndex = this.RaiseAndSetIfChanged(ref _mscIndex, value); }
        }
        private List<string> _stbc = new List<string>
        {
            "0",
            "1"
        };

        public List<string> StbcList
        {
            get { return _stbc; }
            set { _stbc = this.RaiseAndSetIfChanged(ref _stbc, value); }
        }
        private List<string> _ldpc = new List<string>
        {
            "0",
            "1"
        };

        public List<string> LdpcList
        {
            get { return _ldpc; }
            set { _ldpc = this.RaiseAndSetIfChanged(ref _ldpc, value); }
        }
        private List<string> _fecK = new List<string>
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"
        };

        public List<string> FecKList
        {
            get { return _fecK; }
            set { _fecK = this.RaiseAndSetIfChanged(ref _fecK, value); }
        }
        private List<string> _fecN = new List<string>
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"
        };

        public List<string> FecNList
        {
            get { return _fecN; }
            set { _fecN = this.RaiseAndSetIfChanged(ref _fecN, value); }
        }

        private bool _radxaZero3w;

        public bool RadxaZero3W
        {
            get { return _radxaZero3w; }
            set { _radxaZero3w = this.RaiseAndSetIfChanged(ref _radxaZero3w, value); }
        }
        private bool _nvr;

        public bool Nvr
        {
            get { return _nvr; }
            set { _nvr = this.RaiseAndSetIfChanged(ref _nvr, value); }
        }
        private bool _openIpcChecked = true;

        public bool OpenIPCChecked
        {
            get { return _openIpcChecked; }
            set { _openIpcChecked = this.RaiseAndSetIfChanged(ref _openIpcChecked, value); }
        }

        #region BUTTONS

        public ICommand SaveFreq { get; set; }
        public ICommand RestartWFB { get; set; }
        public ICommand SaveCamera { get; set; }
        public ICommand RestartMajestic { get; set; }
        public ICommand SaveTLM { get; set; }
        public ICommand SaveVRX { get; set; }

        #endregion

#pragma warning restore CA1822 // Mark members as static

        public MainViewModel()
        {
            SaveFreq = ReactiveCommand.Create(() =>
            {

            });
            RestartWFB = ReactiveCommand.Create(() =>
            {

            });
            SaveCamera = ReactiveCommand.Create(() => { });
            RestartMajestic = ReactiveCommand.Create(() => { });
            SaveTLM = ReactiveCommand.Create(() => { });
            SaveVRX = ReactiveCommand.Create(() => { });
        }
    }
}
