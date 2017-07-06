using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Autofac;
using CareGateway.Note.Logic;
using CareGateway.Note.Model;
using Gdot.Care.Common.Interface;
using CareGateway.Db.Note.Model;
//using Moq;
using NSubstitute;
using Tests.Controller;
using CareGateway.Note.Controller;
using Gdot.Care.Common.Exceptions;

namespace Tests.Controller.NoteControllerTest
{
    [ExcludeFromCodeCoverage]
    public class NoteControllerFixture : BaseFixture<NoteController>
    {
        public string CareAgentUserName { get; set; }

        public NoteControllerFixture()
        {

            #region Add Note
            Builder.RegisterType<AddNoteManager>()
                .As<INote<AddNoteResponse, AddNoteRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);   
                     
            var addNoteCommand = Substitute.For<ISqlCommand<AddNoteOutput, AddNoteInput>>();
            addNoteCommand.ExecuteAsync(Arg.Any<AddNoteInput>()).Returns(Task.Run(() => { return new AddNoteOutput { Notekey = 111 }; }));
            Builder.RegisterInstance(addNoteCommand);

            var requestHeaderInfo = Substitute.For<IRequestHeaderInfo>();
            requestHeaderInfo.GetUserName().Returns(x=>GetAgentUserName());
            Builder.RegisterInstance(requestHeaderInfo);
            #endregion

            #region GetNote
            Builder.RegisterType<GetNoteManager>()
                .As<INote<List<GetNotesOutput>, string>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            var getNoteCommand = Substitute.For<ISqlCommand<List<GetNotesOutput>, Guid>>();
            InitialGetNote(getNoteCommand);
            Builder.RegisterInstance(getNoteCommand);
            #endregion
        }

        private static void InitialGetNote(ISqlCommand<List<GetNotesOutput>, Guid> getNoteCommand)
        {
            getNoteCommand.ExecuteAsync(Arg.Is<Guid>(p => p == new Guid("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1")))
                .Returns(new List<GetNotesOutput>()
                {
                    new GetNotesOutput()
                    {
                        Notekey = 1,
                        AccountIdentifier = "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1",
                        CareAgentName = "Gorden James",
                        ChangeBy = "Test1",
                        CreateDate = DateTime.MaxValue,
                        Note = "Note 1"
                    },
                    new GetNotesOutput()
                    {
                        Notekey = 2,
                        AccountIdentifier = "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC2",
                        ChangeBy = "Test2",
                        CreateDate = DateTime.MaxValue,
                        Note = "Note 2"
                    },
                });
            getNoteCommand.ExecuteAsync(Arg.Is<Guid>(p => p == new Guid("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC2")))
                .Returns(new List<GetNotesOutput>()
                {
                    new GetNotesOutput()
                    {
                        Notekey = 2,
                        AccountIdentifier = "6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC2",
                        ChangeBy = "Test2",
                        CreateDate = DateTime.MaxValue,
                        Note = "Note 2"
                    },
                });

            getNoteCommand.ExecuteAsync(
                    Arg.Is<Guid>(p => p == new Guid("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3")))
                .Returns((List<GetNotesOutput>) null);

            getNoteCommand.When(
                    m =>
                        m.ExecuteAsync(Arg.Is<Guid>(p => p == new Guid("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC4"))))
                .Do(
                    x =>
                    {
                        throw new GdErrorException(
                            "Error while executing GetNotes 6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC4");
                    });

            getNoteCommand.When(
                    m =>
                        m.ExecuteAsync(Arg.Is<Guid>(p => p == new Guid("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC5"))))
                .Do(
                    x => { throw new Exception("Error while executing GetNotes 6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC5"); });
        }

        private string GetAgentUserName()
        {
            return CareAgentUserName;
        }
    }
}
