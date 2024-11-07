namespace ConsignmentIntegration.Logic.DTO
{
    public class ConsignmentRequest
    {
        public string UniqueId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string CustomerCode { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderAddress2 { get; set; }
        public string SenderSuburb { get; set; }
        public string SenderState { get; set; }
        public string SenderPostcode { get; set; }
        public string SenderReference { get; set; }
        public string SenderEmail { get; set; }
        public string ConsignmentSenderContact { get; set; }
        public string ConsignmentSenderPhone { get; set; }


        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverAddress2 { get; set; }
        public string ReceiverSuburb { get; set; }
        public string ReceiverState { get; set; }
        public string ReceiverPostcode { get; set; }

        public string ReceiverEmail { get; set; }
        public string ConsignmentReceiverContact { get; set; }
        public string ConsignmentReceiverPhone { get; set; }

        public bool PickupRequest { get; set; }
        public bool ConsignmentReceiverIsResidential { get; set; }
        public string ReturnPdfLabels { get; set; }
        public string ReturnPdfConsignment { get; set; }
        public string SpecialInstructions { get; set; }
        public string ConsignmentOtherReferences { get; set; }
        public List<RowResponse> Rows { get; set; }

    }

    public class RowResponse
    {
        public string Reference { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
        public string ItemContentsDescription { get; set; }
        public int Weight { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public string DangerousGoodsUNNumber { get; set; }
        public string DangerousGoodsClass { get; set; }
        public string DangerousGoodsSubRisk { get; set; }
        public string DangerousGoodsPackagingGroup { get; set; }
        public List<ItemResponse> Items { get; set; }
    }

    public class ItemResponse
    {
        public string Barcode { get; set; }
    }


}
