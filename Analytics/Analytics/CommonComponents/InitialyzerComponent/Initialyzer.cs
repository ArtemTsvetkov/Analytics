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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //CommandStore
            //
            components.commandsStore = new ConcreteCommandStore();
            //
            //Exceptions init
            //
            ConcreteExceptionHandlerInitializer.initThisExceptionHandler(
                ExceptionHandler.ExceptionHandler.getInstance());
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
        }
    }
}
