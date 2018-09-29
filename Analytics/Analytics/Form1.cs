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
using Analytics.MarcovitsComponent;
using Analytics.MenuComponent;
using Analytics.Modeling;
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
        private ModelingControllerInterface modelingController;
        private SecurityControllerInterface securityController;
        private HandModifiedDataControllerInterface handModifiedDataController;
        private MarcovitsControllerInterface marcovitsController;
        private CommandsStoreInterface commandsStore;

        //При откате модели до предыдущего состояния, элементы вью тоже меняются,
        //но так как они прослушиваются на изменения вью, то это влечет за собой 
        //изменение модели и добавление еще одной команды, а она не нужна, так как мы
        //только что забрали предыдущую
        private bool activateChangeListeners = true;

        public Form1()
        {
            try
            {
                InitializeComponent();
                //
                //CommandStore
                //
                commandsStore = new ConcreteCommandStore();
                //
                //Exceptions init
                //
                ConcreteExceptionHandlerInitializer.initThisExceptionHandler(
                    ExceptionHandler.getInstance());
                //
                //Security component
                //
                SecurityModel securityModel = new SecurityModel();
                AutorizationSecurityView securityView =
                    new AutorizationSecurityView(this, securityModel);
                securityController = new SecurityController(securityModel);
                Navigator.Navigator.getInstance().addView(securityView);
                //
                //Hand modified data component
                //
                HandModifiedDataModel handModifiedDataModel = new HandModifiedDataModel();
                HandModifiedDataView handModifiedDataView =
                    new HandModifiedDataView(this, handModifiedDataModel);
                handModifiedDataController = new HandModifiedDataController(handModifiedDataModel,
                    securityModel, commandsStore);
                Navigator.Navigator.getInstance().addView(handModifiedDataView);
                //
                //Marcovits component
                //
                MarcovitsModel marcovitsModel = new MarcovitsModel();
                MarcovitsView marcovitsView = new MarcovitsView(this, marcovitsModel);
                marcovitsController = new MarcovitsController(marcovitsModel, 
                    handModifiedDataModel, commandsStore);
                Navigator.Navigator.getInstance().addView(marcovitsView);
                //
                //Modeling component
                //
                ModelingModel modelingModel = new ModelingModel();
                ModelingView modelingView = new ModelingView(this, modelingModel);
                modelingController = new ModelingController(modelingModel, 
                    handModifiedDataModel, commandsStore);
                Navigator.Navigator.getInstance().addView(modelingView);
                //
                //Settings elements on forms
                //
                comboBox1.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
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
                //
                //Load models stores;
                //
                handModifiedDataController.loadStore();
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
            marcovitsController.getStatistics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modelingController.getStatistics();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (activateChangeListeners)
            {
                try
                {
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            if (modelingController != null)
                            {
                                modelingController.intervalChange(BasicType.year);
                            }
                            break;
                        case 1:
                            if (modelingController != null)
                            {
                                modelingController.intervalChange(BasicType.month);
                            }
                            break;
                        case 2:
                            if (modelingController != null)
                            {
                                modelingController.intervalChange(BasicType.day);
                            }
                            break;
                        case 3:
                            if (modelingController != null)
                            {
                                modelingController.intervalChange(BasicType.hour);
                            }
                            break;
                        case 4:
                            if (modelingController != null)
                            {
                                modelingController.intervalChange(BasicType.minute);
                            }
                            break;
                        case 5:
                            if (modelingController != null)
                            {
                                modelingController.intervalChange(BasicType.second);
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            activateChangeListeners = false;
            commandsStore.recoveryModel();
            activateChangeListeners = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (activateChangeListeners)
            {
                try
                {
                    switch (comboBox3.SelectedIndex)
                    {
                        case 0:
                            if (marcovitsController != null)
                            {
                                marcovitsController.intervalChange(BasicType.year);
                            }
                            break;
                        case 1:
                            if (marcovitsController != null)
                            {
                                marcovitsController.intervalChange(BasicType.month);
                            }
                            break;
                        case 2:
                            if (marcovitsController != null)
                            {
                                marcovitsController.intervalChange(BasicType.day);
                            }
                            break;
                        case 3:
                            if (marcovitsController != null)
                            {
                                marcovitsController.intervalChange(BasicType.hour);
                            }
                            break;
                        case 4:
                            if (marcovitsController != null)
                            {
                                marcovitsController.intervalChange(BasicType.minute);
                            }
                            break;
                        case 5:
                            if (marcovitsController != null)
                            {
                                marcovitsController.intervalChange(BasicType.second);
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            activateChangeListeners = false;
            commandsStore.recoveryModel();
            activateChangeListeners = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (activateChangeListeners)
            {
                modelingController.flagUseCovarChange(checkBox1.Checked);
            }
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
            Navigator.Navigator.getInstance().navigateToPreviousView();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectTab(0);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectTab(0);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            navigatoToMenuView();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectTab(1);
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
                    Navigator.Navigator.getInstance().navigateTo("HandModifiedDataView");
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
                    Navigator.Navigator.getInstance().navigateTo("HandModifiedDataView");
                    break;
                default:
                    //ДОБАВИТЬ СЮДА ИСКЛЮЧЕНИЕ - НЕИЗВЕСТНЫЙ ТИП
                    throw new Exception();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (activateChangeListeners)
            {
                modelingController.numberOfModelingStartsChange(
                    int.Parse(numericUpDown1.Value.ToString()));
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            activateChangeListeners = false;
            commandsStore.rollbackRecoveryModel();
            activateChangeListeners = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            activateChangeListeners = false;
            commandsStore.rollbackRecoveryModel();
            activateChangeListeners = true;
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

        private void button18_Click(object sender, EventArgs e)
        {
            activateChangeListeners = false;
            double value = double.Parse(textBox7.Text);
            ModelConfiguratorInterface<HandModifiedDataState> configurator =
                new UpdateNumberOfLicensesWithModificator(value);
            handModifiedDataController.updateModelsConfig(configurator);
            activateChangeListeners = true;
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
            activateChangeListeners = false;
            commandsStore.recoveryModel();
            activateChangeListeners = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            activateChangeListeners = false;
            commandsStore.rollbackRecoveryModel();
            activateChangeListeners = true;
        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (activateChangeListeners)
            {
                activateChangeListeners = false;
                ModelConfiguratorInterface<HandModifiedDataState> configurator =
                    new UpdateTableItem(dataGridView4.Rows[e.RowIndex].
                    Cells[e.ColumnIndex].Value.ToString(), e.RowIndex, true);
                handModifiedDataController.updateModelsConfig(configurator);
                activateChangeListeners = true;
            }
        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                string text = textBox7.Text;
                for (int i=0; i<textBox7.Text.Length; i++)
                {
                    if(text[i].Equals('.'))
                    {
                        text = text.Remove(i,1);
                        text = text.Insert(i, ",");
                    }
                }

                double value = double.Parse(text);
                button18.Enabled = true;
                textBox7.Text = text;
            }
            catch(Exception ex)
            {
                button18.Enabled = false;
                ExceptionHandler.getInstance().processing(
                    new ValueMastBeANumberException());
            }
        }

        private void dataGridView6_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (activateChangeListeners)
            {
                activateChangeListeners = false;
                ModelConfiguratorInterface<HandModifiedDataState> configurator =
                    new UpdateTableItem(dataGridView6.Rows[e.RowIndex].
                    Cells[e.ColumnIndex].Value.ToString(), e.RowIndex, false);
                handModifiedDataController.updateModelsConfig(configurator);
                activateChangeListeners = true;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            handModifiedDataController.loadStore();
            comboBox2.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            Navigator.Navigator.getInstance().navigateToPreviousView();
        }
    }
}
