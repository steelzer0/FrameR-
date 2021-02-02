using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Binaries
{
    public class ProcessesClass
    {
        //Fields
        private Process[] _procList;
        private Process blankProcess;
        private string[] _SortingArray;
        private ListBox _processListBox;
        private ProcessModuleCollection _modules;
        public void Init(ListBox processListBox)
        {
            _processListBox = processListBox;

            //Re-draw listbox
            Update();

            //Add process names in listbox
            for (int i = 0; i < _procList.Length; i++)
            {
                //We don't need svchost and conhost
                if (_SortingArray[i] != null && _SortingArray[i] != "svchost" && _SortingArray[i] != "conhost")
                {
                    _processListBox.Items.Add(_SortingArray[i]);
                }
            }

        }
        //Update method
        public void Update()
        {
            //Getting all processes that runned
            _procList = Process.GetProcesses();

            //Sorting processes by alphabet
            _SortingArray = new string[_procList.Length];

            for (int s = 0; s < _procList.Length; s++)
            {
                _SortingArray[s] = _procList[s].ProcessName;
            }

            //Sort function
            Array.Sort(_SortingArray);
        }

        public void ProcessKill(string eliminate)
        {
            //Seek and equate process into variable
            var killed = from t in _procList
                         where t.ProcessName.ToLower().Equals(eliminate.ToLower())
                         select t;
            //Kill all threads of process
            foreach (Process p in killed)
            {
                _processListBox.Items.Remove(p.ProcessName);
                try
                {
                    p.Kill();
                }catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            //Update call
            Update();
        }
        //Receiving information about process
        public string GetInfo(string getAbout)
        {
            //Exception bypass
            string Info = "";
            //Getting blank to overwrite it by found process
            for(int i = 0; i < _procList.Length; i++)
            {
                if(getAbout == _procList[i].ProcessName)
                {
                    blankProcess = _procList[i];
                }
            }
            //Trying to get modules of process
            try
            {
                _modules = blankProcess.Modules;
            }
            catch(Exception e)
            {
                Info = e.Message;
                return Info;
            }
            //Receiving module info
            foreach(ProcessModule module in _modules)
            {
                Info = "Process Name: " + blankProcess.ProcessName + "\n\n" + "Process Id: " + blankProcess.Id
                    + "\n\n" + "File Name: " + module.FileName + "\n\n" + "Module Name: " + module.ModuleName + "\n\n"
                    + module.FileVersionInfo;
            }

            return Info;
        }
    }
}
