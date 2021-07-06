using DevExpress.ExpressApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [ModelAbstractClass]
    public interface IModelColumnTooltipData:IModelColumn
    {
        IModelTooltipData TooltipData { get; }
    }

    public interface IModelTooltipData: IModelNode
    {

        [Category("DataOnToolTip")]

        bool DataOnToolTip { get; set; }

        [Category("DataOnToolTip")]

        int MaxHeight { get; set; }

        [Category("DataOnToolTip")]

        int MaxWidth { get; set; }

    }
    
}
