using System;
using System.Management;
using System.Windows.Controls;

namespace Binaries
{
    public class HardwareClass
    {
        #region Fields

        string[] Names;
        ManagementObjectSearcher objvide;
        TreeViewItem[] treeViewItems;

        #endregion

        /// <summary>
        /// Fill treeview with components
        /// </summary>
        /// <param name="trw"></param>
        public void FillIn(TreeView trw) 
        {
            Names = FillComponents();
            objvide = new ManagementObjectSearcher();

            #region TreeViewItems fill
            treeViewItems = new TreeViewItem[Names.Length];

            for (int i = 0; i < treeViewItems.Length; i++) //Fill main components
            {
                treeViewItems[i] = new TreeViewItem();
                treeViewItems[i].Header = Names[i];
            }

            for (int i = 0; i < treeViewItems.Length; i++) //Filling treeview item with sub-components
            {
                objvide = null;
                try
                {
                    objvide = new ManagementObjectSearcher("select * from Win32_" + treeViewItems[i].Header);
                }
                catch (Exception e) { }

                var collection = objvide.Get();
                int internalit = 0;

                foreach (ManagementObject obj in collection) 
                {
                    try
                    {
                        treeViewItems[i].Items.Add(new TreeViewItem());
                        (treeViewItems[i].Items[internalit] as TreeViewItem).Header = obj["Name"]; 
                        internalit++;
                    }
                    catch (Exception e) { }
                }
            }

            for (int i = 0; i < treeViewItems.Length; i++) //Add filled items
            {
                trw.Items.Add(treeViewItems[i]);
            }

            #endregion TreeViewItems fill
        }

        /// <summary>
        /// Get component information
        /// </summary>
        /// <param name="display">Where to display</param>
        /// <param name="thisComponent">Chosen component</param>
        public void ComponentInfo(TextBox display, string thisComponent)
        {
            //my girlfriend said that I have to comment it
            //this code is completely awful, never do it like that, I was drunk, or maybe I was not
            Names = FillComponents();
            display.Text = String.Empty; //empty container before putting text

            //Cycle all components and their names, if chosen component.Name == Name from original list then put every property in display box
            for(int i = 0; i < Names.Length; i++) 
            {
                objvide = null;
                try
                {
                    objvide = new ManagementObjectSearcher("select * from Win32_" + Names[i]);
                }
                catch(Exception e) { }
                
                var collection = objvide.Get();

                foreach (ManagementObject obj in collection)
                {
                    try
                    {
                        if(thisComponent == obj["Name"].ToString())
                        {
                            foreach(var prop in obj.Properties)
                            {
                                display.Text += prop.Name + ": ";
                                if (obj[prop.Name] != null)
                                {
                                    display.Text += obj[prop.Name] + "\n\n";
                                }
                                else
                                {
                                    display.Text += "0" + "\n\n";
                                }
                            }

                            return;
                        }
                    }
                    catch (Exception e) { }
                }
            }
        }

        /// <summary>
        /// Returns array which contains components of PC
        /// </summary>
        /// <returns></returns>
        public string[] FillComponents()
        {
            return new string[] {
              "BaseBoard",
               "Battery",
               "BIOS",
               "Bus",
               "CacheMemory",
               "DesktopMonitor",
               "DiskDrive",
               "DiskPartition",
               "Keyboard",
               "LogicalDisk",
               "MemoryDevice",
               "MotherboardDevice",
               "NetworkAdapter",
               "NetworkProtocol",
               "PhysicalMemory",
               "Processor",
               "SoundDevice",
               "USBController",
               "VideoController",
            };
        }
    }
}
