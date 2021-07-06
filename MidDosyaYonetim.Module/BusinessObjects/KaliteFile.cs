using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    [NonPersistent]
    public class KaliteFile : BaseObject
    {
        private IObjectSpace objectSpace;
       
        public KaliteFile(Session session)
             : base(session)
        {
        }       
        private FileData file;
        [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData File
        {
            get { return file; }
            set
            {
                SetPropertyValue("File", ref file, value);
            }
        }

    }
}