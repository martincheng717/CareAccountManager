using Autofac;
using Gdot.Care.Common;
using Gdot.Care.Common.Dependency;
using Gdot.Care.Common.Interface;
using CareGateway.Db.Note.Logic;
using CareGateway.Db.Note.Model;
using CareGateway.Note.Logic;
using CareGateway.Note.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gdot.Care.Common.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Note.Controller
{
    [ExcludeFromCodeCoverage]
    public class RegisterProvider : IRegister
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<NoteController>().InstancePerRequest().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<AddNoteManager>().As<Logic.INote<AddNoteResponse, AddNoteRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<GetNoteManager>().As<INote<List<GetNotesOutput>, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<AddNote>().As<ISqlCommand<AddNoteOutput, AddNoteInput>>();
            builder.RegisterType<GetNoteByAccountIdentifier>().As<ISqlCommand<List<GetNotesOutput>, Guid>>();
            builder.RegisterType<RequestHeaderInfo>().As<IRequestHeaderInfo>();
        }
    }
}
