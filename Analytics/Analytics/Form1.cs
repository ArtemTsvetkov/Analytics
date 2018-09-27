using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.ExceptionHandler;
using Analytics.CommonComponents.ExceptionHandler.Concrete;
using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using Analytics.CommonComponents.ExceptionHandler.View;
using Analytics.CommonComponents.Exceptions;
using Analytics.CommonComponents.Exceptions.Security;
using Analytics.CommonComponents.Views;
using Analytics.HandModifiedDataPanel;
using Analytics.HandModifiedDataPanel.Interfaces;
using Analytics.HandModifiedDataPanel.ModelConfigurator;
using Analytics.MenuComponent;
using Analytics.Modeling.GroupByTypes;
using Analytics.Navigator;
using Analytics.Navigator.Basic;
using Analytics.SecurityComponent;
using Analytics.SecurityComponent.Interfaces;
using Analytics.SecurityComponent.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Analytics
{
    public partial class Form1 : Form
    {
        private MarcovitsView marcovitsView;
        private ModelingView modelingView;
        private SecurityControllerInterface securityController;
        private HandModifiedDataControllerInterface handModifiedDataController;

        public Form1()
        {
            try
            {
                InitializeComponent();
                //
                //Marcovits and modeling components
                //
                ConcreteExceptionHandlerInitializer.initThisExceptionHandler(
                    ExceptionHandler.getInstance());
                marcovitsView = new MarcovitsView(this);
                modelingView = new ModelingView(this);
                Navigator.Navigator.getInstance().addView(marcovitsView);
                Navigator.Navigator.getInstance().addView(modelingView);
                //
                //Autorization component
                //
                SecurityModel securityModel = new SecurityModel();
                AutorizationSecurityView securityView = 
                    new AutorizationSecurityView(this, securityModel);
                securityController = new SecurityController(securityModel);
                Navigator.Navigator.getInstance().addView(securityView);
                //
                //Settings elements on forms
                //
                comboBox1.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
                //
                //Hand modified data component
                //
                HandModifiedDataModel handModifiedDataModel = new HandModifiedDataModel();
                HandModifiedDataView handModifiedDataView =
                    new HandModifiedDataView(this, handModifiedDataModel);
                handModifiedDataController = new HandModifiedDataController(handModifiedDataModel,
                    securityModel);
                Navigator.Navigator.getInstance().addView(handModifiedDataView);
                //
                //Menu
                //
                Navigator.Navigator.getInstance().addView(new MenuView(this));
                //
                //Menu
                //
                Navigator.Navigator.getInstance().addView(new AddUserView(this));
                //
                //Menu
                //
                Navigator.Navigator.getInstance().addView(new ChangePasswordView(this));
                //
                //Navigator
                //
                Navigator.Navigator.getInstance().navigateTo("AutorizationSecurityView");
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        public void setUserName(string userName)
        {
            if(userName.Length>5)
            {
                userName = userName.Substring(0,4);
                userName += "...";
            }
            label37.Text = userName;
            label52.Text = userName;
            label22.Text = userName;
            label23.Text = userName;
            label24.Text = userName;
            label25.Text = userName;
        }

        public Chart chart1Elem
        {
            get { return chart1; }
        }

        public Chart chart2Elem
        {
            get { return chart2; }
        }

        public Label label5Elem
        {
            get { return label5; }
        }

        public Label label12Elem
        {
            get { return label12; }
        }

        public Label label6Elem
        {
            get { return label6; }
        }

        public DataGridView dataGridView2Elem
        {
            get { return dataGridView2; }
        }

        public NumericUpDown numericUpDown1Elem
        {
            get { return numericUpDown1; }
        }

        public ProgressBar progressBar1Elem
        {
            get { return progressBar1; }
        }

        public TabControl tabControl1Elem
        {
            get { return tabControl1; }
        }

        public TextBox textBox2Elem
        {
            get { return textBox2; }
        }

        public TextBox textBox3Elem
        {
            get { return textBox3; }
        }

        public TabControl tabControl2Elem
        {
            get { return tabControl2; }
        }

        public ComboBox comboBox1Elem
        {
            get { return comboBox1; }
        }

        public ComboBox comboBox3Elem
        {
            get { return comboBox3; }
        }

        public Button button23Elem
        {
            get { return button23; }
        }

        public CheckBox checkBox1Elem
        {
            get { return checkBox1; }
        }

        public DataGridView DataGridView4Elem
        {
            get { return dataGridView4; }
        }

        public DataGridView DataGridView6Elem
        {
            get { return dataGridView6; }
        }

        public Label label7Elem
        {
            get { return label7; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            marcovitsView.button2_Click();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modelingView.button1_Click();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        if (modelingView != null)
                        {
                            modelingView.intervalChange(BasicType.year);
                        }
                        break;
                    case 1:
                        if (modelingView != null)
                        {
                            modelingView.intervalChange(BasicType.month);
                        }
                        break;
                    case 2:
                        if (modelingView != null)
                        {
                            modelingView.intervalChange(BasicType.day);
                        }
                        break;
                    case 3:
                        if (modelingView != null)
                        {
                            modelingView.intervalChange(BasicType.hour);
                        }
                        break;
                    case 4:
                        if (modelingView != null)
                        {
                            modelingView.intervalChange(BasicType.minute);
                        }
                        break;
                    case 5:
                        if (modelingView != null)
                        {
                            modelingView.intervalChange(BasicType.second);
                        }
                        break;
                    default:
                        throw new UnknownTimeIntervalType("Unknown time interval type");
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            modelingView.getPreviousState();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox3.SelectedIndex)
                {
                    case 0:
                        if (marcovitsView != null)
                        {
                            marcovitsView.intervalChange(BasicType.year);
                        }
                        break;
                    case 1:
                        if (marcovitsView != null)
                        {
                            marcovitsView.intervalChange(BasicType.month);
                        }
                        break;
                    case 2:
                        if (marcovitsView != null)
                        {
                            marcovitsView.intervalChange(BasicType.day);
                        }
                        break;
                    case 3:
                        if (marcovitsView != null)
                        {
                            marcovitsView.intervalChange(BasicType.hour);
                        }
                        break;
                    case 4:
                        if (marcovitsView != null)
                        {
                            marcovitsView.intervalChange(BasicType.minute);
                        }
                        break;
                    case 5:
                        if (marcovitsView != null)
                        {
                            marcovitsView.intervalChange(BasicType.second);
                        }
                        break;
                    default:
                        throw new UnknownTimeIntervalType("Unknown time interval type");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            marcovitsView.getPreviousState();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            modelingView.flagUseCovarChange(checkBox1.Checked);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                securityController.signIn();
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Equals(textBox6.Text) & textBox5.Text!="" & textBox6.Text!="")
            {
                try
                {
                    if (checkBox2.Checked)
                    {
                        securityController.addNewUser(textBox5.Text, textBox6.Text, true);
                    }
                    else
                    {
                        securityController.addNewUser(textBox5.Text, textBox6.Text, false);
                    }
                }
                catch(Exception ex)
                {
                    ExceptionHandler.getInstance().processing(ex);
                }
            }
            else
            {
                try
                {
                    throw new BadCheckedPasswords();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.getInstance().processing(ex);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            navigatoToMenuView();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    /*if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.year);
                    }*/
                    break;
                case 1:
                    /*if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.day);
                    }*/
                    tabControl1.SelectTab(2);
                    break;
                default:
                    //ДОБАВИТЬ СЮДА ИСКЛЮЧЕНИЕ - НЕИЗВЕСТНЫЙ ТИП
                    throw new Exception();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    /*if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.year);
                    }*/
                    break;
                case 1:
                    /*if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.day);
                    }*/
                    tabControl1.SelectTab(2);
                    break;
                default:
                    //ДОБАВИТЬ СЮДА ИСКЛЮЧЕНИЕ - НЕИЗВЕСТНЫЙ ТИП
                    throw new Exception();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            modelingView.numberOfModelingStartsChange(
                int.Parse(numericUpDown1.Value.ToString()));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            marcovitsView.getNextState();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            modelingView.getNextState();
        }
        
        private void button12_Click(object sender, EventArgs e)
        {
            if(textBox8.Text.Equals(textBox1.Text) & 
                !textBox8.Text.Equals("") & !textBox9.Equals(""))
            securityController.changeUserPassword(textBox9.Text, textBox8.Text);
            else
            {
                try
                {
                    throw new BadCheckedPasswords();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.getInstance().processing(ex);
                }
            }
        }
        
        private void textBox2_Leave(object sender, EventArgs e)
        {
            securityController.setConfig(textBox2.Text, textBox3.Text);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            securityController.setConfig(textBox2.Text, textBox3.Text);
        }

        private void button23_Click_1(object sender, EventArgs e)
        {
            try
            {
                Navigator.Navigator.getInstance().navigateTo("AddUserView");
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                Navigator.Navigator.getInstance().navigateTo("ChangePasswordView");
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            try
            {
                Navigator.Navigator.getInstance().navigateTo("ModelingView");
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                Navigator.Navigator.getInstance().navigateTo("AutorizationSecurityView");
                securityController.signOut();
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            try
            {
                Navigator.Navigator.getInstance().navigateToPreviousView();
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            navigatoToMenuView();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            navigatoToMenuView();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            navigatoToMenuView();
        }

        private void navigatoToMenuView()
        {
            try
            {
                Navigator.Navigator.getInstance().navigateTo("MenuView");
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            navigatoToMenuView();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            ModelConfiguratorInterface<HandModifiedDataState> configurator =
                new UpdateTableItem();
            handModifiedDataController.updateModelsConfig(configurator);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            ModelConfiguratorInterface<HandModifiedDataState> configurator =
               new UpdateNumberOfLicensesWithModificator();
            handModifiedDataController.updateModelsConfig(configurator);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                handModifiedDataController.saveNewData();
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            handModifiedDataController.getPreviousState();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            handModifiedDataController.getNextState();
        }
    }
}
