﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using B4B.Phone.Resources;
using B4B_Lab1.ViewModels;
using B4B_Lab1;
using System.IO;
using System.IO.IsolatedStorage;
namespace B4B.Phone.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        int count = 0;
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }
        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }
        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }
        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }
        public void Save(String courseInfo)
        {
            using (var IS = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var Stream = IS.OpenFile("data.txt", FileMode.Create))
                {
                    using (var SW = new StreamWriter(Stream))
                    {
                        //foreach (var Item in Items)
                        //{
                            SW.WriteLine(count + "," + courseInfo);
                            count++;    
                                /*string.Format("{0},{1},{2},{3}", Item.CourseName,
                                Item.Time, Item.Assignments, Item.Grades));*/
                        //}
                    }
                }
            }
        }
        private void Load()
        {
            Items.Clear();
            int readLines = 0;
            using (var IS = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (IS.FileExists("data.txt"))
                    using (var Stream = IS.OpenFile("data.txt", FileMode.Open))
                    {
                        using (var SR = new StreamReader(Stream))
                        {
                            while (!SR.EndOfStream)
                            {
                                var Line = SR.ReadLine();
                                var Fields = Line.Split(new char[] { ',' });
                                var Item = new ItemViewModel()
                                {
                                    ID = Fields[0],
                                    CourseName = Fields[1],
                                    Time = Fields[2],
                                    Assignments = Fields[3],
                                    Grades = Fields[4]
                                };
                                Items.Add(Item);
                            }
                        }
                    }
            }
        }
        public bool IsDataLoaded
        {
            get;
            private set;
        }
        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            Load();
            // Sample data; replace with real data
            // Items = ItemService.GetItems();
            //NotifyPropertyChanged("Items");
            //this.IsDataLoaded = true;
            //Save();
            return;
        }
        /*//Call Save/Load from UI, e.g. in button click event handler…
 private void Button_Click(object sender, RoutedEventArgs e)
{
            App.ViewModel.Save();
}
        */
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}