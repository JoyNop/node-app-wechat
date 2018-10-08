using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddInConfig
{
    public class AddInConfig
    {
        public AddInConfig()
        {
            SubItems = new List<AddInConfig>();
        }
 
        public int ID { set; get; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { set; get; }

        public string ToolTip { set; get; }

        public int ImageListIndex { set; get; }

        public string HintString { set; get; }

        public bool IsMenu { set; get; }

        public bool IsButton { set; get; }

        public int ButtonIndex { set; get; }

        public List<AddInConfig> SubItems { set; get; }
    }

}
