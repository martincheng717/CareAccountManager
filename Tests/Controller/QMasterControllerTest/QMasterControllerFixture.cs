using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.Db.Note.Model;
using CareGateway.Db.QMaster.Model;
using CareGateway.QMaster.Controller;
using CareGateway.QMaster.Logic;
using CareGateway.QMaster.Model;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using NSubstitute;

namespace Tests.Controller.QMasterControllerTest
{
    public class QMasterControllerFixture:BaseFixture<QMasterController>
    {
        public QMasterControllerFixture()
        {
            #region  Call Transfer  
            Builder.RegisterType<CallTransferManager>().As<IQMasterManager<CallTransferResponse, CallTransferRequest>>()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            InitialQMasterInfoByPartnerCaseNo();

            InitialCallTransfer();
            InitialAddNote();

            #endregion

            #region GetQMasterInfo

            Builder.RegisterType<GetQMasterManager>().As<IQMasterManager<QMasterInfoResponse, int>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            InitailQMasterInfoByQMasterKey();
            #endregion

            #region Update QMaster Info

            Builder.RegisterType<UpdateQMasterManager>().As<IQMasterManager<UpdateQMasterRequest>>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            InitialUpdateQMaster();

            #endregion
            #region Common Data
            var logging = Substitute.For<ILogger>();
            logging.Error(Arg.Any<LogObject>());
            logging.When(x => x.Error(Arg.Any<LogObject>()))
                .Do(x => { var i = 1; });
            InitalPartnerCallType();
            #endregion
        }


        #region Call Transfer
        private void InitialAddNote()
        {
            var addNote = Substitute.For<ISqlCommand<AddNoteOutput, AddNoteInput>>();
            addNote.ExecuteAsync(Arg.Is<AddNoteInput>(m => m.AccountIdentifier == new Guid("04C9E5B5716A43C2B55DD4B351C0AA84")))
                .Returns(new AddNoteOutput()
                {
                    Notekey = 1,
                });
            addNote.ExecuteAsync(Arg.Is<AddNoteInput>(m => m.AccountIdentifier == Guid.Empty))
                .Returns(new AddNoteOutput()
                {
                    Notekey = 2,
                });

            addNote.When(
                    x =>
                        x.ExecuteAsync(
                            Arg.Is<AddNoteInput>(m => m.AccountIdentifier == new Guid("04C9E5B5716A43C2B55DD4B351C0AA87"))))
                .Do(x => { throw new Exception("Error while executing addNote 04C9E5B5716A43C2B55DD4B351C0AA87"); });
            Builder.RegisterInstance(addNote);
        }

        private void InitialCallTransfer()
        {
            var callTransfer = Substitute.For<ISqlCommand<CallTransferOutput, CallTransferInput>>();
            callTransfer.ExecuteAsync(
                    Arg.Is<CallTransferInput>(m => m.AccountIdentifier == "04C9E5B5716A43C2B55DD4B351C0AA84"
                    || m.PartnerCaseNo == "Wave000001"))
                .Returns(new CallTransferOutput()
                {
                    QMasterKey = 1
                });
            callTransfer.ExecuteAsync(
                    Arg.Is<CallTransferInput>(m => m.AccountIdentifier == "04C9E5B5716A43C2B55DD4B351C0AA85" &&
                    m.PartnerCallTypeKey == 1))
                .Returns(new CallTransferOutput()
                {
                    QMasterKey = 2
                });
            callTransfer.ExecuteAsync(
                    Arg.Is<CallTransferInput>(m => m.AccountIdentifier == "04C9E5B5716A43C2B55DD4B351C0AA86"))
                .Returns(new CallTransferOutput()
                {
                    QMasterKey = 3
                });
            callTransfer.ExecuteAsync(
                    Arg.Is<CallTransferInput>(m => m.AccountIdentifier == "04C9E5B5716A43C2B55DD4B351C0AA87"))
                .Returns(new CallTransferOutput()
                {
                    QMasterKey = 4
                });
            callTransfer.ExecuteAsync(
                    Arg.Is<CallTransferInput>(m => m.PartnerCaseNo == "Wave000011"))
                .Returns(new CallTransferOutput()
                {
                    QMasterKey = 11
                });
            callTransfer.ExecuteAsync(
                    Arg.Is<CallTransferInput>(m => m.AccountIdentifier == "04C9E5B5716A43C2B55DD4B351C0AA88"))
                .Returns((CallTransferOutput)null);
            callTransfer.When(
                    m =>
                        m.ExecuteAsync(
                            Arg.Is<CallTransferInput>(p => p.AccountIdentifier == "04C9E5B5716A43C2B55DD4B351C0AA89")))
                .Do(x => { throw new Exception("Error while executing Call Transfer 04C9E5B5716A43C2B55DD4B351C0AA89"); });

            Builder.RegisterInstance(callTransfer);
        }

