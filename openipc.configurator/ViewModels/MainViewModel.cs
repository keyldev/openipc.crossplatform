
using ReactiveUI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace openipc.configurator.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static

        #region FiveEightFrequency
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
        private string _fiveEightSelectedItem;

        public string FiveEightSelectedItem
        {
            get { return _fiveEightSelectedItem; }
            set
            {
                _fiveEightSelectedItem = this.RaiseAndSetIfChanged(ref _fiveEightSelectedItem, value);

                var match = Regex.Match(FiveEightSelectedItem ?? "", @"\[(\d+)\]");
                FiveEightChannelFormatted = "channel=" + (match.Success ? match.Groups[1].Value : FiveEightSelectedItem);
            }
        }
        private string _fiveEightChannelFormatted;

        public string FiveEightChannelFormatted
        {
            get => _fiveEightChannelFormatted;
            set => this.RaiseAndSetIfChanged(ref _fiveEightChannelFormatted, value);
        }

        public List<string> FiveEightFrequencyList
        {
            get { return _fiveEightFrequencyList; }
            set { _fiveEightFrequencyList = this.RaiseAndSetIfChanged(ref _fiveEightFrequencyList, value); }
        }
        #endregion
        #region FiveEightTxPower
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

        private string _fiveEightTxPowerSelectedItem;

        public string FiveEightTxPowerSelectedItem
        {
            get { return _fiveEightTxPowerSelectedItem; }
            set
            {
                _fiveEightTxPowerSelectedItem = this.RaiseAndSetIfChanged(ref _fiveEightTxPowerSelectedItem, value);
                FiveEightTxFormatted = "driver_txpower_override=" + value;
            }
        }
        private string _fiveEightTxFormatted;

        public string FiveEightTxFormatted
        {
            get { return _fiveEightTxFormatted; }
            set { _fiveEightTxFormatted = this.RaiseAndSetIfChanged(ref _fiveEightTxFormatted, value); }
        }


        public List<string> FiveEightTxPowerList
        {
            get { return _fiveEightTxPower; }
            set { _fiveEightTxPower = this.RaiseAndSetIfChanged(ref _fiveEightTxPower, value); }
        }
        #endregion
        #region TwoFourFrequency
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
        private string _twoFourFrequencySelectedItem;

        public string TwoFourFrequencySelectedItem
        {
            get { return _twoFourFrequencySelectedItem; }
            set
            {
                _twoFourFrequencySelectedItem = this.RaiseAndSetIfChanged(ref _twoFourFrequencySelectedItem, value);
                var match = Regex.Match(TwoFourFrequencySelectedItem ?? "", @"\[(\d+)\]");
                TwoFourFrequencyFormatted = "channel=" + (match.Success ? match.Groups[1].Value : "");
            }
        }
        private string _twoFourFrequencyFormatted;

        public string TwoFourFrequencyFormatted
        {
            get { return _twoFourFrequencyFormatted; }
            set { _twoFourFrequencyFormatted = this.RaiseAndSetIfChanged(ref _twoFourFrequencyFormatted, value); }
        }

        public List<string> TwoFourFrequencyList
        {
            get { return _twoFourFrequency; }
            set { _twoFourFrequency = this.RaiseAndSetIfChanged(ref _twoFourFrequency, value); }
        }
        #endregion
        #region TwoFourTxPower
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

        private string _twoFourTxPowerSelectedItem;

        public string TwoFourTxPowerSelectedItem
        {
            get { return _twoFourTxPowerSelectedItem; }
            set
            {
                _twoFourTxPowerSelectedItem = this.RaiseAndSetIfChanged(ref _twoFourTxPowerSelectedItem, value);
                TwoFourTxPowerFormatted = "txpower=" + value;
            }
        }
        private string _twoFourTxPowerFormatted;

        public string TwoFourTxPowerFormatted
        {
            get { return _twoFourTxPowerFormatted; }
            set { _twoFourTxPowerFormatted = this.RaiseAndSetIfChanged(ref _twoFourTxPowerFormatted, value); }
        }


        public List<string> TwoFourTxPowerList
        {
            get { return _twoFourTxPower; }
            set { _twoFourTxPower = this.RaiseAndSetIfChanged(ref _twoFourTxPower, value); }
        }
        #endregion
        #region MSCIndex
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
        private string _mscIndexSelectedItem;

        public string MscIndexSelectedItem
        {
            get { return _mscIndexSelectedItem; }
            set
            {
                _mscIndexSelectedItem = this.RaiseAndSetIfChanged(ref _mscIndexSelectedItem, value);
                MscIndexFormatted = "mcs_index=" + value;
            }
        }
        private string _mscIndexFormatted;

        public string MscIndexFormatted
        {
            get { return _mscIndexFormatted; }
            set { _mscIndexFormatted = this.RaiseAndSetIfChanged(ref _mscIndexFormatted, value); }
        }

        public List<string> MscIndexList
        {
            get { return _mscIndex; }
            set { _mscIndex = this.RaiseAndSetIfChanged(ref _mscIndex, value); }
        }
        #endregion
        #region STBC
        private List<string> _stbc = new List<string>
        {
            "0",
            "1"
        };
        private string _stbcSelectedItem;

        public string StbcSelectedItem
        {
            get { return _stbcSelectedItem; }
            set
            {
                _stbcSelectedItem = this.RaiseAndSetIfChanged(ref _stbcSelectedItem, value);
                StbcFormatted = "stbc=" + value;
            }
        }
        private string _stbcFormatted;

        public string StbcFormatted
        {
            get { return _stbcFormatted; }
            set { _stbcFormatted = this.RaiseAndSetIfChanged(ref _stbcFormatted, value); }
        }

        public List<string> StbcList
        {
            get { return _stbc; }
            set { _stbc = this.RaiseAndSetIfChanged(ref _stbc, value); }
        }
        #endregion
        #region LDPC
        private List<string> _ldpc = new List<string>
        {
            "0",
            "1"
        };
        private string _ldpcSelectedItem;

        public string LpdcSelectedItem
        {
            get { return _ldpcSelectedItem; }
            set { _ldpcSelectedItem = this.RaiseAndSetIfChanged(ref _ldpcSelectedItem, value);
                LdpcFormatted = "ldpc=" + value;
            }
        }
        private string _ldpcFormatted;

        public string LdpcFormatted
        {
            get { return _ldpcFormatted; }
            set { _ldpcFormatted = this.RaiseAndSetIfChanged(ref _ldpcFormatted, value); }
        }

        public List<string> LdpcList
        {
            get { return _ldpc; }
            set { _ldpc = this.RaiseAndSetIfChanged(ref _ldpc, value); }
        }
        #endregion
        #region FECK
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

        private string _fecKSelectedItem;

        public string FecKSelectedItem
        {
            get { return _fecKSelectedItem; }
            set { _fecKSelectedItem = this.RaiseAndSetIfChanged(ref _fecKSelectedItem, value);
                FecKFormatted = "fec_k=" + value;
            }
        }
        private string _fecKFormatted;

        public string FecKFormatted
        {
            get { return _fecKFormatted; }
            set { _fecKFormatted = this.RaiseAndSetIfChanged(ref _fecKFormatted, value); }
        }

        public List<string> FecKList
        {
            get { return _fecK; }
            set { _fecK = this.RaiseAndSetIfChanged(ref _fecK, value); }
        }
        #endregion
        #region FECN
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

        private string _fecNSelectedItem;

        public string FecNSelectedItem
        {
            get { return _fecNSelectedItem; }
            set { _fecNSelectedItem = this.RaiseAndSetIfChanged(ref _fecNSelectedItem, value);
                FecNFormatted = "fec_n=" + value;
            }
        }
        private string _fecNFormatted;

        public string FecNFormatted
        {
            get { return _fecNFormatted; }
            set { _fecNFormatted = this.RaiseAndSetIfChanged(ref _fecNFormatted, value); }
        }


        public List<string> FecNList
        {
            get { return _fecN; }
            set { _fecN = this.RaiseAndSetIfChanged(ref _fecN, value); }
        }
        #endregion
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
