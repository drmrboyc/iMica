using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Finviz_Scraper
{
    using Factories;
    using Objects;
    using static Objects.ScanFilterSettings;

    public partial class FormMain : Form
    {
        int TotalOptions = 0;
        int TotalFilters = 0;

        List<string> FiltersToSkip = new List<string>();

        Timer ScanTimer;
        DateTime ScanTimerStartTime;

        List<string> Args = new List<string>();

        int TestingFilterOptionScanMax = 5;

        bool ScanInProgress = false;
        public static bool StartedByCommandLine = false;

        string BalloonTitle = "iMICA - Auto Scanning Disabled";
        string BalloonUpcomingScan = "No scheduled scan found.";
        string BalloonScanStatus = "No scan is currently running.";
        private ContextMenu m_TrayIconMenu;

        List<DateTime> smallScanTimes = new List<DateTime>();
        readonly Dictionary<DateTime, ScanType> ScheduledScanTimes = new Dictionary<DateTime, ScanType>
        {
            //{ new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute + 1, 00), ScanType.ALL_STOCKS_BASE_INFO },
            { new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 31, 00), ScanType.ALL_STOCKS_BASE_INFO },
            { new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 00, 00), ScanType.ALL_STOCKS_BASE_INFO },
            { new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 30, 00), ScanType.ALL_STOCKS_BASE_INFO },
            { new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 00), ScanType.ALL_STOCKS_BASE_INFO },
            { new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 55, 00), ScanType.ALL_STOCKS_BASE_INFO },
            { new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 00), ScanType.FILTER_OPTION },
            { new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 15, 00), ScanType.ALL_STOCKS_BASE_INFO },
            //{ new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 00), ScanType.FILTER_OPTION },
        };
        Dictionary<DateTime, ScanType> scanTimes = new Dictionary<DateTime, ScanType>();
        List<DateTime> scansRunToday = new List<DateTime>();
        Timer ScanManagerTimer;


        public FormMain()
        {
            InitializeComponent();

            InitializeTrayIconContextMenu();

            WriteToLog("SYSTEM", "iMICA Initializing...");
            DisableEnableUI(false);
            ScrapeFactory.ScannerPageUpdate += ScrapeFactory_ScannerPageUpdate;
            DBFactory.CheckFileSystemIntegrity();

            WriteToLog("SYSTEM", "File System check successful.");

            // If the form is closing disconnect and release the _db object in the factory.
            this.FormClosing += (s, e) =>
            {
                if (ScanInProgress)
                {
                    //  Ask "A scan is in progress. Are you sure you want to cancel the scan?"
                }
            };

            //  Load all the filters when we start the program.
            LoadDatabaseData();

            WriteToLog("SYSTEM", "Database check successful.");

            Args = Environment.GetCommandLineArgs().ToList<string>();
            if (Args.Count > 1 && (Args[1] == "-a" || Args[1] == "-automate"))
            {
                //StartedByCommandLine = true;
                //HandleCommandLineAutomation(Args);
            }
            else
            {
                WriteToLog("SYSTEM", "No valid command line parameters detected.");
            }

            DisableEnableUI(true);
                       
            //  Enable the Auto scanning manager.
            if (chkAutoScan.Checked)
            {
                WriteToLog("SYSTEM", "Auto Scanning is enabled...initializing the Scan Manager.");

                BalloonTitle = "iMICA - Auto Scanning Enabled";
                ResetScanTimes();
                StartScanManagerTimer();
                DisplayNextScheduledScan();
            }

            WriteToLog("SYSTEM", "iMICA initialization is complete.");
        }

        #region SCAN MANAGER TIMER & SCHEDULING

        private void ResetScanTimes(bool _overwriteTimeFilter = false)
        {
            WriteToLog("SCAN MANAGER", "Resetting Scan Times.");

            //  Clear the upcoming scheduled scans list.
            scanTimes = new Dictionary<DateTime, ScanType>();

            //  Add the upcoming scheduled scans to the today's scans list.
            foreach(KeyValuePair<DateTime, ScanType> scan in ScheduledScanTimes)
            {
                if (_overwriteTimeFilter)
                {
                    scanTimes.Add(scan.Key, scan.Value);
                }
                else if (scan.Key > DateTime.Now)
                {
                    scanTimes.Add(scan.Key, scan.Value);
                }
            }
        }

        private void StartScanManagerTimer()
        {
            ScanManagerTimer = new Timer
            {
                Interval = 55000
            };
            ScanManagerTimer.Tick -= ScanManagerTimer_Tick;
            ScanManagerTimer.Tick += ScanManagerTimer_Tick;
            ScanManagerTimer.Start();

            WriteToLog("SCAN MANAGER", "Scan timer is set to " + (ScanManagerTimer.Interval / 1000).ToString() + " seconds.");
        }

        private void StopScanManagerTimer()
        {
            ScanManagerTimer.Stop();
            ScanManagerTimer.Tick -= ScanTimer_Tick;
        }

        public void ScanManagerTimer_Tick(object sender, System.EventArgs e)
        {

            //  If we're currently in a scan, don't bother looking for another.
            if (ScanInProgress)
            {
                return;
            }
            
            //  If it just hit midnight then reset all the times.
            if (DateTime.Now.Hour == 23 && scanTimes.Count < ScheduledScanTimes.Count)
            {
                WriteToLog("SCAN MANAGER", "New day detected...resetting scans.");
                ResetScanTimes(true);
            }

            //  If we have no more scans today then exit.
            if (scanTimes.Count == 0 || DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                //WriteToLog("SCAN MANAGER", "No scheduled scan found.");
                return;
            }

            //  If we just hit a scheduled scan time, then remove the scan from today's scanTimes and run the scan.
            if (DateTime.Now.Hour == scanTimes.First().Key.Hour && DateTime.Now.Minute == scanTimes.First().Key.Minute)
            { 
                ScanType scanToRun = scanTimes.First().Value;
                WriteToLog("SCAN MANAGER", "Scheduled scan [" + GetScanTitleByType(scanToRun) + "] found...starting scan now.");

                scanTimes.Remove(scanTimes.First().Key);

                DisplayNextScheduledScan();                

                DisableEnableUI(false);
                Task.Run(() => PerformScan(scanToRun).GetAwaiter().GetResult());
                DisableEnableUI(true);
            }
            else
            {
                //WriteToLog("SCAN MANAGER", "No scheduled scan found.");
            }
        }

        private void DisplayNextScheduledScan()
        {
            if (scanTimes.Count < 1)
            {
                BalloonUpcomingScan = "No scheduled scans found.";
                txtNextScan.Text = BalloonUpcomingScan;
                return;
            }

            //  Get the next scheduled scan for today.
            KeyValuePair<DateTime, ScanType> nextTime = scanTimes.OrderBy(d => d.Key).SkipWhile(x => x.Key <= DateTime.Now).First();

            string nextScanText = GetScanTitleByType(nextTime.Value) + " @ " + nextTime.Key.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture);

            BalloonUpcomingScan = "Next Scan: " + nextScanText;
            txtNextScan.Text = nextScanText;
        }

        private void ChkAutoScan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoScan.Checked)
            {
                BalloonTitle = "iMICA - Auto Scanning Enabled.";
                ResetScanTimes();
                StartScanManagerTimer();
                DisplayNextScheduledScan();
            }
            else
            {
                //  If we're stopping the auto scanning, then remove all scheduled scans from the list and stop the timer.                
                scanTimes = new Dictionary<DateTime, ScanType>();
                StopScanManagerTimer();
                BalloonTitle = "iMICA - Auto Scanning Disabled.";
                txtNextScan.Text = "Auto scanning is disabled.";
                BalloonUpcomingScan = "Auto scanning is disabled.";
            }
        }

        #endregion

        private void HandleCommandLineAutomation(List<string> args)
        {
            DisableEnableUI(false);

            switch (args[2].ToLower())
            {
                case "filterupdate":
                    
                    break;
                case "priceupdatescan":
                    PerformScan(ScanType.ALL_STOCKS_BASE_INFO).GetAwaiter().GetResult();
                    break;
                case "fullfilterscan":
                    PerformScan(ScanType.FILTER_OPTION).GetAwaiter().GetResult();
                    break;
                case "fullstockinfoscan":
                    MessageBox.Show("This feature is not yet implemented.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                    break;
                default:
                    break;
            }

            DisableEnableUI(true);
        }

        private void DisableEnableUI(bool _enable)
        {
            btnPerformScan.Enabled = _enable;
            btnScanFiltersOptions.Enabled = _enable;
            btnLoadFilterScanResults.Enabled = _enable;
            chkExcludeAverageVolume.Enabled = _enable;
            chkExcludeIndustry.Enabled = _enable;
            chkExcludePrice.Enabled = _enable;
            chkExcludeSector.Enabled = _enable;
            chkScanExcludeFunds.Enabled = _enable;
            chkScanFloatUnder50m.Enabled = _enable;
            chkScanPriceUnder20.Enabled = _enable;
            chkScanVolOver400k.Enabled = _enable;
            rdoScanAllFilters.Enabled = _enable;            
            rdoScanBaseInfo.Enabled = _enable;
        }

        private void LoadDatabaseData()
        {            
            DisplayFiltersAndOptions();
        }

        private void BtnScanFiltersOptions_Click(object sender, EventArgs e)
        {
            WriteToLog("SCAN MANAGER", "Scraping FINVIZ for new filters.");
            DisableEnableUI(false);

            ScrapeFactory.GetAllFiltersAndOptions(out List<Filter> filters);
            DBFactory.UpdateFiltersInDatabase(filters);
            DisplayFiltersAndOptions();

            DisableEnableUI(true);
            WriteToLog("SCAN MANAGER", "Scrape completed.");
        }

        private void DisplayFiltersAndOptions()
        {
            List<Filter> filters = DBFactory.RetrieveFilterList();

            treeView1.Nodes.Clear();
            TotalFilters = 0;
            TotalOptions = 0;

            foreach (Filter fltr in filters)
            {
                //  If the filter has no options, move on to the next filter
                if (fltr.FilterOptions.Count == 0)
                    continue;

                TreeNode Parent = new TreeNode(fltr.Name + " (" + fltr.URL + ")");
                TotalFilters++;

                foreach (FilterOption option in fltr.FilterOptions)
                {
                    Parent.Nodes.Add(option.Name + " (" + option.DataURLFilter + ")");
                    TotalOptions++;
                }

                treeView1.Nodes.Add(Parent);
            }

            WriteToLog("SYSTEM", "Number of Filters Found: " + TotalFilters + ".");
            WriteToLog("SYSTEM", "Number of Filter-Options Found: " + TotalOptions + ".");

            lblTotalFilters.Text = "Total Filters : " + TotalFilters;
            lblTotalOptions.Text = "Total Options : " + TotalOptions;
        }

        private async void BtnPerformScan_Click(object sender, EventArgs e)
        {
            ScanType scanType = rdoScanAllFilters.Checked ? ScanType.FILTER_OPTION :
                                rdoScanBaseInfo.Checked ? ScanType.ALL_STOCKS_BASE_INFO :
                                                        ScanType.ALL_STOCKS_ALL_INFO;

            DisableEnableUI(false);
            await PerformScan(scanType);
            DisableEnableUI(true);
        }

        public async Task PerformScan(ScanFilterSettings.ScanType _scanType)
        {
            ScanInProgress = true;
            BalloonScanStatus = "A " + GetScanTitleByType(_scanType) + " scan is running.";

            //  Let the user know we started a scan.
            ShowBalloonToolTip();

            if (!StartedByCommandLine)
                StartScanTimer();

            ScanFilters scanFilters = ScanFilters.None;
            scanFilters = chkScanPriceUnder20.Checked ? (scanFilters | ScanFilters.PriceUnder20) : scanFilters;
            scanFilters = chkScanVolOver400k.Checked ? (scanFilters | ScanFilters.AvgVolumeOver400k) : scanFilters;
            scanFilters = chkScanFloatUnder50m.Checked ? (scanFilters | ScanFilters.FloatUnder50M) : scanFilters;
            scanFilters = chkScanExcludeFunds.Checked ? (scanFilters | ScanFilters.ExcludeFunds) : scanFilters;

            UpdateFilterSkipList();

            ScanFilterSettings scanSettings = new ScanFilterSettings(_scanType, scanFilters);

            //  Create a record of the scan for the DB.
            ScanRecord record = new ScanRecord(scanSettings);

            if (_scanType == ScanType.ALL_STOCKS_BASE_INFO)
                await PerformBaseScan(scanSettings);
            else if (_scanType == ScanType.FILTER_OPTION)
                await PerformFiltersScan(scanSettings);
            else if (_scanType == ScanType.ALL_STOCKS_ALL_INFO)
                await PerformFullScan(scanSettings);

            record.SetEndTime();
            DBFactory.WriteScanRecord(record);

            if (!StartedByCommandLine)
                EndScanTimer();

            ScanInProgress = false;
            BalloonScanStatus = "No scan is currently running.";

            //  If this program was started by a command line command (ie. Windows Task), then close the application after the scan if finished.
            if (StartedByCommandLine)
                Application.Exit();
        }

        public async Task PerformBaseScan(ScanFilterSettings scanSettings)
        {
            List<SmallScanResult> scanList = new List<SmallScanResult>();
            scanList = await Task.Run(() => ScrapeFactory.PerformScanAsync(scanSettings));

            DBFactory.WriteSmallScanResult(scanList);
        }

        private async Task PerformFiltersScan(ScanFilterSettings scanSettings)
        {
            List<FilterOptionScanResult> scanResults = new List<FilterOptionScanResult>();

            int curFilter = 0;
            int maxFilters = DBFactory.NumFilterOptions;

            int testModeIterator = 0;

            foreach (Filter filter in DBFactory.Filters)
            {                
                //  If we skip this filter, mark these as complete and move to the next filter.
                if (FiltersToSkip.Contains(filter.URL))
                {
                    curFilter += filter.FilterOptions.Count();
                    continue;
                }

                //  Reset the scanResults to populate with the next filter's items.
                scanResults = new List<FilterOptionScanResult>();
                    
                //  Scrape the results for each option in this current Filter.
                foreach (FilterOption option in filter.FilterOptions)
                {
                    UpdateFilterProgress(++curFilter, maxFilters);

                    FilterOptionScanResult tmpRes = new FilterOptionScanResult(option);

                    scanSettings.RemoveAllAdditionalFilters();
                    scanSettings.AddAdditionalFilter(option.URL);

                    tmpRes.Stocks = await Task.Run(() => ScrapeFactory.PerformScanAsync(scanSettings));

                    scanResults.Add(tmpRes);
                }

                //  Write the filter's results to the DB.
                DBFactory.WriteFilterOptionScanResult(scanResults);

                if (chkTestMode.Checked && ++testModeIterator >= TestingFilterOptionScanMax)
                    break;
            }

            UpdateFilterProgress(0, 0);
        }

        private async Task PerformFullScan(ScanFilterSettings scanSettings)
        {
            List<StockScanResult> scanList = new List<StockScanResult>();
            scanList = await Task.Run(() => ScrapeFactory.PerformScanAsync(scanSettings));

            DBFactory.WriteStockScanResult(scanList);
        }

        private void BtnLoadFilterScanResults_Click(object sender, EventArgs e)
        {
            List<FilterOptionScanResult> filterOptionResults = DBFactory.RetrieveFilterOptionScanResults();
            List<ScanRecord> scanRecords = DBFactory.RetrieveScanRecords();

            lblTotalFilterResults.Text = "Total Filter Scan Records: " + filterOptionResults.Count;
            lblLastRecordDate.Text = "Last Record Date: " + scanRecords.Last().StartTime.ToString();

            //DBFactory.RetrieveSmallScanResults(out List<SmallScanResult> smallScanResults);
        }

        private void UpdateFilterSkipList()
        {
            FiltersToSkip = new List<string>();

            if (chkScanPriceUnder20.Checked)
                FiltersToSkip.Add("sh_price");

            if (chkScanVolOver400k.Checked)
                FiltersToSkip.Add("sh_avgvol");

            if (chkScanFloatUnder50m.Checked)
                FiltersToSkip.Add("sh_float");

            if (chkScanExcludeFunds.Checked)
                FiltersToSkip.Add("ind");
        }

        #region PROGRESS UI FEEDBACK

        private void ScrapeFactory_ScannerPageUpdate(object sender, ScannerPageUpdateEventArgs e)
        {
            //if (InvokeRequired)
            //{
            //    Invoke((EventHandler<ScannerPageUpdateEventArgs>)ScrapeFactory_ScannerPageUpdate, sender, e);
            //    return;
            //}

            if (lblPageUpdate.InvokeRequired)
            {
                lblPageUpdate.BeginInvoke(new MethodInvoker(delegate
                {
                    lblPageUpdate.Text = e.TotalPages == 0 ? "Page Scan Progress" : "Scanning " + (e.FinishedPage).ToString() + " / " + e.TotalPages.ToString() + " pages";
                }));

                //lblPageUpdate.Invalidate();
            }
            else
            {
                lblPageUpdate.Text = e.TotalPages == 0 ? "Page Scan Progress" : "Scanning " + (e.FinishedPage).ToString() + " / " + e.TotalPages.ToString() + " pages";
            }

            if (prgPageUpdate.InvokeRequired)
            {
                prgPageUpdate.BeginInvoke(new MethodInvoker(delegate
                {
                    prgPageUpdate.Maximum = e.TotalPages;
                    prgPageUpdate.Value = e.FinishedPage;
                }));
            }
            else
            {
                prgPageUpdate.Maximum = e.TotalPages;
                prgPageUpdate.Value = e.FinishedPage;
            }

            Invalidate();
        }

        private void UpdateFilterProgress(int curFilter, int maxFilters)
        {
            if (lblFilterUpdate.InvokeRequired)
            {
                lblFilterUpdate.BeginInvoke(new MethodInvoker(delegate
                {
                    lblFilterUpdate.Text = curFilter == 0 ? "Filter Scan Progress" : "Scanning " + curFilter.ToString() + " / " + maxFilters.ToString() + " filters";
                }));
            }
            else
            {
                lblFilterUpdate.Text = curFilter == 0 ? "Filter Scan Progress" : "Scanning " + curFilter.ToString() + " / " + maxFilters.ToString() + " filters";
            }

            if (prgFilterUpdate.InvokeRequired)
            {
                prgFilterUpdate.BeginInvoke(new MethodInvoker(delegate
                {
                    prgFilterUpdate.Maximum = maxFilters;
                    prgFilterUpdate.Value = curFilter;
                }));
            }
            else
            {
                prgFilterUpdate.Maximum = maxFilters;
                prgFilterUpdate.Value = curFilter;
            }
        }

        private void StartScanTimer()
        {
            ScanTimerStartTime = DateTime.Now;

            ScanTimer = new Timer
            {
                Interval = 1000
            };
            ScanTimer.Tick -= ScanTimer_Tick;
            ScanTimer.Tick += ScanTimer_Tick;
            ScanTimer.Start();
        }

        private void EndScanTimer()
        {
            ScanTimer.Stop();
            ScanTimer.Tick -= ScanTimer_Tick;
        }

        private void ScanTimer_Tick(object sender, System.EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - ScanTimerStartTime;

            txtScanElapsedTime.Text = elapsed.ToString(@"hh\:mm\:ss");
        }

        #endregion

        #region TRAY ICON

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                trayIcon.Visible = true;
                ShowBalloonToolTip();
            }
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowBalloonToolTip();
            }
            else if (e.Button == MouseButtons.Right)
            {
                



                //this.ShowInTaskbar = false;
                //this.WindowState = FormWindowState.Normal;
                //Show();                
            }
        }

        private void ShowBalloonToolTip()
        {
            trayIcon.ShowBalloonTip(5000, BalloonTitle, BalloonScanStatus + "\n" + BalloonUpcomingScan, ToolTipIcon.Info);
        }

        private void InitializeTrayIconContextMenu()
        {
            //MenuItem[] menuList = new MenuItem[]{new MenuItem("Maximize"), new MenuItem("Close"), new MenuItem("Open")};

            m_TrayIconMenu = new ContextMenu();

            MenuItem maximizeMenuItem = new MenuItem("Open iMICA", TrayIconMaximize_Click);
            m_TrayIconMenu.MenuItems.Add(maximizeMenuItem);

            MenuItem closeMenuItem = new MenuItem("Exit iMICA", TrayIconCloseApplication_Click);
            m_TrayIconMenu.MenuItems.Add(closeMenuItem);

            MenuItem autoScanningToggleMenuItem = new MenuItem("Use Auto Scanning", TrayIconDisableAutoScanning_Click)
            {
                Checked = chkAutoScan.Checked ? true : false
            };
            m_TrayIconMenu.MenuItems.Add(autoScanningToggleMenuItem);

            trayIcon.ContextMenu = m_TrayIconMenu;
            trayIcon.ContextMenu.Popup += ContextMenu_Popup;
        }

        private void ContextMenu_Popup(object sender, EventArgs e)
        {
            trayIcon.ContextMenu.MenuItems[0].Enabled = this.WindowState == FormWindowState.Normal ? false : true;
            Console.WriteLine("WINDOW STATE: " + this.WindowState.ToString());

            int scanToggleItem = trayIcon.ContextMenu.MenuItems.Count - 1;
            trayIcon.ContextMenu.MenuItems[scanToggleItem].Checked = chkAutoScan.Checked ? true : false;

            //System.Drawing.Size windowSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
            //System.Drawing.Point menuPoint = new System.Drawing.Point(windowSize.Width - 180, windowSize.Height - 5);
            //menuPoint = this.PointToClient(menuPoint);
            //trayIcon.ContextMenu.Show(this, menuPoint);
        }

        void TrayIconMaximize_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;

            trayIcon.Visible = false;
            this.ShowInTaskbar = false;            
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;           
            this.Activate();
            Show();

        }

        void TrayIconCloseApplication_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            string message = ScanInProgress ? "A scan is currenctly running! \n\n Are you sure you want to close the program?" : "Are you sure you want to close the program?";

            if (MessageBox.Show(message, "Are You Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                Application.Exit();
        }

        void TrayIconDisableAutoScanning_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;

            if (menuItem.Checked)
            {
                menuItem.Checked = false;
                chkAutoScan.Checked = false;
                ShowBalloonToolTip();
            }                
            else
            {
                menuItem.Checked = true;
                chkAutoScan.Checked = true;
                ShowBalloonToolTip();
            }
                
        }

        #endregion

        private String GetScanTitleByType(ScanType _scanType)
        {
            return _scanType == ScanType.ALL_STOCKS_BASE_INFO ? "Basic Info" :
                        _scanType == ScanType.FILTER_OPTION ? "Full Filters" :
                            _scanType == ScanType.ALL_STOCKS_ALL_INFO ? "Full Info" : "[UNKNOWN]";            
        }

        private void WriteToLog(string _sender, string _line, bool _omitTimeStamp = false)
        {
            string dateStamp = !_omitTimeStamp ? "[" + DateTime.Now.ToLongTimeString() + "] " : "";
            dateStamp = dateStamp + "<" + _sender + "> ";

            rtbLog.AppendText(dateStamp + _line + "\n");
            //rtbLog.Text = dateStamp + _line + "\n" + rtbLog.Text;

            ScrollToBottom(rtbLog);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        private const int WM_VSCROLL = 277;
        private const int SB_PAGEBOTTOM = 7;

        internal static void ScrollToBottom(System.Windows.Forms.RichTextBox richTextBox)
        {
            SendMessage(richTextBox.Handle, WM_VSCROLL, (IntPtr)SB_PAGEBOTTOM, IntPtr.Zero);
            richTextBox.SelectionStart = richTextBox.Text.Length;
        }
    }
}