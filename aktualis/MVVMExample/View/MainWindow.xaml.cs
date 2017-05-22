using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;
using Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Common;
using System.IO;
using System.Drawing;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(false);
            this.Closed += this.MainWindow_Closed;

            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);

            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(ComboBox.TextProperty, typeof(ComboBox));

            dpd.AddValueChanged(combobox, OnComboBoxTextChanged);

            SerchButton.Focus();         // Set Logical Focus
            Keyboard.Focus(SerchButton);
        }

        private void MainWindow_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }


        /// <summary>
        /// FIRST TAB ITEM 
        /// </summary>

        WebCam webcam;
        public List<Ugyfelek> ugyfelek;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Ugyfelek> list = new List<Ugyfelek>();

        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Fill items when combobox selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnComboBoxTextChanged(object sender, EventArgs args)
        {
            MainWindowViewModel mvvm = new MainWindowViewModel();

            ugyfelek = mvvm.Ugyfelek;

            foreach (Ugyfelek ugyfel in ugyfelek)
            {
                if (ugyfel.Kep != null)
                {
                    if (ugyfel.Nev == combobox.Text)
                    {
                        NameTextBox.Text = ugyfel.Nev;
                        EmailTextBox.Text = ugyfel.E_mail;
                        DateTextBox.Text = ugyfel.Szuletesi_Datum.ToString();

                        this.imgVideo.Source = WebCamHelper.byteArrayToImage(ugyfel.Kep);
                    }
                }
                else
                {
                    if (ugyfel.Nev == combobox.Text)
                    {
                        NameTextBox.Text = ugyfel.Nev;
                        EmailTextBox.Text = ugyfel.E_mail;
                        DateTextBox.Text = ugyfel.Szuletesi_Datum.ToString();

                        this.imgVideo.Source = null;
                    }
                }

            }
        }

        /// <summary>
        /// Clear all items from window on TAB ITEM 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            combobox.Text = "";
            NameTextBox.Text = "";
            EmailTextBox.Text = "";
            DateTextBox.Text = "";
            imgVideo.Source = null;
        }

        /// <summary>
        /// Serch for Name or Email or BirthDay in Ugyfelek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerchButton_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            MainWindowViewModel mvvm = new MainWindowViewModel();

            ugyfelek = mvvm.Ugyfelek;

            if (SerchTextBox.Text == "")
            {
                counter++;
            }
            else
            {
                foreach (Ugyfelek ugyfel in ugyfelek)
                {
                    if ((ugyfel.Nev == SerchTextBox.Text) || (ugyfel.E_mail == SerchTextBox.Text) || (ugyfel.Szuletesi_Datum.ToString() == SerchTextBox.Text))
                    {
                        NameTextBox.Text = ugyfel.Nev;
                        EmailTextBox.Text = ugyfel.E_mail;
                        DateTextBox.Text = ugyfel.Szuletesi_Datum.ToString();
                        SerchTextBox.Text = "";
                        combobox.Text = ugyfel.Nev;
                        counter++;
                    }
                }
            }

            if (counter == 0)
            {
                MessageBox.Show(Application.Current.MainWindow, "Ügyfél nem talalható.", "Sikertelen keresés!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel);
                SerchTextBox.Text = "";
            }
        }

        /// <summary>
        /// Start the webcam
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CAMButton_Click(object sender, RoutedEventArgs e)
        {
            webcam.Start();
        }

        /// <summary>
        /// Capture picture and stop the webcam
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhotoButton_Click(object sender, RoutedEventArgs e)
        {
            imgVideo.Source = imgVideo.Source;

            webcam.Stop();
        }

        /// <summary>
        /// Save to mycomputer the captured picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.imgVideo.Source = null;
        }

        /// <summary>
        /// Adding data to DbContext from TAB ITEM 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {

            list.Clear();

            if (imgVideo.Source != null)
            {
                if ((NameTextBox.Text != "") && (EmailTextBox.Text != "") && (DateTextBox.Text != ""))
                {
                    var ujUgyfel = new Ugyfelek();

                    MainWindowViewModel mvvm = new MainWindowViewModel();

                    bool exisits = false;
                    foreach (Ugyfelek item in mvvm.Ugyfelek)
                    {
                        if ((item.Nev == NameTextBox.Text) && (item.E_mail == EmailTextBox.Text) && (item.Szuletesi_Datum == Convert.ToDateTime(DateTextBox.Text)))
                        {
                            exisits = true;
                        }
                    }

                    if (exisits)
                    {
                        MessageBox.Show(Application.Current.MainWindow, "Az ügyfél már létezik ...", "Sikertelen hozzáadás!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel);
                        return;
                    }
                    else
                    {
                        ujUgyfel.Nev = NameTextBox.Text;
                        ujUgyfel.E_mail = EmailTextBox.Text;
                        ujUgyfel.Szuletesi_Datum = Convert.ToDateTime(DateTextBox.Text);

                        ujUgyfel.Kep = WebCamHelper.imageToByteArray((BitmapSource)imgVideo.Source);

                        //create DBContext object
                        using (var dbCtx = new MyDBContext())
                        {
                            //Add Student object into Students DBset
                            dbCtx.Ugyfelek.Add(ujUgyfel);

                            // call SaveChanges method to save student into database
                            dbCtx.SaveChanges();
                        }

                        MessageBox.Show("Sikeres hozzáadás!");

                        MainWindowViewModel mvvm1 = new MainWindowViewModel();

                        foreach (Ugyfelek item in mvvm1.Ugyfelek)
                        {
                            list.Add(item);
                        }

                        combobox.ItemsSource = null;
                        combobox.Items.Clear();
                        combobox.ItemsSource = list;

                        NameTextBox.Text = "";
                        EmailTextBox.Text = "";
                        DateTextBox.Text = "";
                        imgVideo.Source = null;

                    }
                }
                else
                {
                    MessageBox.Show(Application.Current.MainWindow, "Kötelező mezök: Teljes név, Email cím , Születési dátum ...", "Sikertelen hozzáadás!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel);
                }
            }
            else
            {
                if ((NameTextBox.Text != "") && (EmailTextBox.Text != "") && (DateTextBox.Text != ""))
                {
                    var ujUgyfel = new Ugyfelek();

                    ujUgyfel.Nev = NameTextBox.Text;
                    ujUgyfel.E_mail = EmailTextBox.Text;
                    ujUgyfel.Szuletesi_Datum = Convert.ToDateTime(DateTextBox.Text);

                    //create DBContext object
                    using (var dbCtx = new MyDBContext())
                    {
                        //Add Student object into Students DBset
                        dbCtx.Ugyfelek.Add(ujUgyfel);

                        // call SaveChanges method to save student into database
                        dbCtx.SaveChanges();
                    }

                    MessageBox.Show("Sikeres hozzáadás!");

                    MainWindowViewModel mvvm1 = new MainWindowViewModel();

                    foreach (Ugyfelek item in mvvm1.Ugyfelek)
                    {
                        list.Add(item);
                    }

                    combobox.ItemsSource = null;
                    combobox.Items.Clear();
                    combobox.ItemsSource = list;

                    NameTextBox.Text = "";
                    EmailTextBox.Text = "";
                    DateTextBox.Text = "";

                }
                else
                {
                    MessageBox.Show(Application.Current.MainWindow, "Kötelező mezök: Teljes név, Email cím , Születési dátum ...", "Sikertelen hozzáadás!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel);
                }
            }
        }

        /// <summary>
        /// Ubdate DbContext from TAB ITEM 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

            string combotext = combobox.Text;

            if (combotext != "")
            {

                Ugyfelek ugyfelek;

                list.Clear();

                //1. Get student from DB
                using (var ctx = new MyDBContext())
                {
                    ugyfelek = ctx.Ugyfelek.Where(s => s.Nev == combotext).FirstOrDefault<Ugyfelek>();
                }

                //2. change student name in disconnected mode (out of ctx scope)
                if (ugyfelek != null)
                {
                    if (imgVideo.Source != null)
                    {
                        ugyfelek.Nev = NameTextBox.Text;
                        ugyfelek.E_mail = EmailTextBox.Text;
                        ugyfelek.Szuletesi_Datum = Convert.ToDateTime(DateTextBox.Text);
                        ugyfelek.Kep = WebCamHelper.imageToByteArray((BitmapSource)imgVideo.Source);
                    }
                    else
                    {
                        ugyfelek.Nev = NameTextBox.Text;
                        ugyfelek.E_mail = EmailTextBox.Text;
                        ugyfelek.Szuletesi_Datum = Convert.ToDateTime(DateTextBox.Text);
                        ugyfelek.Kep = null;
                    }
                }

                //save modified entity using new Context
                using (var dbCtx = new MyDBContext())
                {
                    //3. Mark entity as modified
                    dbCtx.Entry(ugyfelek).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }

                MessageBox.Show("Sikeres módosítás!");

                MainWindowViewModel mvvm1 = new MainWindowViewModel();

                foreach (Ugyfelek item in mvvm1.Ugyfelek)
                {
                    list.Add(item);
                }

                combobox.ItemsSource = null;
                combobox.Items.Clear();
                combobox.ItemsSource = list;

                NameTextBox.Text = "";
                EmailTextBox.Text = "";
                DateTextBox.Text = "";
                imgVideo.Source = null;

            }
            else
            {
                MessageBox.Show(Application.Current.MainWindow, "Valassza ki az ügyfelet.", "Sikertelen módosítás!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel);
            }
        }

    }
}
