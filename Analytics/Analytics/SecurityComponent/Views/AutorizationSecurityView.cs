﻿using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.ExceptionHandler;
using Analytics.Navigator.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent
{
    class AutorizationSecurityView : Observer, NavigatorsView
    {
        private Form1 form;
        private BasicModel<SecurityUserInterface, SecurityUserInterface> model;

        public AutorizationSecurityView(Form1 form,
            BasicModel<SecurityUserInterface, SecurityUserInterface> model)
        {
            this.form = form;
            this.model = model;
            this.model.subscribe(this);
        }

        public void notify()
        {
            if (Navigator.Navigator.getInstance().getCurrentViewsName().Equals("AutorizationSecurityView"))
            {
                SecurityUserInterface currentUser = model.getResult();
                if(currentUser.isEnterIntoSystem())
                {
                    try
                    {
                        Navigator.Navigator.getInstance().navigateTo("ModelingView");
                        form.setUserName(currentUser.getLogin());
                        if (currentUser.isAdmin())
                        {
                            form.button23Elem.Visible = true;
                        }
                        else
                        {
                            form.button23Elem.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.getInstance().processing(ex);
                    }
                }
                else
                {
                    form.textBox2Elem.Text = "";
                    form.textBox3Elem.Text = "";
                    Navigator.Navigator.getInstance().navigateTo("AutorizationSecurityView");
                }
            }
        }

        public void show()
        {
            form.tabControl1Elem.SelectTab(0);
        }

        public string getName()
        {
            return "AutorizationSecurityView";
        }
    }
}
