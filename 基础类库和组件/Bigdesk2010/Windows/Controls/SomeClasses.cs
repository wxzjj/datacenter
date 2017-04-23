using System;
using System.Collections.Generic;
using System.Text;

namespace Bigdesk2010.Windows.Controls
{
    public class ListItem
    {
        public ListItem(string value, string text)
        {
            this.ListItemValue = value;
            this.ListItemText = text;
        }

        //public override string ToString()
        //{
        //    return this.ListItemText;
        //}
        public string ListItemValue
        {
            get;
            set;
        }
        public string ListItemText
        {
            get;
            set;
        }
    }
}
