using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using CareGateway.Note.Controller;
using Autofac;
using System.Threading.Tasks;
using System.IO;
using System.Web.Http.Results;
using CareGateway.Account.Model;
using CareGateway.Db.Note.Model;
using Gdot.Care.Common.Exceptions;
using CareGateway.Note.Model;
using NSubstitute;
using Gdot.Care.Common.Interface;

namespace Tests.Controller.NoteControllerTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class NoteControllerTest
    {
        private NoteController _controller;
        private NoteControllerFixture _fixture;
        public NoteControllerTest()
        {
            _fixture = new NoteControllerFixture();
            _fixture.Container = _fixture.Builder.Build();
            _controller = _fixture.Container.Resolve<NoteController>();
        }
        #region Add Note
        [Test]
        public async Task TestAddNote_Success()
        {
            _fixture.CareAgentUserName = "GordenJames@Greendotcorp.com";
            var response = await _controller.AddNote(new AddNoteRequest { Note = "test", UserFullName = "GordenJames", AccountIdentifier = "1A552781-373A-4F2C-87D8-EDFCB40767F9" });
            Assert.NotNull(response);
            return;
        }

        [Test]
        public async Task TestAddNote_GdErrorException()
        {
            try
            {
                _fixture.CareAgentUserName = "GordenJames@Greendotcorp.com";
                var response = await _controller.AddNote(new AddNoteRequest
                {
                    Note = "test",
                    UserFullName = "Lisa",
                    AccountIdentifier = "1A552781-373A-4F2C-87D8-EDFCB40767F8"
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
            return;
        }

        [Test]
        public async Task TestAddNote_CareUserKey_BadRequestException()
        {
            _fixture.CareAgentUserName = "";
            try
            {
                var response = await _controller.AddNote(new AddNoteRequest
                {
                    Note = "test",
                    UserFullName = "Lisa",
                    AccountIdentifier = "1A552781-373A-4F2C-87D8-EDFCB40767F8"
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<BadRequestException>(ex);
            }
            return;
        }

        [Test]
        public async Task TestAddNote_AccountIdentifier_GdErrorException()
        {
            try
            {
                _fixture.CareAgentUserName = "GordenJames@Greendotcorp.com";
                var response = await _controller.AddNote(new AddNoteRequest
                {
                    Note = "test",
                    UserFullName = "Lisa",
                    AccountIdentifier = ""
                });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }


            return;
        }
        #endregion
        #region Get Note

        [Test]
        public async Task TestGetNote_Success()
        {
            var response = await _controller.Get("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1");

            var negResult = response as OkNegotiatedContentResult<List<GetNotesOutput>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(2, negResult.Content.Count);
            Assert.AreEqual("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC1", negResult.Content[0].AccountIdentifier);
            Assert.AreEqual("Gorden James", negResult.Content[0].CareAgentName);
            Assert.AreEqual("Test1", negResult.Content[0].ChangeBy);
            Assert.AreEqual(DateTime.MaxValue, negResult.Content[0].CreateDate);
            Assert.AreEqual("Note 1", negResult.Content[0].Note);
            Assert.AreEqual(1, negResult.Content[0].Notekey);
        }
        [Test]
        public async Task TestGetNote_Invalidate()
        {
            try
            {
                var response = await _controller.Get("777177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3");

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdValidateException>(ex);
            }
        }

        [Test]
        public async Task TestGetNote_NoFound()
        {
            try
            {
                var response = await _controller.Get("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC3");

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NotFoundException>(ex);
            }
        }

        [Test]
        public async Task TestGetNote_GDException()
        {
            try
            {
                var response = await _controller.Get("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC4");

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<GdErrorException>(ex);
            }
        }

        [Test]
        public async Task TestGetNote_Exception()
        {
            try
            {
                var response = await _controller.Get("6177A1C3-C17A-4E7C-83CD-A2D4CA62CDC5");

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<Exception>(ex);
            }
        }
        #endregion
    }
}
