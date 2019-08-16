using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace window_explorer.Class
{
    public class sFileInfo
    {
        #region Variables
        private string _name = "";
        private decimal _size = 0;
        private string _path = "";
        #endregion

        #region Constructors
        public sFileInfo()
        {
        }
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        public decimal Size
        {
            get
            {
                return this._size;
            }
            set
            {
                this._size = value;
            }
        }
        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
            }
        }
        #endregion

        #region Private methods

        #endregion
    }
}