        private void InitialQMasterInfoByPartnerCaseNo()
        {
            var getQMasterByPartnerCaseNo =
                Substitute.For<ISqlCommand<QMasterInfoOutput, GetQMasterbyPartnerCaseNoInput>>();
            getQMasterByPartnerCaseNo.ExecuteAsync(
                    Arg.Is<GetQMasterbyPartnerCaseNoInput>(m => m.PartnerCaseNo == "Wave000001"))
                .Returns(new QMasterInfoOutput()
                {
                    QMasterKey = 1,
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA84",
                    PartnerCaseNo = "Wave000001",
                    PartnerCallTypeKey = 1,
                    CaseID = "",
                    SessionID = Guid.NewGuid()
                });
            getQMasterByPartnerCaseNo.ExecuteAsync(
                    Arg.Is<GetQMasterbyPartnerCaseNoInput>(m => m.PartnerCaseNo == "Wave000002"))
                .Returns(new QMasterInfoOutput()
                {
                    QMasterKey = 2,
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA85",
                    PartnerCaseNo = "Wave000002",
                    PartnerCallTypeKey = 1
                });
            getQMasterByPartnerCaseNo.ExecuteAsync(
                    Arg.Is<GetQMasterbyPartnerCaseNoInput>(m => m.PartnerCaseNo == "Wave000003"))
                .Returns(new QMasterInfoOutput()
                {
                    QMasterKey = 3,
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA86",
                    PartnerCaseNo = "Wave000003"
                });
            getQMasterByPartnerCaseNo.ExecuteAsync(
                    Arg.Is<GetQMasterbyPartnerCaseNoInput>(m => m.PartnerCaseNo == "Wave000004"))
                .Returns(new QMasterInfoOutput()
                {
                    QMasterKey = 4,
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA87",
                    PartnerCaseNo = "Wave000004",
                    PartnerCallTypeKey = 1
                });
            
            Builder.RegisterInstance(getQMasterByPartnerCaseNo);
        }

        #endregion

        #region GetQMasterInfo

        private void InitailQMasterInfoByQMasterKey()
        {
            var getQMasterByQMasterKey =
                Substitute.For<ISqlCommand<QMasterInfoOutput, GetQMasterByQMasterKeyInput>>();
            getQMasterByQMasterKey.ExecuteAsync(
                    Arg.Is<GetQMasterByQMasterKeyInput>(m => m.QMasterKey == 1))
                .Returns(new QMasterInfoOutput()
                {
                    QMasterKey = 1,
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA84",
                    PartnerCaseNo = "Wave000001",
                    PartnerCallTypeKey = 1,
                    CaseID = "",
                    SessionID = new Guid("2099DF46706A424BA175BFD2339290B8")
                });
            getQMasterByQMasterKey.ExecuteAsync(
                    Arg.Is<GetQMasterByQMasterKeyInput>(m => m.QMasterKey == 2))
                .Returns(new QMasterInfoOutput()
                {
                    QMasterKey = 2,
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA85",
                    PartnerCaseNo = "Wave000002",
                    PartnerCallTypeKey = 1
                });
            getQMasterByQMasterKey.ExecuteAsync(
                    Arg.Is<GetQMasterByQMasterKeyInput>(m => m.QMasterKey == 3))
                .Returns(new QMasterInfoOutput()
                {
                    QMasterKey = 3,
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA86",
                    PartnerCaseNo = "Wave000003"
                });
            getQMasterByQMasterKey.ExecuteAsync(
                    Arg.Is<GetQMasterByQMasterKeyInput>(m => m.QMasterKey == 4))
                .Returns(new QMasterInfoOutput()
                {
                    QMasterKey = 4,
                    AccountIdentifier = "04C9E5B5716A43C2B55DD4B351C0AA87",
                    PartnerCaseNo = "Wave000004",
                    PartnerCallTypeKey = 1
                });
            getQMasterByQMasterKey.ExecuteAsync(
                    Arg.Is<GetQMasterByQMasterKeyInput>(m => m.QMasterKey == 11))
                .Returns(new QMasterInfoOutput()
                {
                    QMasterKey = 11,
                    AccountIdentifier = "",
                    PartnerCaseNo = "Wave000011",
                    PartnerCallTypeKey = 1,
                    CaseID = "",
                    SessionID = new Guid("2099DF46706A424BA175BFD2339290B8")
                });

            getQMasterByQMasterKey.ExecuteAsync(
                   Arg.Is<GetQMasterByQMasterKeyInput>(m => m.QMasterKey == 5))
               .Returns((QMasterInfoOutput)null);

            Builder.RegisterInstance(getQMasterByQMasterKey);
        }

        #endregion

        #region UpdateQMaster
        private void InitialUpdateQMaster()
        {
            var updateQMaster = Substitute.For<ISqlCommand<bool, UpdateQMasterInput>>();
            updateQMaster.ExecuteAsync(Arg.Is<UpdateQMasterInput>(m => m.QMasterKey == 1)).Returns(true);
            updateQMaster.ExecuteAsync(Arg.Is<UpdateQMasterInput>(m => m.QMasterKey == 2)).Returns(false);

            updateQMaster.ExecuteAsync(Arg.Is<UpdateQMasterInput>(m => m.CaseID == "1000001")).Returns(true);
            updateQMaster.ExecuteAsync(Arg.Is<UpdateQMasterInput>(m => m.CaseID == "1000002")).Returns(false);

            updateQMaster.When(m => m.ExecuteAsync(Arg.Is<UpdateQMasterInput>(p => p.QMasterKey == 3)))
                .Do(x => { throw new Exception("Error while executing UpdateQMaster 1000003"); });

            Builder.RegisterInstance(updateQMaster);
        }
        #endregion

        #region Common Data
        private void InitalPartnerCallType()
        {

            var partnerCallTypes = new SortedList<int, PartnerCallTypeOutput>();
            partnerCallTypes.Add(1, new PartnerCallTypeOutput()
            {
                PartnerCallTypeKey = 1,
                PartnerCallType = "P01",
                PartnerCallTypeDescription = "P01"
            });

            partnerCallTypes.Add(2, new PartnerCallTypeOutput()
            {
                PartnerCallTypeKey = 2,
                PartnerCallType = "P02",
                PartnerCallTypeDescription = "P02"
            });

            var getAllPartnerCallType =
                Substitute.For<ISqlCommand<SortedList<int, PartnerCallTypeOutput>>>();
            getAllPartnerCallType.ExecuteAsync().Returns(info => partnerCallTypes);

            Builder.RegisterInstance(getAllPartnerCallType);
        }
        #endregion

    }
}
