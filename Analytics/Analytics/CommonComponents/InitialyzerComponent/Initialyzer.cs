using Analytics.CommonComponents.ExceptionHandler.Concrete;
using Analytics.CommonComponents.Views;
using Analytics.HandModifiedDataPanel;
using Analytics.MarcovitsComponent;
using Analytics.MenuComponent;
using Analytics.Modeling;
using Analytics.SecurityComponent;
using Analytics.SecurityComponent.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.CommonComponents.InitialyzerComponent
{
    class Initialyzer
    {
        private InitComponents components;
        private Form1 form;

        public Initialyzer(InitComponents components, Form1 form)
        {
            this.form = form;
            this.components = components;
        }

        public void init()
        {
            //
            //Exceptions init
            //
            ConcreteExceptionHandlerInitializer.initThisExceptionHandler(
                ExceptionHandler.ExceptionHandler.getInstance());
            //
            //Config tabs
            form.tabControl1Elem.Appearance = TabAppearance.FlatButtons;
            form.tabControl1Elem.ItemSize = new Size(0, 1);
            form.tabControl1Elem.SizeMode = TabSizeMode.Fixed;
            form.tabControl1Elem.TabStop = false;
            form.tabControl2Elem.Size=new Size(form.tabControl2Elem.Size.Width, form.tabControl2Elem.Size.Height+70);
            form.pictureBox6Elem.Size = new Size(form.pictureBox6Elem.Size.Width, form.pictureBox6Elem.Size.Height+70);
            form.pictureBox8Elem.Size = new Size(form.pictureBox8Elem.Size.Width, form.pictureBox8Elem.Size.Height + 70);
            //
            //
            //Set unvisible elements on first form(for check reading config)
            //
            form.textBox2Elem.Visible = false;
            form.textBox3Elem.Visible = false;
            form.button5Elem.Visible = false;
            //
            //ReadConfig
            //
            ConfigReader.getInstance().read();
            //
            //CommandStore
            //
            components.commandsStore = new ConcreteCommandStore();
            //
            //Security component
            //
            SecurityModel securityModel = new SecurityModel();
            AutorizationSecurityView securityView =
                new AutorizationSecurityView(form, securityModel);
            components.securityController = new SecurityController(securityModel);
            Navigator.Navigator.getInstance().addView(securityView);
            //
            //Hand modified data component
            //
            HandModifiedDataModel handModifiedDataModel = new HandModifiedDataModel();
            HandModifiedDataView handModifiedDataView =
                new HandModifiedDataView(form, handModifiedDataModel);
            components.handModifiedDataController = new HandModifiedDataController(handModifiedDataModel,
                securityModel, components.commandsStore);
            Navigator.Navigator.getInstance().addView(handModifiedDataView);
            //
            //Marcovits component
            //
            MarcovitsModel marcovitsModel = new MarcovitsModel();
            MarcovitsView marcovitsView = new MarcovitsView(form, marcovitsModel);
            components.marcovitsController = new MarcovitsController(marcovitsModel,
                handModifiedDataModel, components.commandsStore);
            Navigator.Navigator.getInstance().addView(marcovitsView);
            //
            //Modeling component
            //
            ModelingModel modelingModel = new ModelingModel();
            ModelingView modelingView = new ModelingView(form, modelingModel);
            components.modelingController = new ModelingController(modelingModel,
                handModifiedDataModel, components.commandsStore);
            Navigator.Navigator.getInstance().addView(modelingView);
            //
            //Settings elements on forms
            //
            form.comboBox1Elem.SelectedIndex = 0;
            form.comboBox3Elem.SelectedIndex = 0;
            form.comboBox2Elem.SelectedIndex = 0;
            form.comboBox4Elem.SelectedIndex = 0;
            //
            //Menu
            //
            Navigator.Navigator.getInstance().addView(new MenuView(form));
            //
            //Menu
            //
            Navigator.Navigator.getInstance().addView(new AddUserView(form));
            //
            //Menu
            //
            Navigator.Navigator.getInstance().addView(new ChangePasswordView(form));
            //
            //Navigator
            //
            Navigator.Navigator.getInstance().navigateTo("AutorizationSecurityView");
            //
            //Load models stores;
            //
            components.handModifiedDataController.loadStore();
            //
            //Set visible elements on first form
            //
            form.textBox2Elem.Visible = true;
            form.textBox3Elem.Visible = true;
            form.button5Elem.Visible = true;
        }
    }
}
