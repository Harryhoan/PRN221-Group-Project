namespace PetSpaBussinessObject
{
    public class VnPayResponseModel
    {
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderDescription { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }

    }
    public class VnPayRequestModel
    {
        public bool id { get; set; }
        public bool Name { get; set; }
        public string Voucher { get; set; }
        public string Amount { get; set; }
    }

}
