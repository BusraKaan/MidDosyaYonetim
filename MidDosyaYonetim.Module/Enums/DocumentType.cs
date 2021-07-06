
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using Microsoft.VisualBasic;

namespace Enums
{
    public enum DocumentType
    {

        [ImageName("TemplatesV2Images.Action_Export_ToExcel")]
        Excel = 1,
        [ImageName("TemplatesV2Images.Action_Export_ToRTF")]
        Word = 2,
        [ImageName("TemplatesV2Images.Action_Export_ToPDF")]
        Pdf = 3,
        [ImageName("TemplatesV2Images.Action_Export_ToImage")]
        Resim = 4,
        [ImageName("Action_FileAttachment_Attach")]
        Diger = 5,
        [ImageName("Action_FileAttachment_Attach")]
        Dwg = 6,
        [ImageName("Action_FileAttachment_Attach")]
        Dfx = 7,
        
    }
}