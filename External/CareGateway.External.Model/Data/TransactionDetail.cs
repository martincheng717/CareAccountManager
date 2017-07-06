using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Data
{
    public class TransactionDetail
    {
        public string TransactionCodeDescription { get; set; }

        public string MerchantLocation { get; set; }
        public string MCCCode { get; set; }
        public string MCCCategory { get; set; }

        public string DeclineCode { get; set; }

        public string DeclineReason { get; set; }

        public decimal? ConversionRate { get; set; }

        public decimal? AuthorizationAmount { get; set; }

        public string AuthorizationDate { get; set; }

        public string AuthorizationReleaseDate { get; set; }

        public string ApprovalCode { get; set; }

        public string WalletID { get; set; }

        public string AccountProxy { get; set; }

        public string AccountGUID { get; set; }

        public string P2PType { get; set; }

        public string P2PSenderName { get; set; }

        public string P2PRecipientName { get; set; }

        public string ReceiptStatus { get; set; }

        public string AnotherSourceAccountType { get; set; }

        public decimal? AnotherSourceAccountAmount { get; set; }

        public decimal? AnotherSourceAmountFee { get; set; }

        public decimal? P2PGrandTotal { get; set; }

        public string AchOutOriginalRequestID { get; set; }

        public string AchOutCardholderCompleteName { get; set; }

        public string AchOutTargetAccountRoutingNumber { get; set; }

        public string AchOutTargetAccountNumber { get; set; }

        public string TopUpCardType { get; set; }
        public decimal? TopUpCardFee { get; set; }
        public string ARN { get; set; }
    }
}
