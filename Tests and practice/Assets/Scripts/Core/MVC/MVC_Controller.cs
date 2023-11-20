using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace Assets.Scripts.Core.MVC
{
    public class MVC_Controller : Controller<MVC_Test, MVC_Model>
    {
        public override void UpdateView()
        {
            View.Tmpro.text = Model.Value.ToString();
        }

        protected override void HideView()
        {
            View.Button.gameObject.SetActive(false);
            View.Tmpro.gameObject.SetActive(false);
            View.Button.onClick.RemoveListener(ButtonHandler);
        }

        protected override void ShowView()
        {
            View.Button.gameObject.SetActive(true);
            View.Tmpro.gameObject.SetActive(true);
            View.ButtonTMP.text = "Initial button title";
            View.Tmpro.text = "Initial text for text field";
            View.Button.onClick.AddListener(ButtonHandler);
        }

        private void ButtonHandler()
        {
            Model.IncValue();
            if (Model.Value > 3)
            {
                HideView();
            }
            UpdateView();
        }
    }
}
