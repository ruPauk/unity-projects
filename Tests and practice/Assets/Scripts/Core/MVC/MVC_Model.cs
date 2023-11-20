using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.MVC
{
    public class 
        MVC_Model : IModel
    {
        private int _value = 0;

        public int Value => _value;
        public void IncValue()
        {
            ++_value;
        }

        //Вот с этими методами не оч понятно
        public void Request()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
